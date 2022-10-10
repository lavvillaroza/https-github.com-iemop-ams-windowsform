Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmImportWESMBillFromCRSS
    'Private WBillHelper As New WESMBillHelper02
    Private impWBCRSSHelper As New ImportWESMBillFromCRSSDBHelper    
    Private ListOfSignatories As List(Of DocSignatories)
    Private HeaderCheckBox As CheckBox = Nothing
    Private IsHeaderCheckBoxClicked As Boolean = False
    Private TotalCheckBoxes As Integer = 0
    Private TotalCheckedCheckBoxes As Integer = 0

    Private Sub frmImportWESMBillFromCRSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            Me.RefreshDueDateList()
            'Me.AddHeaderCheckBox()
            'AddHandler HeaderCheckBox.KeyUp, AddressOf Me.HeaderCheckBox_KeyUp
            'AddHandler HeaderCheckBox.MouseClick, AddressOf Me.HeaderCheckBox_MouseClick            
            'AddHandler dgv_WESMInvoices.CurrentCellDirtyStateChanged, AddressOf Me.dgv_WESMInvoices_CurrentCellDirtyStateChanged
            'AddHandler dgv_WESMInvoices.CellPainting, AddressOf Me.dgv_WESMInvoices_CellPainting
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub RefreshDueDateList()        
        Me.cmb_DueDate.Items.Clear()        
        For Each item In impWBCRSSHelper.newWESMBillDueDateList
            Me.cmb_DueDate.Items.Add(item.ToString("MM/dd/yyyy"))
        Next
        Me.dgv_WESMInvoices.Rows.Clear()
        Me.dgv_wesmbillsalesandpurchases.Rows.Clear()
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub cmb_DueDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DueDate.SelectedIndexChanged
        Try
            Dim getSelectedDueDate As Date = CDate(Me.cmb_DueDate.SelectedItem)
            ProgressThread.Show("Please wait while fetching...")

            Dim objImportWESMBillFromCRSSList As List(Of ImportWESMBillFromCRSS) = impWBCRSSHelper.FillInTable(getSelectedDueDate)
            Me.dgv_WESMInvoices.Rows.Clear()
            Me.dgv_wesmbillsalesandpurchases.Rows.Clear()
            For Each item In objImportWESMBillFromCRSSList
                If item.WESMBillType = EnumImportFileType.WESMInvoice Then
                    Me.dgv_WESMInvoices.Rows.Add(False, item.BillingPeriod, item.SettlementRun, item.FileType, FormatNumber(item.TotalARAmount, 2, , TriState.True).ToString(), FormatNumber(item.TotalAPAmount, 2, , TriState.True).ToString(), item.Remarks)
                Else
                    Me.dgv_wesmbillsalesandpurchases.Rows.Add(item.BillingPeriod, item.SettlementRun, item.FileType, FormatNumber(item.TotalARAmount, 2, , TriState.True).ToString(), FormatNumber(item.TotalAPAmount, 2, , TriState.True).ToString(), item.Remarks)
                End If
            Next
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()            
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_WESMInvoices_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WESMInvoices.CellContentClick
        If (e.ColumnIndex = 0 AndAlso e.RowIndex >= 0) Then
            Dim value = DirectCast(dgv_WESMInvoices(e.ColumnIndex, e.RowIndex).FormattedValue,  _
                                   Nullable(Of Boolean))
            If (value.HasValue AndAlso value = True) Then
                Dim result = MessageBox.Show("Are you sure to uncheck item?", "", _
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (result = System.Windows.Forms.DialogResult.Yes) Then
                    dgv_WESMInvoices(e.ColumnIndex, e.RowIndex).Value = False                    
                End If
            Else
                dgv_WESMInvoices(e.ColumnIndex, e.RowIndex).Value = True
                Dim rBillingPeriod As Integer = CInt(dgv_WESMInvoices(1, e.RowIndex).Value)
                Dim rSTLRun As String = CStr(dgv_WESMInvoices(2, e.RowIndex).Value)
                Dim rFileType As EnumFileType = CType(CStr(System.Enum.Parse(GetType(EnumFileType), dgv_WESMInvoices(3, e.RowIndex).Value)), EnumFileType)
                Dim totalAR As Decimal = CDec(dgv_WESMInvoices(4, e.RowIndex).Value)
                Dim totalAP As Decimal = CDec(dgv_WESMInvoices(5, e.RowIndex).Value)
                Dim diffTotal As Decimal = totalAR + Math.Abs(totalAP)
                validateUploadingData(rBillingPeriod, rSTLRun, rFileType, totalAR, totalAP, diffTotal, e.ColumnIndex, e.RowIndex)
               

            End If       
        End If
    End Sub

    Private Sub validateUploadingData(bp As Integer, stlRun As String, filetype As EnumFileType,
                                      totalAR As Decimal, totalAP As Decimal, difftotal As Decimal,
                                      eColIndx As Integer, eRowIndx As Integer)

        If difftotal <> 0 And filetype = EnumFileType.Energy Then
            MessageBox.Show("There is difference between the Total AP and AR!" & vbNewLine & "Difference: " & difftotal, "Uploading Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dgv_WESMInvoices(eColIndx, eRowIndx).Value = False
            Exit Sub
        End If

        Dim getWESMBillGroup = (From x In impWBCRSSHelper.newWESMBillGroupList _
                                Where x.BillingPeriod = bp _
                                And x.SettlementRun = stlRun _
                                And x.FileType = filetype _
                                Select x).ToList

        If getWESMBillGroup.Count > 1 Then
            For Each item In getWESMBillGroup
                If item.ListOfError.Distinct.Count > 0 Then
                    MessageBox.Show("Error found in uploaded data.", "Uploading Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    frmViewDetails.ShowListOfErrorsForUploadingInCRSSDB(item.ListOfError.Distinct.ToList)
                    frmViewDetails.gb_totalColl.Hide()
                    frmViewDetails.ShowDialog()
                    dgv_WESMInvoices(eColIndx, eRowIndx).Value = False
                End If
            Next
        End If
    End Sub
    Private Sub dgv_WESMInvoices_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As EventArgs)
        If TypeOf dgv_WESMInvoices.CurrentCell Is DataGridViewCheckBoxCell Then dgv_WESMInvoices.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub HeaderCheckBox_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs)
        HeaderCheckBoxClick(CType(sender, CheckBox))
    End Sub

    Private Sub HeaderCheckBox_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Space Then HeaderCheckBoxClick(CType(sender, CheckBox))
    End Sub

    Private Sub dgv_WESMInvoices_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex = -1 AndAlso e.ColumnIndex = 0 Then ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex)
    End Sub

    'Private Sub AddHeaderCheckBox()
    '    HeaderCheckBox = New CheckBox()
    '    HeaderCheckBox.Size = New Size(15, 15)
    '    Me.dgv_WESMInvoices.Controls.Add(HeaderCheckBox)
    'End Sub

    Private Sub ResetHeaderCheckBoxLocation(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim oRectangle As Rectangle = Me.dgv_WESMInvoices.GetCellDisplayRectangle(ColumnIndex, RowIndex, True)
        Dim oPoint As Point = New Point()
        oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1
        oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1
        HeaderCheckBox.Location = oPoint
    End Sub

    Private Sub HeaderCheckBoxClick(ByVal HCheckBox As CheckBox)
        IsHeaderCheckBoxClicked = True        
        For Each Row As DataGridViewRow In dgv_WESMInvoices.Rows
            CType(Row.Cells("chkbox_select"), DataGridViewCheckBoxCell).Value = HCheckBox.Checked
            If HCheckBox.Checked = True Then
                Dim rBillingPeriod As Integer = CInt(Row.Cells("txtbox_BillingPeriod").Value)

                Dim rSTLRun As String = CStr(Row.Cells("txtbox_stlRun").Value)
                Dim rFileType As EnumFileType = CType(CStr(System.Enum.Parse(GetType(EnumFileType), Row.Cells("txtbox_FileType").Value)), EnumFileType)
                Dim totalAR As Decimal = CDec(Row.Cells("txtbox_TotalARAmount").Value)
                Dim totalAP As Decimal = CDec(Row.Cells("txtbox_TotalAPAmount").Value)
                Dim diffTotal As Decimal = totalAR + Math.Abs(totalAP)
                validateUploadingData(rBillingPeriod, rSTLRun, rFileType, totalAR, totalAP, diffTotal, 0, Row.Index)
            End If          
        Next
        dgv_WESMInvoices.RefreshEdit()
        TotalCheckedCheckBoxes = If(HCheckBox.Checked, TotalCheckBoxes, 0)
        IsHeaderCheckBoxClicked = False
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim CountWESMBillExist As Integer = 0
        Dim BillPeriod As Integer = 0
        Dim FileBillingPd As Integer = 0
        Dim FileSettlementRun As String = ""
        Dim BillType As String = ""
        Dim ftype As String = ""
        Dim ans As MsgBoxResult
        Dim FileChargeType As EnumChargeType
        Dim PathFolder As String = ""
        Dim FileName As String = ""
        Try

            'Check if there is row selected
            Dim ticked As Boolean = False
            For index As Integer = 0 To Me.dgv_WESMInvoices.RowCount - 1
                If CBool(Me.dgv_WESMInvoices.Rows(index).Cells("chkbox_select").Value) Then
                    ticked = True
                    Exit For
                End If
            Next

            If Not ticked Then
                MsgBox("No row/s ticked", MsgBoxStyle.Exclamation, "Select WESM Bill")
                Exit Sub
            End If

            'Check if the WESM Bill selected is already posted
            For index As Integer = 0 To Me.dgv_WESMInvoices.RowCount - 1
                With Me.dgv_WESMInvoices.Rows(index)
                    If CBool(.Cells("chkbox_select").Value) Then
                        FileBillingPd = CInt(.Cells("txtbox_BillingPeriod").Value)
                        FileSettlementRun = CStr(.Cells("txtbox_stlRun").Value)
                        If CType(System.Enum.Parse(GetType(EnumFileType), CStr(.Cells("txtbox_FileType").Value)), EnumFileType) = EnumFileType.Energy Then
                            FileChargeType = EnumChargeType.E
                        Else
                            FileChargeType = EnumChargeType.MF
                        End If
                        Dim isBillPosted = impWBCRSSHelper.WBillHelper.IsWESMBillPosted(FileBillingPd, FileSettlementRun, FileChargeType)
                        If isBillPosted = True Then
                            MsgBox("Billing Period = " & CStr(FileBillingPd) & ", Settlement Run = " & FileSettlementRun & _
                                   ", Charge Type = " & FileChargeType.ToString() & " are already posted!", MsgBoxStyle.Critical, "Warning")
                            Exit Sub
                        End If
                    End If
                End With
            Next

            'Check if there are already existing record in database
            For index As Integer = 0 To Me.dgv_WESMInvoices.RowCount - 1
                With Me.dgv_WESMInvoices.Rows(index)
                    If CBool(.Cells("chkbox_select").Value) Then
                        FileBillingPd = CInt(.Cells("txtbox_BillingPeriod").Value)
                        FileSettlementRun = CStr(.Cells("txtbox_stlRun").Value)
                        If CType(System.Enum.Parse(GetType(EnumFileType), CStr(.Cells("txtbox_FileType").Value)), EnumFileType) = EnumFileType.Energy Then
                            FileChargeType = EnumChargeType.E
                        Else
                            FileChargeType = EnumChargeType.MF
                        End If

                        'Check if there is existing records
                        CountWESMBillExist = impWBCRSSHelper.WBillHelper.GetWESMBillCount(FileBillingPd, FileSettlementRun, FileChargeType)

                        If CountWESMBillExist <> 0 Then
                            ans = MsgBox("There are already existing records, " & vbCrLf _
                                        & "Do you want to replace the existing data?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
                            If ans = MsgBoxResult.No Then
                                Exit Sub
                            Else
                                Exit For
                            End If
                        End If
                    End If
                End With
            Next

            ans = MsgBox("Do you really want to save the records?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while processing.")
            Me.ListOfSignatories = impWBCRSSHelper.WBillHelper.GetSignatories()

            For index As Integer = 0 To Me.dgv_WESMInvoices.RowCount - 1
                With Me.dgv_WESMInvoices.Rows(index)
                    If CBool(.Cells("chkbox_select").Value) Then
                        FileBillingPd = CInt(.Cells("txtbox_BillingPeriod").Value)
                        FileSettlementRun = CStr(.Cells("txtbox_stlRun").Value)                        

                        Dim FileFileType = CType(System.Enum.Parse(GetType(EnumFileType), CStr(.Cells("txtbox_FileType").Value)), EnumFileType)

                        If CType(System.Enum.Parse(GetType(EnumFileType), CStr(.Cells("txtbox_FileType").Value)), EnumFileType) = EnumFileType.Energy Then
                            FileChargeType = EnumChargeType.E
                        Else
                            FileChargeType = EnumChargeType.MF
                        End If

                        FileName = "Billing Period: " & FileBillingPd & ", STLRun: " & FileSettlementRun & ", ChargeType: " & FileChargeType.ToString

                        'ADDED BY LANCE AS OF 10/17/2017 for adjustment of Parent ID
                        Dim getWBSChangeParentList As List(Of WESMBillSummaryChangeParentId) = (From x In impWBCRSSHelper.WBillHelper.GetWESMBillSummaryChangeParentIDAll() Where x.Status = EnumStatus.Active Select x).ToList()
                        'ADDED BY LANCE AS OF 08/08/2021 for adjustment of Reg ID
                        Dim getWBSCRSSMappingList As List(Of WESMInvoiceCRSSMappping) = (From x In impWBCRSSHelper.WBillHelper.GetWESMInvoiceCRSSMappingAll() Select x).ToList()

                        Dim getBillingPeriod As CalendarBillingPeriod = (From x In impWBCRSSHelper.WBillHelper.GetCalendarBP Where x.BillingPeriod = FileBillingPd Select x).FirstOrDefault

                        'Get the WESM Bill in CRSS                        
                        Dim ListWESMInvoice As List(Of WESMInvoice) = (From x In impWBCRSSHelper.newWESMInvoiceList Where x.BillingPeriod = FileBillingPd And x.SettlementRun = FileSettlementRun And x.FileType = FileFileType Select x).ToList()
                        Dim ListSalesAndPurchased As List(Of WESMBillSalesAndPurchased) = (From x In impWBCRSSHelper.newWESMBillSalesPurchaseList Where x.BillingPeriod = FileBillingPd And x.SettlementRun = FileSettlementRun Select x).ToList()
                        Dim TotalEWT = (From x In ListWESMInvoice Where x.ChargeID = "WT" _
                                        Select x.Amount).Sum()

                        'Added for aggregated WESM Invoices/Bills/SalesAndPurchases
                        Dim listAggregatedWESMInvoice As List(Of WESMInvoice) = impWBCRSSHelper.GetAggregatedWESMInvoicesList(getWBSCRSSMappingList, FileBillingPd, FileSettlementRun, FileFileType)
                        Dim listAggregatedWESMSalesAndPurchases As List(Of WESMBillSalesAndPurchased) = impWBCRSSHelper.GetAggregatedWESMSalesPurchases(getWBSCRSSMappingList, FileBillingPd, FileSettlementRun)
                        Dim listAggregatedWESMBill As New List(Of WESMBill)

                        Dim itemJV As JournalVoucher
                        If FileChargeType = EnumChargeType.E Then
                            listAggregatedWESMBill = impWBCRSSHelper.GetAggregatedWESMBills(getWBSCRSSMappingList, FileBillingPd, FileSettlementRun, EnumChargeType.E)
                            itemJV = Me.GenerateJournalVoucherForEnergy(listAggregatedWESMBill, TotalEWT)
                        Else
                            listAggregatedWESMBill = impWBCRSSHelper.GetAggregatedWESMBills(getWBSCRSSMappingList, FileBillingPd, FileSettlementRun, EnumChargeType.MF)
                            itemJV = Me.GenerateJournalVoucherForMarketFees(listAggregatedWESMBill)
                        End If

                        Dim itemGP = Me.GenerateGPPosted(FileBillingPd, FileSettlementRun, listAggregatedWESMBill(0).DueDate, _
                                                         FileChargeType, itemJV)

                       
                        'impWBCRSSHelper.WBillHelper.SaveWESMBill(getBillingPeriod, FileSettlementRun, ListWESMBill, listAggregatedWESMInvoice, FileFileType, _
                        '                                        itemJV, itemGP, FileChargeType, getWBSChangeParentList)

                        'Get the WESM Invoice after saving and update the Invoice Number of its corresponding Sales and Purchased
                        'If fileChargeType = EnumChargeType.E Then
                        '    'Get the WESM Invoice
                        '    Dim listFinalWESMInvoice = listAggregatedWESMInvoice 'impWBCRSSHelper.WBillHelper.GetWESMInvoices(FileBillingPd, FileSettlementRun, EnumFileType.Energy)

                        '    'Update the Invoice Number of the Sales and Purchased
                        '    For Each item In listAggregatedWESMSalesAndPurchases
                        '        item.InvoiceNumber = (From x In listFinalWESMInvoice _
                        '                              Where x.IDNumber = item.IDNumber.IDNumber _
                        '                              And x.RegistrationID = item.RegistrationID _
                        '                              Select x.InvoiceNumber).FirstOrDefault

                        '        If item.InvoiceNumber Is Nothing Then
                        '            Dim getItemChangeID = (From x In getWBSChangeParentList Where x.BillingPeriod = FileBillingPd _
                        '                        And x.ParentParticipants.IDNumber = item.IDNumber.IDNumber _
                        '                        And x.ChildParticipants.IDNumber = item.RegistrationID Select x).FirstOrDefault

                        '            item.InvoiceNumber = (From x In listFinalWESMInvoice _
                        '                              Where x.IDNumber = getItemChangeID.NewParentParticipants.IDNumber _
                        '                              And x.RegistrationID = getItemChangeID.ChildParticipants.IDNumber _
                        '                              Select x.InvoiceNumber).FirstOrDefault

                        '        End If
                        '    Next
                        '    For Each item In getWBSChangeParentList
                        '        Dim xitem = (From x In ListSalesAndPurchased
                        '                     Where x.BillingPeriod = item.BillingPeriod And x.IDNumber.IDNumber = item.ParentParticipants.IDNumber And x.RegistrationID = item.ChildParticipants.IDNumber
                        '                     Select x).FirstOrDefault
                        '        If Not xitem Is Nothing Then
                        '            xitem.IDNumber.IDNumber = item.NewParentParticipants.IDNumber
                        '        End If
                        '    Next
                        '    'impWBCRSSHelper.WBillHelper.SaveWESMBillSalesAndPurchased(FileBillingPd, FileSettlementRun, listAggregatedWESMSalesAndPurchases)
                        'End If

                        'added by Lance 08/08/2021
                        impWBCRSSHelper.WBillHelper.SaveWESMInvoiceCRSS(getBillingPeriod, FileSettlementRun, listAggregatedWESMBill, listAggregatedWESMInvoice, FileFileType, _
                                                                itemJV, itemGP, FileChargeType, getWBSChangeParentList, listAggregatedWESMSalesAndPurchases, ListWESMInvoice, ListSalesAndPurchased)

                        'Updated By Lance 08/17/2014
                        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString, FileName, "Uploading WESM Bills from CRSS DB", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUploaded.ToString, AMModule.UserName)
                    End If
                End With
            Next

            ProgressThread.Close()
            Me.RefreshDueDateList()

            MsgBox("Successfully uploaded to Database", MsgBoxStyle.Information, "Success!")
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
                           And (x.ChargeType = EnumChargeType.E _
                           Or x.ChargeType = EnumChargeType.EV) _
                           Select x.Amount).Sum()

            'Get the Total AP
            Dim totalAP = (From x In listItems _
                           Where x.Amount > 0 _
                           And (x.ChargeType = EnumChargeType.E _
                           Or x.ChargeType = EnumChargeType.EV) _
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
            Dim signatories = (From x In impWBCRSSHelper.WBillHelper.GetSignatories() _
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

    Private Function GenerateGPPosted(ByVal billingPeriod As Integer, ByVal settlementRun As String, _
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
                .BillingPeriod = billingPeriod
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

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.RefreshDueDateList()
    End Sub
End Class