'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmImportWESMBillFlatfile
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     October 06, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Uploading WESM Bills CSV File to the Accounts Management Database, automatically computes for summary and 
'                        for posting in GP.
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL, AM_WESM_BILL_SUMMARY, AM_JV, AM_JV_DETAILS, AM_WESM_BILL_GP_POSTED - WESM Bill CSV File to be uploaded
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                    Description
'   December 12, 2011       juan Carlo L. Panopio               Added checking of Participants in Bill Participants
'                                                               From the csv file.
'   February 12, 2012       Vladimir E. Espiritu                Added Quantity column and functionality to upload WESM Invoice
'   March 09, 2012          Vladimir E. Espiritu                Added progressbar in saving
'   March 15, 2012          Vladimir E. Espiritu                Added GenerateJournalVoucher function
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

Public Class frmImportWESMBillFlatfile

    Dim UtiImporter As ImporterUtility
    Dim WBillHelper As WESMBillHelper
    Dim ListOfSignatories As List(Of DocSignatories)

    Private Sub frmImportInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm
        Try
            UtiImporter = ImporterUtility.GetInstance()
            UtiImporter.ConnectionString = AMModule.ConnectionString

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            'Get WESM Bill Charges
            UtiImporter.WESMBillCharges = WBillHelper.GetChargeIDCodes()

            ListOfSignatories = WBillHelper.GetSignatories()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Dim openFD As New OpenFileDialog

        With openFD
            .Filter = "CSV Files (*.csv)|*.csv"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtDirectory.Text = .FileName
                Me.txtDirectory.SelectionStart = Me.txtDirectory.Text.Length + 1
            End If
        End With
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim fileType As EnumFileType
        Dim CountWESMBillExist As Integer = 0
        Dim BillPeriod As Integer = 0
        Dim FileBillingPd As Integer = 0
        Dim FileSettlementRun As String = ""
        Dim BillType As String = ""
        Dim ftype As String = ""
        Dim ans As MsgBoxResult
        Dim fileChargeType As EnumChargeType
        Dim PathFolder As String = ""
        Dim FileName As String = ""

        Try
            If Me.txtDirectory.Text.Trim.Length = 0 Then
                MsgBox("Please specify the file to be uploaded.!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            ElseIf Not File.Exists(Me.txtDirectory.Text.Trim()) Then
                MsgBox("File does not exist!", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            PathFolder = Path.GetDirectoryName(Me.txtDirectory.Text.Trim())
            FileName = Path.GetFileName(Me.txtDirectory.Text.Trim())

            Dim fnameSplit = Split(FileName, "_")
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

            If UCase(fnameSplit2(0).ToString) <> "MF" And UCase(fnameSplit2(0).ToString) <> "MFR" Then
                MsgBox("Invalid Transaction Type found! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If (Trim(Mid(fnameSplit(2).ToString, 1, 1))) <> "F" Or Len(fnameSplit(2).ToString) > 4 Then
                MsgBox("Invalid Settlement Run! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            FileBillingPd = CInt(fnameSplit(1).ToString)
            FileSettlementRun = If(UCase(fnameSplit2(0).ToString) = "MF", CStr(fnameSplit(2).ToString), CStr(fnameSplit(2).ToString).Replace("F", "R"))
            BillType = CStr(fnameSplit2(0).ToString)

            fileType = EnumFileType.MarketFees

            Dim listWesmBill = UtiImporter.ImportWESMBill(FileBillingPd, FileSettlementRun, PathFolder, FileName)
            Dim listWESMInvoice = UtiImporter.ImportWESMInvoice(FileBillingPd, FileSettlementRun, fileType, _
                                                           PathFolder, FileName)

            If listWesmBill.Count = 0 Then
                MsgBox("The file to be uploaded is empty!", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If

            Dim listCharges = WBillHelper.GetChargeIDCodes()

            'Check Participants
            Dim AllParticipants = (From x In WBillHelper.GetAMParticipants() Where x.Status = EnumStatus.Active Select x).ToList()

            Dim NotInList As New List(Of String)
            For Each item In listWesmBill
                Dim ParentID = item.IDNumber
                Dim CheckParticipant = (From x In AllParticipants Where x.IDNumber = ParentID)
                If CheckParticipant.Count = 0 Then
                    NotInList.Add(ParentID)
                End If

                Dim ChildID = item.RegistrationID
                CheckParticipant = (From x In AllParticipants Where x.IDNumber = ChildID)
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

            If UCase(fnameSplit2(0).ToString) = "MF" Or UCase(fnameSplit2(0).ToString) = "MFR" Then
                fileChargeType = EnumChargeType.MF

                Dim items = (From x In listWESMInvoice Join y In listCharges On x.ChargeID Equals y.ChargeId
                             Where (y.cIDType = EnumChargeType.E Or y.cIDType = EnumChargeType.EV)
                             Select y.ChargeId Distinct).ToList()
                If items.Count > 0 Then
                    MsgBox("Invalid Charge ID Type " & items(0), MsgBoxStyle.Exclamation, "Invalid")
                    Exit Sub
                End If
            End If

            'Check Billing Period
            Dim CalBilling = WBillHelper.GetCalendarBP()
            'Dim MaxBillPd = (From y In CalBilling Order By y.BillingPeriod Descending Select y.BillingPeriod).FirstOrDefault

            Dim countBP = (From x In CalBilling _
                           Where x.BillingPeriod = FileBillingPd _
                           Select x).Count()
            If countBP = 0 Then
                MsgBox("Billing period " & FileBillingPd.ToString() & " does not exist in Calendar BP table", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            Dim selectedBP = (From x In CalBilling _
                              Where x.BillingPeriod = FileBillingPd _
                              Select x).First()

            Dim tmpCharge = From x In listWesmBill Select x.ChargeType Distinct
            For Each item In tmpCharge
                Dim isBillPosted = WBillHelper.IsWESMBillPosted(FileBillingPd, FileSettlementRun, item)
                If isBillPosted = True Then
                    MsgBox("Cannot upload the records, data are already posted!", MsgBoxStyle.Critical, "Warning")
                    Exit Sub
                End If
            Next

            'Check if there is existing records
            CountWESMBillExist = WBillHelper.GetWESMBillCount(FileBillingPd, FileSettlementRun, fileChargeType)

            If CountWESMBillExist <> 0 Then

                ans = MsgBox("There are already existing records, " & vbCrLf _
                              & "Do you want to replace the existing data?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
                If ans = MsgBoxResult.No Then
                    Exit Sub
                End If
            Else
                ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
                If ans = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If

            If ans = MsgBoxResult.Yes Then
                ProgressThread.Show("Please wait while processing.")
                
                Dim itemJV As JournalVoucher
                itemJV = Me.GenerateJournalVoucherForMarketFees(listWesmBill)

                Dim itemGP = Me.GenerateGPPosted(selectedBP, FileSettlementRun, listWesmBill(0).DueDate, _
                                                 fileChargeType, itemJV)

                'ADDED BY LANCE AS OF 10/17/2017 for adjustment of Parent ID
                'START HERE **********************************************************************************************************************************************************************************
                Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
                'END HERE **********************************************************************************************************************************************************************************

                WBillHelper.SaveWESMBill(selectedBP, FileSettlementRun, listWesmBill, listWESMInvoice, fileType, _
                                         itemJV, itemGP, fileChargeType, getWBSChangeParentList)

                ProgressThread.Close()
                MsgBox("Successfully uploaded to Database", MsgBoxStyle.Information, "Success!")

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillWindow.ToString, FileName, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUploaded.ToString, AMModule.UserName)

            Else
                MsgBox("Transaction Cancelled", MsgBoxStyle.Information, "")
                Exit Sub
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

#Region "Functions/Methods"


    Private Function GenerateJournalVoucherForEnergy(ByVal listItems As List(Of WESMBill), ByVal WithholdingTax As Decimal) As JournalVoucher
        Dim result As New JournalVoucher
        Try
            Dim jvDetails As New List(Of JournalVoucherDetails)
            Dim jvDetail As JournalVoucherDetails

            Dim signatories As New DocSignatories

            Try
                signatories = (From x In Me.ListOfSignatories _
                               Where x.DocCode = EnumDocCode.JV.ToString() _
                               Select x).First()
            Catch ex As Exception
                Throw New ApplicationException("No Signatories for Journal Voucher")
            End Try


            'Get the Total AR
            Dim totalAR = (From x In listItems _
                           Where x.Amount < 0 _
                           Select x.Amount).Sum()

            'Get the Total AP
            Dim totalAP = (From x In listItems _
                           Where x.Amount > 0 _
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
            Dim signatories = (From x In Me.ListOfSignatories _
                               Where x.DocCode = EnumDocCode.JV.ToString() _
                               Select x).First()

            'Get the Total AR Market Fees
            Dim totalMFAR = (From x In listItems _
                             Where x.ChargeType = EnumChargeType.MF And x.Amount < 0 _
                             Select x.Amount).Sum()

            'Get the Total AP Market Fees
            Dim totalMFAP = (From x In listItems _
                             Where x.ChargeType = EnumChargeType.MF And x.Amount > 0 _
                             Select x.Amount).Sum()

            'Get the Total AR Vat on Market Fees
            Dim totalMFVAR = (From x In listItems _
                              Where x.ChargeType = EnumChargeType.MFV And x.Amount < 0 _
                              Select x.Amount).Sum()

            'Get the Total AP Vat on Market Fees
            Dim totalMFVAP = (From x In listItems _
                              Where x.ChargeType = EnumChargeType.MFV And x.Amount > 0 _
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

    Private Function GenerateGPPosted(ByVal billingPeriod As CalendarBillingPeriod, ByVal settlementRun As String, _
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

#End Region


End Class
