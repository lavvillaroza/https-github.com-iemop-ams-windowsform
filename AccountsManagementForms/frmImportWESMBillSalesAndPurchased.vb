'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmImportWESMBillSalesAndPurchased
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     October 15, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Uploading WESM Bills Sales and Purchased
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                    Description
'   October 15, 2012        Vladimir E. Espiritu                GUI design and basic functionalities
'   May 19, 2013            Vladimir E. Espiritu                Do not include load
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

Public Class frmImportWESMBillSalesAndPurchased
    Dim UtiImporter As ImporterUtility
    Dim WBillHelper As WESMBillHelper

    Private Sub frmImportWESMBillSalesAndPurchased_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            UtiImporter = ImporterUtility.GetInstance()
            UtiImporter.ConnectionString = AMModule.ConnectionString

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

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
        Dim PathFolder As String = ""
        Dim FileName As String = ""
        Try
            Dim FileBillingPd As Integer = 0
            Dim FileSettlementRun As String = ""

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
                MsgBox("Invalid filename format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If fnameSplit.Length <> 4 Or fnameSplit2.Length <> 2 Then
                MsgBox("Invalid filename format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If UCase(fnameSplit2(1).ToString) <> "CSV" Then
                MsgBox("The file to be uploaded must be a CSV file! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If UCase(fnameSplit2(0).ToString) <> "SP" Then
                MsgBox("Invalid filename format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            If (Trim(Mid(fnameSplit(2).ToString, 1, 1))) <> "F" Or Len(fnameSplit(2).ToString) > 4 Then
                MsgBox("Invalid filename format! Please choose another file.", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            FileBillingPd = CInt(fnameSplit(1).ToString)
            FileSettlementRun = CStr(fnameSplit(2).ToString)

            'Check Billing Period
            Dim CalBilling = WBillHelper.GetCalendarBP()
            Dim countBP = (From x In CalBilling _
                           Where x.BillingPeriod = FileBillingPd _
                           Select x).Count()

            If countBP = 0 Then
                MsgBox("Billing period " & FileBillingPd.ToString() & " does not exist in Calendar BP table", MsgBoxStyle.Critical, "Invalid")
                Exit Sub
            End If

            'Get the WESM Invoice
            Dim listWESMInvoice = WBillHelper.GetWESMInvoices(FileBillingPd, FileSettlementRun, EnumFileType.Energy)

            If listWESMInvoice.Count = 0 Then
                MsgBox("There is no existing WESM Bills. Upload first the WESM Bill flatfile. ", MsgBoxStyle.Critical, "No data")
                Exit Sub
            End If

            'Not allowed if the WESM Bill is already posted
            Dim isBillPosted = WBillHelper.IsWESMBillPosted(FileBillingPd, FileSettlementRun, EnumChargeType.E)
            If isBillPosted = True Then
                MsgBox("Cannot upload the records, data are already posted!", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If

            'Get the list of Participants
            Dim listParticipants = WBillHelper.GetAMParticipants()

            'Added by lance 07032020
            Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()

            'Get the Sales and Purchases in the file
            Dim listSalesPurchases = UtiImporter.ImportWESMBillSalesAndPurchased(FileBillingPd, FileSettlementRun, _
                                                                                 PathFolder, FileName, listWESMInvoice, _
                                                                                 listParticipants, getWBSChangeParentList)
            If listSalesPurchases.Count = 0 Then
                MsgBox("The file to be uploaded is empty!", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If

            Dim listCharges = WBillHelper.GetChargeIDCodes()
            
            For Each item In listSalesPurchases

                Dim selectedItem = item

                Dim getParentChangeId = (From x In getWBSChangeParentList
                                         Where x.BillingPeriod = item.BillingPeriod And x.ParentParticipants.IDNumber = item.IDNumber.IDNumber And x.ChildParticipants.IDNumber = item.RegistrationID
                                         Select x).FirstOrDefault

                'Dim TotalTTA As Decimal = Math.Round(selectedItem.VatableSales + selectedItem.ZeroRatedSales + selectedItem.VatablePurchases _
                '                          + selectedItem.ZeroRatedPurchases + selectedItem.ZeroRatedEcozone, 2)

                Dim TotalTTA As Decimal = selectedItem.NetSettlementAmount
                Dim TotalTTAperColumn As Decimal = selectedItem.VatableSales + selectedItem.ZeroRatedSales + selectedItem.ZeroRatedEcozone + selectedItem.VatablePurchases + selectedItem.ZeroRatedPurchases
                Dim totalVATinSP As Decimal = selectedItem.VATonSales + selectedItem.VATonPurchases

                Dim itemWESMBillAmount As Decimal = 0D
                Dim itemWESMBillAmountVat As Decimal = 0D

                itemWESMBillAmount = Math.Round((From x In listWESMInvoice Join y In listCharges On _
                                          x.ChargeID Equals y.ChargeId _
                                          Where x.IDNumber = selectedItem.IDNumber.IDNumber _
                                          And x.RegistrationID = selectedItem.RegistrationID _
                                          And Not x.ChargeID.Contains("TAX")
                                          Select x.Amount).Sum(), 2)

                itemWESMBillAmountVat = Math.Round((From x In listWESMInvoice Join y In listCharges On _
                                      x.ChargeID Equals y.ChargeId _
                                      Where x.IDNumber = selectedItem.IDNumber.IDNumber _
                                      And x.RegistrationID = selectedItem.RegistrationID _
                                      And x.ChargeID.Contains("TAX")
                                      Select x.Amount).Sum(), 2)


                If itemWESMBillAmount <> TotalTTA And itemWESMBillAmount <> TotalTTAperColumn Then
                    Throw New ApplicationException("There is difference between the Invoice and Sales and Purchases Net Amount: " & vbNewLine _
                                     & "ID Number: " & selectedItem.IDNumber.IDNumber & vbNewLine _
                                     & "Registration ID: " & selectedItem.RegistrationID & vbNewLine _
                                     & "Invoice Net Amount: " & FormatNumber(itemWESMBillAmount, 2) & vbNewLine _
                                     & "SalesAndPurchase TTA: " & FormatNumber(TotalTTA, 2))
                End If

                If itemWESMBillAmountVat <> totalVATinSP Then
                    Throw New ApplicationException("There is difference between the Invoice and Sales and Purchases Vat Amount: " & vbNewLine _
                                     & "ID Number: " & selectedItem.IDNumber.IDNumber & vbNewLine _
                                     & "Registration ID: " & selectedItem.RegistrationID & vbNewLine _
                                     & "Invoice Vat Amount: " & FormatNumber(itemWESMBillAmountVat, 2) & vbNewLine _
                                     & "SalesAndPurchase Vat Amount: " & FormatNumber(totalVATinSP, 2))
                End If

            Next

            Dim ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub            
            End If

            'Save the WESM Bill
            WBillHelper.SaveWESMBillSalesAndPurchased(FileBillingPd, FileSettlementRun, listSalesPurchases, getWBSChangeParentList)
            MsgBox("Successfuly Saved!", MsgBoxStyle.Information, "Saved")

            'Updated By Lance 08/17/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillSAPWindow.ToString, FileName, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUploaded.ToString, AMModule.UserName)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Contact Administrator")           
        End Try

    End Sub
End Class