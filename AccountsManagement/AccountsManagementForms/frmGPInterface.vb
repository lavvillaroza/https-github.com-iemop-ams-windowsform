'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGPInterface
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     August 28, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing and updating of Entries for posting to great plains
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'
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

Public Class frmGPInterface

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private lstParticipants As New List(Of AMParticipants)
    Private lstWESMBillGP As New List(Of WESMBillGPPosted)
    Private lstJournalVoucher As New List(Of JournalVoucher)
    Private lstJournalVoucherDetails As New List(Of JournalVoucherDetails)
    Private curGPEntry As New WESMBillGPPosted
    Private lstObjDetails As New Object    
    Private _IsSearch As Boolean
    Private UpdateToSaveGPRefNo As Boolean = False
    Private Property IsSearch() As Boolean
        Get
            Return _IsSearch
        End Get
        Set(ByVal value As Boolean)
            _IsSearch = value
        End Set
    End Property
    

    Private Sub rb_Uploaded_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_Uploaded.CheckedChanged, rb_Offsetting.CheckedChanged, rb_Collection.CheckedChanged, _
                                                                                                               rb_Payment.CheckedChanged, rb_Adjustment.CheckedChanged, rb_prudential.CheckedChanged
        If rb_Uploaded.Checked Or rb_Offsetting.Checked Or rb_Adjustment.Checked Then
            gb_ChargeType.Enabled = True
        Else
            gb_ChargeType.Enabled = False
        End If

        _IsSearch = False
    End Sub

    Private Sub dtp_From_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp_From.ValueChanged, dtp_To.ValueChanged
        Dim ts As TimeSpan
        ts = dtp_To.Value.Subtract(dtp_From.Value)
        If ts.TotalDays < 0 Then
            MsgBox("Date From Value should be less than or equal to Date To!", MsgBoxStyle.Exclamation, "Error!")
            dtp_From.Value = dtp_To.Value
            Exit Sub
        End If
    End Sub

    Private Sub frmGPInterface_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            WBillHelper = WESMBillHelper.GetInstance()

            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance()

            Me.lstParticipants = WBillHelper.GetAMParticipants()
            Me.cmd_UpdateGPRef.Image = My.Resources.EditDocumentColored22x22
            Me.txt_GPRefNo.ReadOnly = True
            _IsSearch = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_GreatPlainsWindow.ToString(), "Error in loading of window", ex.Message, "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInAccessing.ToString(), 'LDAPModule.LDAP.Username)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_search.Click
        Try
            Dim GetStatus As Integer
            Dim StrLogs As String = ""
            Me.ResetLabels()
            Me.dgv_ViewJV.DataSource = Nothing
            If rb_Uploaded.Checked Or rb_Offsetting.Checked Or rb_Adjustment.Checked Then
                gb_ChargeType.Enabled = True

                If rb_Uploaded.Checked Then
                    Me.UploadedSet()
                    StrLogs = "TransactionType: Uploaded"
                End If

                If rb_Offsetting.Checked Then
                    Me.OffsettingSet()
                    StrLogs = "TransactionType: Offsetting"
                End If

                If rb_Adjustment.Checked Then
                    Me.AdjustmentSet()
                    StrLogs = "TransactionType: Adustment"
                End If
            Else
                gb_ChargeType.Enabled = False

                If rb_Collection.Checked Then
                    Me.CollectionSet()
                    StrLogs = "TransactionType: Collection"
                End If

                If rb_Payment.Checked Then
                    Me.PaymentSet()
                    StrLogs = "TransactionType: Payment"
                End If

                If rb_prudential.Checked Then
                    Me.PrudentialSet()
                    StrLogs = "TransactionType: Prudential"
                End If

                If rb_nssInterest.Checked Then
                    Me.NSSInterestSet()
                    StrLogs = "TransactionType: NSSInterest"
                End If

                If rb_EarnedInterest.Checked Then
                    Me.EarnedInterestSet()
                    StrLogs = "TransactionType: Earned Interest"
                End If

                If rb_SPA.Checked Then
                    Me.SPASet()
                    StrLogs = "TransactionType: Special Payment Agreement"
                End If

                If rb_WhTaxAdj.Checked Then
                    Me.WhTAXAdj()
                    StrLogs = "TransactionType: Withholding TAX Adjustment"
                End If

            End If

            If rb_All.Checked Then
                GetStatus = 2
                StrLogs = StrLogs & ", Status: All"
            ElseIf rb_Posted.Checked Then
                GetStatus = 1
                StrLogs = StrLogs & ", Status: Posted"
            ElseIf rb_NotPost.Checked Then
                GetStatus = 0
                StrLogs = StrLogs & ", Status: Not Posted"
            End If

            Dim lstGPPosted = WBillHelper.GetWESMBillGPPosted(dtp_From.Value, dtp_To.Value, GetStatus)
            Dim ForDatatable As New List(Of WESMBillGPPosted)

            lstJournalVoucher = WBillHelper.GetJournalVoucher(dtp_From.Value, dtp_To.Value)
            lstJournalVoucherDetails = WBillHelper.GetJournalVoucherDetails()

            Me.ClearTextBox()


            For Each itmControl In gb_TransType.Controls
                If itmControl.GetType() Is GetType(RadioButton) Then
                    Dim ctrl = CType(itmControl, RadioButton)
                    If ctrl.Checked = False Then
                        Continue For
                    End If
                    Select Case UCase(ctrl.Text)
                        Case "UPLOADED"
                            lstGPPosted = (From x In lstGPPosted _
                                           Where x.TransactionType = EnumPostedType.U).ToList
                            Exit For
                        Case "OFFSETTING"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.O _
                                       And x.JVNumber <> 0).ToList
                            Exit For
                        Case "COLLECTION"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.C _
                                             Or x.TransactionType = EnumPostedType.DC).ToList
                            Exit For
                        Case "PAYMENT"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.P _
                                             Or x.TransactionType = EnumPostedType.PA _
                                             Or x.TransactionType = EnumPostedType.PD _
                                             Or x.TransactionType = EnumPostedType.PEFT).ToList
                            Exit For
                        Case "ADJUSTMENT"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.MDMCM).ToList
                            Exit For
                        Case "PRUDENTIAL"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.PRR _
                                       Or x.TransactionType = EnumPostedType.PRI _
                                       Or x.TransactionType = EnumPostedType.PRTI).ToList

                        Case "NSS INTEREST"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.NSSI).ToList

                        Case "EARNED INTEREST"
                            lstGPPosted = (From x In lstGPPosted _
                                       Where x.TransactionType = EnumPostedType.IES).ToList

                        Case "SPA"
                            lstGPPosted = (From x In lstGPPosted _
                                           Where x.TransactionType = EnumPostedType.SPAC _
                                           Or x.TransactionType = EnumPostedType.SPASU).ToList()
                        Case "WHTAX ADJUSTMENT"
                            lstGPPosted = (From x In lstGPPosted _
                                           Where x.TransactionType = EnumPostedType.ADJWHTAX).ToList()
                    End Select
                End If
            Next


            If rb_Uploaded.Checked Or rb_Offsetting.Checked Or rb_Adjustment.Checked Then
                Dim FilterByBP As New List(Of WESMBillGPPosted)

                If rb_Uploaded.Checked Then
                    If cb_Energy.Checked Or cb_EVat.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.E _
                                      Or x.Charge = EnumChargeType.EV).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & ", ChargeType: Energy"
                    End If

                    If cb_MarketFees.Checked Or cb_MFVat.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.MF _
                                      Or x.Charge = EnumChargeType.MFV).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & If(InStrRev(StrLogs, "ChargeType:") = 0, ", ChargeType: MarketFees", " - MarketFees")
                    End If

                    If ForDatatable.Count = 0 Then
                        MsgBox("No records found.", MsgBoxStyle.Exclamation, "No Records")
                        Exit Sub
                    End If

                    dgv_ViewJV.DataSource = Me.ListToTable(ForDatatable)
                    Me.lstWESMBillGP = ForDatatable
                Else
                    If cb_Energy.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.E _
                                      ).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & ", ChargeType: Energy"
                    End If

                    If cb_EVat.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.EV _
                                      ).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & If(InStrRev(StrLogs, "ChargeType:") = 0, ", ChargeType: VATonEnergy", " - VATonEnergy")
                    End If

                    If cb_MarketFees.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.MF _
                                      ).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & If(InStrRev(StrLogs, "ChargeType:") = 0, ", ChargeType: MarketFees", " - MarketFees")
                    End If

                    If cb_MFVat.Checked Then
                        FilterByBP = (From x In lstGPPosted _
                                      Where x.Charge = EnumChargeType.MFV).ToList
                        ForDatatable.AddRange(FilterByBP)
                        StrLogs = StrLogs & If(InStrRev(StrLogs, "ChargeType:") = 0, ", ChargeType: VATonMarketFees", " - VATonMarketFees")
                    End If

                    If ForDatatable.Count = 0 Then
                        MsgBox("No records found.", MsgBoxStyle.Exclamation, "No Records")
                        Exit Sub
                    End If

                    dgv_ViewJV.DataSource = Me.ListToTable(ForDatatable)
                    Me.lstWESMBillGP = ForDatatable
                End If

            Else
                If lstGPPosted.Count = 0 Then
                    MsgBox("No records found.", MsgBoxStyle.Exclamation, "No Records")
                    Exit Sub
                End If

                dgv_ViewJV.DataSource = Me.ListToTable(lstGPPosted)
                Me.lstWESMBillGP = lstGPPosted
            End If

            If lstGPPosted.Count = 0 Then
                Me.ClearTextBox()
            End If

            If dgv_ViewJV.Rows.Count > 0 Then
                Me.FillDetailsValue()
            End If

            dgv_ViewJV.Columns("Document Amount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            _IsSearch = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
           
        End Try
    End Sub

    Private Function ListToTable(ByVal lstWESMBillGP As List(Of WESMBillGPPosted)) As DataTable
        Dim dt As New DataTable

        With dt.Columns
            .Add("JV Number")
            .Add("GP Ref No")
            .Add("JV Date")
            .Add("Transaction Type")
            .Add("Document Amount")
            .Add("Status")
            .Add("Particulars")
        End With

        For Each itmWESMGP In lstWESMBillGP
            Dim dr As DataRow
            Dim TransType As String = ""
            dr = dt.NewRow

            Select Case itmWESMGP.TransactionType
                Case EnumPostedType.U
                    TransType = "Uploaded"
                Case EnumPostedType.O
                    TransType = "Offsetting"
                Case EnumPostedType.MDMCM
                    TransType = "Manual DMCM"
                Case EnumPostedType.C, EnumPostedType.DC
                    If itmWESMGP.TransactionType = EnumPostedType.C Then
                        TransType = "Collection"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.DC Then
                        TransType = "Daily Collection"
                    End If
                Case EnumPostedType.P, EnumPostedType.PA, EnumPostedType.PD, EnumPostedType.PEFT
                    If itmWESMGP.TransactionType = EnumPostedType.P Then
                        TransType = "Payment"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.PA Then
                        TransType = "Payment Allocation"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.PD Then
                        TransType = "Payment Default Interest"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.PEFT Then
                        TransType = "Payment EFT/Check/FTF"
                    End If
                Case EnumPostedType.PRR, EnumPostedType.PRI, EnumPostedType.PRTI
                    If itmWESMGP.TransactionType = EnumPostedType.PRR Then
                        TransType = "Prudential Requirement - Replenishment"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.PRI Then
                        TransType = "Prudential Requirement - Interest"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.PRTI Then
                        TransType = "Prudential Requirement - Transfer Interest"
                    End If
                Case EnumPostedType.SPAC, EnumPostedType.SPASU
                    If itmWESMGP.TransactionType = EnumPostedType.SPAC Then
                        TransType = "Prudential Requirement - SPA Closing"
                    ElseIf itmWESMGP.TransactionType = EnumPostedType.SPASU Then
                        TransType = "Prudential Requirement - SPA Setup"                    
                    End If
                Case EnumPostedType.ADJWHTAX
                    TransType = "Prudential Requirement - WhTax Setup"
            End Select

            With itmWESMGP

                dr("JV Number") = Me.BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                dr("GP Ref No") = IIf(.GPRefNo.ToString = "", "GP Reference Not Set", .GPRefNo)
                dr("JV Date") = FormatDateTime(.TransactionDate, DateFormat.ShortDate)
                dr("Transaction Type") = TransType
                dr("Document Amount") = FormatNumber(.DocumentAmount, 2, TriState.True, TriState.True)
                dr("Status") = IIf(.Posted = 0, "Not Posted", "Posted").ToString()
                dr("Particulars") = .Remarks
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        Return dt
    End Function

    Private Sub cmd_Post_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Post.Click
        Try
            'Get JV Number of Selected data grid
            Dim JVNumber As New List(Of Long)

            If dgv_ViewJV.RowCount = 0 Then
                MsgBox("There is no record selected", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If IsSearch = False Then
                MsgBox("Search filters have changed, please click Search first.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewJV.SelectedRows.Count = 1 Then
                JVNumber.Add(CLng(Mid(dgv_ViewJV.CurrentRow.Cells("JV Number").Value.ToString, 5)))
            ElseIf dgv_ViewJV.SelectedRows.Count > 1 Then
                For x = 0 To dgv_ViewJV.SelectedRows.Count - 1
                    JVNumber.Add(CLng(Mid(dgv_ViewJV.SelectedRows(x).Cells("JV Number").Value.ToString, 5)))
                Next
            End If

            'Check if the uploaded WESM Bill have corresponding sales and purchased
            'Disabled by Lance as of 09/16/2016
            'If Me.rb_Uploaded.Checked Then
            '    For Each item In JVNumber
            '        Dim checkExist As Boolean
            '        checkExist = WBillHelper.GetWESMBillSalesAndPurchasedCount(item)

            '        If checkExist = False Then
            '            MsgBox("JV No " & BFactory.GenerateBIRDocumentNumber(item, BIRDocumentsType.JournalVoucher) & " has no corresponding WESM Bill Sales and Purchased flat file. " & _
            '                   "Please upload the file first then try again.", MsgBoxStyle.Exclamation, "Can not post to GP")
            '            Exit Sub
            '        End If
            '    Next
            'End If

            'If rb_Uploaded.Checked = True Then
            '    If Me.txt_ChargeType.Text.Contains("Energy") Then
            '        Dim _chkSalesPurchase = Me.WBillHelper.GetWESMInvoiceSalesAndPurchased(CInt(Me.txt_BillPeriod.Text), Me.txt_STLRun.Text)
            '        If _chkSalesPurchase.Count = 0 Then
            '            MsgBox("No WESM Bill Sales and Purchases found for the selected transaction, Please upload the file first then try again.", MsgBoxStyle.Critical, "No Sales and Purchases found")
            '            Exit Sub
            '        End If
            '    End If
            'End If

            Dim ans As New MsgBoxResult
            ans = MsgBox("Do you really want to post the entries?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Post to GP")

            If ans = MsgBoxResult.Yes Then
                WBillHelper.PostToGP(JVNumber)
                MsgBox("Successfully Posted!", MsgBoxStyle.Information, "Success!")

                Dim jvnumbers As String = ""
                For Each item In JVNumber
                    jvnumbers = item.ToString() & " "
                Next

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPWESMBillJVPostingWindow.ToString, "JV Number/s " & jvnumbers, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyPosted.ToString, AMModule.UserName)

                Me.cmd_search_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

    Private Sub cmd_GenReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenReport.Click
        Try
            If IsSearch = False Then
                MsgBox("Search filters have changed, please click Search first.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If dgv_ViewJV.SelectedRows.Count = 1 Then
                Dim ds As New DataSet
                Dim ForReportMain As New DSReport.JournalVoucherDataTable
                Dim ForReportDetails As New DSReport.JournalVoucherDetailsDataTable
                Dim ForReportAcctCode As New DSReport.AccountingCodeDataTable
                Dim ForReportHeader As String = ""

                For Each itmControl In gb_TransType.Controls
                    If itmControl.GetType Is GetType(RadioButton) Then
                        Dim _itmControl As New RadioButton
                        _itmControl = CType(itmControl, RadioButton)
                        If _itmControl.Checked = True Then
                            ForReportHeader = _itmControl.Text & " Application"
                            Exit For
                        End If

                    End If
                Next

                'Get Journal Voucher of Selected
                Dim JVNumber As Long = 0
                Dim GetAccountCodes = WBillHelper.GetAccountingCodes()

                For Each itmAcctcode In GetAccountCodes
                    Dim dr As DataRow
                    dr = ForReportAcctCode.NewRow

                    dr("ACCT_CODE") = itmAcctcode.AccountCode
                    dr("DESCRIPTION") = itmAcctcode.Description
                    dr("STATUS") = itmAcctcode.Status

                    ForReportAcctCode.Rows.Add(dr)
                    ForReportAcctCode.AcceptChanges()
                Next


                JVNumber = CLng(Mid(dgv_ViewJV.CurrentRow.Cells("JV Number").Value.ToString, 5))

                'Get JV Main and details
                Dim ForJVReportHeader = (From MainJV In lstJournalVoucher _
                                   Where MainJV.JVNumber = JVNumber _
                                   Select MainJV).FirstOrDefault

                Dim ForJVReportDetails = (From DetailsJV In lstJournalVoucherDetails _
                                          Where DetailsJV.JVNumber = JVNumber _
                                          Select DetailsJV).ToList

                Dim drMain As DataRow


                drMain = ForReportMain.NewRow


                With ForJVReportHeader
                    'Main Table
                    drMain("JV_NO") = Me.BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                    drMain("JV_DATE") = .UpdatedDate
                    drMain("BATCHCODE") = .BatchCode
                    drMain("STATUS") = .Status
                    drMain("PREPAREDBY") = .PreparedBy
                    drMain("CHECKEDBY") = .CheckedBy
                    drMain("APPROVEDBY") = .ApprovedBy
                    drMain("UPDATEDBY") = .UpdatedBy
                    drMain("UPDATEDDATE") = .UpdatedDate
                    drMain("REMARKS") = (From x In lstWESMBillGP _
                                         Where x.JVNumber = .JVNumber _
                                         Select x.Remarks).FirstOrDefault
                    drMain("GPREF_NO") = (From x In lstWESMBillGP _
                                         Where x.JVNumber = .JVNumber _
                                         Select x.GPRefNo).FirstOrDefault

                    'For Report Header in Payment
                    If rb_Payment.Checked = True Then
                        Dim _PostType = (From x In lstWESMBillGP _
                                         Where x.JVNumber = .JVNumber _
                                         Select x.PostType).FirstOrDefault

                        If _PostType = EnumPostedType.PEFT.ToString Then
                            ForReportHeader = "Remmittance"
                        ElseIf _PostType = EnumPostedType.P.ToString Then
                            ForReportHeader = "Payment Allocation"
                        ElseIf _PostType = EnumPostedType.PA.ToString Then
                            ForReportHeader = "Payment Allocations - Offsetting"
                        End If

                    End If

                    For Each itmJVDetails In ForJVReportDetails
                        Dim drDetails As DataRow
                        drDetails = ForReportDetails.NewRow
                        If itmJVDetails.Credit = 0 And itmJVDetails.Debit = 0 Then
                            Continue For
                        End If
                        With itmJVDetails
                            'Details Table
                            drDetails("JV_NO") = Me.BFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                            drDetails("ACCOUNTCODE") = .AccountCode
                            drDetails("CREDIT") = .Credit
                            drDetails("DEBIT") = .Debit
                            drDetails("UPDATEDBY") = .UpdatedBy
                            'drDetails("UPDATED_DATE") = .DetailsJV.UpdatedDate
                        End With
                        ForReportDetails.Rows.Add(drDetails)
                        ForReportMain.AcceptChanges()

                    Next
                    ForReportMain.Rows.Add(drMain)
                    ForReportMain.AcceptChanges()
                End With

                ds.Tables.Add(ForReportMain)
                ds.Tables.Add(ForReportDetails)
                ds.Tables.Add(ForReportAcctCode)

                ProgressThread.Show("Please wait while processing.")
                Dim rptViewer As New frmReportViewer
                With rptViewer
                    .LoadJournalVoucher(ds, ForReportHeader)
                    ProgressThread.Close()
                    .Show()
                End With

            Else
                MsgBox("Please select only one entry!", MsgBoxStyle.Exclamation, "Error!")
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub cmd_UpdateGPRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_UpdateGPRef.Click
        Try
            If Me.dgv_ViewJV.RowCount = 0 Then
                Exit Sub
            End If

            If Me.UpdateToSaveGPRefNo = False Then
                Me.UpdateToSaveGPRefNo = True
                Me.cmd_UpdateGPRef.Image = My.Resources.SaveIconColored22x22
                Me.txt_GPRefNo.Focus()
                Me.txt_GPRefNo.ReadOnly = False
            Else

                If dgv_ViewJV.SelectedRows.Count > 1 Then
                    MsgBox("Please select only One(1) entry to update", MsgBoxStyle.Exclamation, "Error!")
                    Exit Sub
                End If

                If Len(Trim(Me.txt_GPRefNo.Text)) = 0 Then
                    MsgBox("Please enter GP Reference Number", MsgBoxStyle.Critical, "Error!")
                    Exit Sub
                End If

                If Len(Trim(Me.txt_GPRefNo.Text)) > 25 Then
                    MsgBox("Allowed input is up to 25 characters only", MsgBoxStyle.Critical, "Error!")
                    Me.txt_GPRefNo.Focus()
                    Exit Sub
                End If

                'Get JV number
                Dim ForJVUpdate = CLng(Mid(dgv_ViewJV.CurrentRow.Cells("JV Number").Value.ToString, 5))

                Dim ans As MsgBoxResult
                ans = MsgBox("Do you really want to Update the GP Reference No?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "GP Interface Update")

                If ans = MsgBoxResult.Yes Then

                    If rb_Uploaded.Checked = True Then
                        If Me.txt_ChargeType.Text.Contains("Energy") Then
                            Dim _chkSalesPurchase = Me.WBillHelper.GetWESMInvoiceSalesAndPurchased(CInt(Me.txt_BillPeriod.Text), Me.txt_STLRun.Text)
                            If _chkSalesPurchase.Count = 0 Then
                                MsgBox("No WESM Bill Sales and Purchases found for the selected transaction, Please upload the file first then try again.", MsgBoxStyle.Critical, "No Sales and Purchases found")
                                Exit Sub
                            End If
                        End If
                    End If

                    WBillHelper.UpdateGPReference(ForJVUpdate, Me.txt_GPRefNo.Text)
                    MsgBox("Successfully updated!", MsgBoxStyle.Information, "Success!")
                    'Updated By Lance 08/17/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPWESMBillJVPostingWindow.ToString, "Update GP reference for JV Number " & ForJVUpdate, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString, AMModule.UserName)

                End If

                Me.cmd_search_Click(Nothing, Nothing)
                Me.cmd_UpdateGPRef.Image = My.Resources.EditDocumentColored22x22
                Me.txt_GPRefNo.ReadOnly = True
                Me.UpdateToSaveGPRefNo = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try
    End Sub

#Region "Details control"
    Private Sub UploadedSet()
        Me.DetailsControl(True, True, True, True, True, True, True)
    End Sub

    Private Sub OffsettingSet()
        Me.DetailsControl(True, True, False, True, True, False, True)
    End Sub

    Private Sub AdjustmentSet()
        Me.DetailsControl(False, False, False, True, False, False, True)
    End Sub

    Private Sub CollectionSet()
        Me.DetailsControl(False, False, False, True, False, False, True)
        Me.lbl_BillingPeriod.Text = "Allocation Date: "
        Me.lbl_ChargeType.Text = "Document Amount: "
    End Sub

    Private Sub PaymentSet()
        Me.DetailsControl(False, False, False, True, True, True, True)
        Me.lbl_BillingPeriod.Text = "Allocation Date: "
        Me.lbl_ChargeType.Text = "Total Collection Allocated: "
        Me.lbl_DueDate.Text = "Total Payment Applied: "
        Me.lbl_STLRun.Text = "Document Amount: "
    End Sub

    Private Sub PrudentialSet()
        Me.DetailsControl(False, False, False, False, False, False, True)
    End Sub

    Private Sub NSSInterestSet()
        Me.DetailsControl(False, False, False, True, False, False, True)
    End Sub

    Private Sub EarnedInterestSet()
        Me.DetailsControl(False, False, False, False, False, False, False)
    End Sub

    Private Sub SPASet()
        Me.DetailsControl(False, False, False, False, False, False, False)
    End Sub

    Private Sub WhTAXAdj()
        Me.DetailsControl(False, False, False, False, False, False, False)
    End Sub

    Private Sub DetailsControl(ByVal isAmt1 As Boolean, ByVal isAmt2 As Boolean, ByVal isAmt3 As Boolean, _
                               ByVal isCharge As Boolean, ByVal isDuedate As Boolean, ByVal isSTL As Boolean, _
                               ByVal isBillPeriod As Boolean)

        'Amount 1 Label and Textbox
        Me.lbl_Amount1.Visible = isAmt1
        Me.txt_Amount1.Visible = isAmt1

        'Amount 2 Label and Textbox
        Me.lbl_Amount2.Visible = isAmt2
        Me.txt_Amount2.Visible = isAmt2

        'Amount 3 Label and TextBox
        Me.lbl_Amount3.Visible = isAmt3
        Me.txt_Amount3.Visible = isAmt3

        'Chargetype Labe and TextBox
        Me.lbl_ChargeType.Visible = isCharge
        Me.txt_ChargeType.Visible = isCharge

        'Due Date Label and Textbox
        Me.lbl_DueDate.Visible = isDuedate
        Me.txt_DueDate.Visible = isDuedate

        'STLRun Label and TextBox
        Me.lbl_STLRun.Visible = isSTL
        Me.txt_STLRun.Visible = isSTL

        'Billing Period Label and TextBox
        Me.lbl_BillingPeriod.Visible = isBillPeriod
        Me.txt_BillPeriod.Visible = isBillPeriod
    End Sub

    Private Sub ResetLabels()
        Me.lbl_Amount1.Text = "Total AP: "
        Me.lbl_Amount2.Text = "Total AR: "
        Me.lbl_Amount3.Text = "Total NSS: "
        Me.lbl_BillingPeriod.Text = "Billing Period: "
        Me.lbl_ChargeType.Text = "Charge Type: "
        Me.lbl_DueDate.Text = "Due Date: "
        Me.lbl_STLRun.Text = "STL Run: "

        Me.txt_BillPeriod.TextAlign = HorizontalAlignment.Center
        Me.txt_ChargeType.TextAlign = HorizontalAlignment.Center
        Me.txt_DueDate.TextAlign = HorizontalAlignment.Center
        Me.txt_STLRun.TextAlign = HorizontalAlignment.Center

    End Sub
#End Region

#Region "Details Value"
    Private Sub FillDetailsValue()
        Dim JVNumber As Long = 0

        If dgv_ViewJV.Rows.Count = 0 Then
            Exit Sub
        End If

        JVNumber = CLng(Mid(Me.dgv_ViewJV.CurrentRow.Cells("JV Number").Value.ToString, 5))

        Dim GPEntry = (From x In lstWESMBillGP _
                        Where x.JVNumber = JVNumber _
                        Select x).FirstOrDefault

        If GPEntry Is Nothing Then
            Exit Sub
        End If

        Me.ClearTextBox()

        curGPEntry = GPEntry

        Me.txt_GPRefNo.Text = curGPEntry.GPRefNo

        If rb_Uploaded.Checked Then ' Uploaded is selected
            Dim WBillList = WBillHelper.GetWESMBills(GPEntry.BillingPeriod, GPEntry.SettlementRun)

            If GPEntry.Charge = EnumChargeType.E Or GPEntry.Charge = EnumChargeType.EV Then
                WBillList = (From x In WBillList _
                             Where x.ChargeType = EnumChargeType.E _
                             Or x.ChargeType = EnumChargeType.EV _
                             Select x).ToList
            Else
                WBillList = (From x In WBillList _
                             Where x.ChargeType = EnumChargeType.MF _
                             Or x.ChargeType = EnumChargeType.MFV _
                             Select x).ToList
            End If

            Me.lstObjDetails = WBillList

            'If Charge type is Energy or VAT On Energy 
            If GPEntry.Charge = EnumChargeType.E Or GPEntry.Charge = EnumChargeType.EV Then
                Dim ARAmount = (From x In WBillList _
                            Where x.Amount < 0 _
                            Select x.Amount).Sum

                Dim APAmount = (From x In WBillList _
                                Where x.Amount > 0 _
                                Select x.Amount).Sum

                Me.lbl_Amount1.Text = "Total AP: "
                Me.lbl_Amount2.Text = "Total AR: "

                Me.DetailsControl(True, True, True, True, True, True, True)

                Dim NSSAmount = ARAmount + APAmount

                Me.txt_Amount1.Text = FormatNumber(APAmount, 2, TriState.True, TriState.True)
                Me.txt_Amount2.Text = FormatNumber(ARAmount, 2, TriState.True, TriState.True)
                Me.txt_Amount3.Text = FormatNumber(NSSAmount, 2, TriState.True, TriState.True)

                'If Charge Type is Market Fees or VAT On Market Fees
            ElseIf GPEntry.Charge = EnumChargeType.MF Or GPEntry.Charge = EnumChargeType.MFV Then
                Me.lbl_Amount1.Text = "Market Fees: "
                Me.lbl_Amount2.Text = "Output Tax: "

                'Change Viewing For Market Fees
                Me.DetailsControl(True, True, False, True, True, True, True)

                Dim MFTotal = (From x In WBillList _
                               Where x.ChargeType = EnumChargeType.MF _
                               Select x.Amount).Sum

                Dim MFVTotal = (From x In WBillList _
                                Where x.ChargeType = EnumChargeType.MFV _
                                Select x.Amount).Sum

                Me.txt_Amount1.Text = FormatNumber(MFTotal, 2, TriState.True, TriState.True, TriState.True)
                Me.txt_Amount2.Text = FormatNumber(MFVTotal, 2, TriState.True, TriState.True, TriState.True)
            End If

            Me.txt_BillPeriod.Text = FormatNumber(GPEntry.BillingPeriod, 0, TriState.False, TriState.False)
            Me.txt_STLRun.Text = GPEntry.SettlementRun
            Me.txt_DueDate.Text = FormatDateTime(WBillList.FirstOrDefault.DueDate, DateFormat.ShortDate)
            Me.txt_GPRefNo.Text = GPEntry.GPRefNo

            'Get Charge Types
            Dim ChargeTypes = (From x In WBillList _
                               Select x.ChargeType Distinct).ToList

            Dim ForCharge As String = ""

            For Each itmCharges In ChargeTypes
                Select Case itmCharges
                    Case EnumChargeType.E
                        ForCharge &= "Energy, "
                    Case EnumChargeType.EV
                        ForCharge &= "VAT On Energy, "
                    Case EnumChargeType.MF
                        ForCharge &= "Market Fees, "
                    Case EnumChargeType.MFV
                        ForCharge &= "VAT On Market Fees, "
                End Select
            Next

            Me.txt_ChargeType.Text = Trim(Mid(ForCharge, 1, Len(ForCharge) - 2))
            Me.txt_BatchCode.Text = GPEntry.BatchCode
        End If ' End of Uploaded Selected

        If rb_Offsetting.Checked Then ' If Offsetting is selected
            Dim OffsetList = WBillHelper.GetWESMBillOffsetDetails(GPEntry.BatchCode)

            Dim GetOffsetType = WBillHelper.GetDebitCreditMemoMain(OffsetList.FirstOrDefault.DMCMNumber)

            Me.lstObjDetails = OffsetList

            Dim ARTotal As Decimal = 0
            Dim APTotal As Decimal = 0

            Dim lstEnumDMCMType As New List(Of EnumDMCMTransactionType)

            If GetOffsetType.FirstOrDefault.TransType = EnumDMCMTransactionType.WESMBillP2COffsetting Then
                lstEnumDMCMType.Add(EnumDMCMTransactionType.WESMBillP2COffsetting)
            Else
                lstEnumDMCMType.Add(EnumDMCMTransactionType.WESMBillP2PC2COffsetting)
            End If

            Dim DMCMList = WBillHelper.GetDebitCreditMemoMain(lstEnumDMCMType, CDate(FormatDateTime(dtp_From.Value, DateFormat.ShortDate).ToString), _
                                                              CDate(FormatDateTime(dtp_To.Value, DateFormat.ShortDate).ToString), GPEntry.BillingPeriod, GPEntry.DueDate, _
                                                              GPEntry.Charge, Nothing)

            For Each itmDMCM In DMCMList
                For Each itmDMCMDetails In itmDMCM.DMCMDetails
                    If GPEntry.Charge = itmDMCM.ChargeType Then
                        With itmDMCMDetails
                            APTotal += .Debit
                            ARTotal += .Credit
                        End With
                    End If
                Next
            Next

            Me.txt_Amount1.Text = FormatNumber(APTotal, 2, TriState.True, TriState.True)
            Me.txt_Amount2.Text = FormatNumber(ARTotal * -1D, 2, TriState.True, TriState.True)

            Me.txt_BatchCode.Text = GPEntry.BatchCode
            Me.txt_BillPeriod.Text = CStr(GPEntry.BillingPeriod)

            'Get Charge Types
            Dim ForCharge As String = ""

            Select Case curGPEntry.Charge
                Case EnumChargeType.E
                    ForCharge &= "Energy, "
                Case EnumChargeType.EV
                    ForCharge &= "VAT On Energy, "
                Case EnumChargeType.MF
                    ForCharge &= "Market Fees, "
                Case EnumChargeType.MFV
                    ForCharge &= "VAT On Market Fees, "
            End Select

            Me.txt_ChargeType.Text = Trim(Mid(ForCharge, 1, Len(ForCharge) - 2))
            Me.txt_DueDate.Text = FormatDateTime(GPEntry.DueDate, DateFormat.ShortDate)
        End If ' End Offsetting Selected


        If rb_Collection.Checked Then ' If Collection Is Selected
            Dim CAllocation = WBillHelper.GetCollections(GPEntry.BatchCode)

            If CAllocation.Count = 0 Then
                CAllocation = WBillHelper.GetCollectionsDaily(GPEntry.BatchCode)
            End If

            Me.lstObjDetails = CAllocation

            Me.txt_BatchCode.Text = GPEntry.BatchCode

            'txt_BillingPeriod is changed to Allocation Date
            If CAllocation.Count = 0 Then 'CAllocation.FirstOrDefault.AllocationDate = Nothing 
                Me.txt_BillPeriod.Text = " - Not Yet Allocated - "
            Else
                If CAllocation.FirstOrDefault.AllocationDate = Nothing Then
                    Me.txt_BillPeriod.Text = " - Not Yet Allocated - "
                Else
                    Me.txt_BillPeriod.Text = FormatDateTime(CAllocation.FirstOrDefault.AllocationDate, DateFormat.ShortDate)
                End If

            End If

            'txt_ChargeType is Changed to Collection Allocation
            Me.txt_ChargeType.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True, TriState.True)
            Me.txt_ChargeType.TextAlign = HorizontalAlignment.Right

        End If ' End Collection Selected

        If rb_Payment.Checked Then ' Payment is Selected
            Select Case GPEntry.TransactionType
                Case EnumPostedType.P
                    Dim Payment = WBillHelper.GetAMPaymentNew(GPEntry.JVNumber)
                    Me.txt_BatchCode.Text = GPEntry.BatchCode
                    Me.txt_BillPeriod.Text = FormatDateTime(Payment.CollAllocationDate, DateFormat.ShortDate)

                    'Total Collection Applied
                    Me.txt_ChargeType.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_ChargeType.TextAlign = HorizontalAlignment.Right

                    'Total Payment Applied
                    Me.txt_DueDate.Text = "0.00"
                    Me.txt_DueDate.TextAlign = HorizontalAlignment.Right

                    'Total Document Amount
                    Me.txt_STLRun.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_STLRun.TextAlign = HorizontalAlignment.Right
                Case EnumPostedType.PA
                    Dim PaymentAllocation = WBillHelper.GetAMPaymentNew(GPEntry.JVNumber)
                    Me.txt_BatchCode.Text = GPEntry.BatchCode
                    Me.txt_BillPeriod.Text = FormatDateTime(PaymentAllocation.CollAllocationDate, DateFormat.ShortDate)

                    'Total Collection Applied
                    Me.txt_ChargeType.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_ChargeType.TextAlign = HorizontalAlignment.Right

                    'Total Payment Applied
                    Me.txt_DueDate.Text = "0.00"
                    Me.txt_DueDate.TextAlign = HorizontalAlignment.Right

                    'Total Document Amount
                    Me.txt_STLRun.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_STLRun.TextAlign = HorizontalAlignment.Right

                Case EnumPostedType.PEFT
                    Dim PaymentEFT = WBillHelper.GetAMPaymentNew(GPEntry.JVNumber)
                    Me.txt_BatchCode.Text = GPEntry.BatchCode
                    Me.txt_BillPeriod.Text = FormatDateTime(PaymentEFT.CollAllocationDate, DateFormat.ShortDate)

                    'Total Collection Applied
                    Me.txt_ChargeType.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_ChargeType.TextAlign = HorizontalAlignment.Right

                    'Total Payment Applied
                    Me.txt_DueDate.Text = "0.00"
                    Me.txt_DueDate.TextAlign = HorizontalAlignment.Right

                    'Total Document Amount
                    Me.txt_STLRun.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
                    Me.txt_STLRun.TextAlign = HorizontalAlignment.Right
            End Select

        End If ' End Payment Selected

        If rb_prudential.Checked Then ' Prudential is Selected
            Me.txt_BatchCode.Text = GPEntry.BatchCode.ToString
            Me.txt_BillPeriod.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
            Me.lbl_BillingPeriod.Text = "PR Amount: "
        End If ' End Prudential Selected

        If rb_Adjustment.Checked Then
            Me.txt_BatchCode.Text = GPEntry.BatchCode.ToString
            Me.txt_ChargeType.Text = FormatNumber(GPEntry.DocumentAmount, 2, TriState.True, TriState.True)
            Me.lbl_ChargeType.Text = "Document Amount"
            Me.lbl_BillingPeriod.Text = "Charge Type"
            Select Case GPEntry.Charge
                Case EnumChargeType.E
                    Me.txt_BillPeriod.Text = "Energy"
                Case EnumChargeType.EV
                    Me.txt_BillPeriod.Text = "VAT on Energy"
                Case EnumChargeType.MF
                    Me.txt_BillPeriod.Text = "Market Fees"
                Case EnumChargeType.MFV
                    Me.txt_BillPeriod.Text = "VAT on Market Fees"
            End Select
        End If
    End Sub

    Private Sub dgv_ViewJV_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_ViewJV.SelectionChanged
        If _IsSearch = True Then
            Me.FillDetailsValue()
        End If
    End Sub

    Private Sub ClearTextBox()
        For Each itmcontrol In Me.grp_Details.Controls
            If GetType(TextBox) Is itmcontrol.GetType() Then
                Dim tBox = CType(itmcontrol, TextBox)
                tBox.Text = ""
            End If
        Next
    End Sub
#End Region

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_Details1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Details1.Click
        Try
            Dim StrLogs As String = ""
            If dgv_ViewJV.RowCount = 0 Then
                MsgBox("There are no records to generate the JV report details.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If IsSearch = False Then
                MsgBox("Search filters have changed, please click Search first.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            Dim JVDetailsSignatory = WBillHelper.GetSignatories("JVDETAILS").FirstOrDefault

            'Added 20140814 - JCLP
            Dim prticipantDetails As New DSReport.BillingParticipantsDataTable
            Dim AllParticipants As New List(Of AMParticipants)
            AllParticipants = WBillHelper.GetAMParticipants()
            For Each itmAllParticipants In AllParticipants
                Dim dr As DataRow
                dr = prticipantDetails.NewRow
                With itmAllParticipants
                    dr("ID_NUMBER") = .IDNumber
                    dr("PARTICIPANT_ID") = .ParticipantID
                    dr("PARTICIPANTADDRESS") = .ParticipantAddress
                    dr("PARTICIPANT_NAME") = .FullName
                    dr("GEN_LOAD") = .GenLoad
                    dr("CONTACT_NO") = .Representative.Contact
                End With
                prticipantDetails.Rows.Add(dr)
                prticipantDetails.AcceptChanges()
            Next
            'End 20140814


            If rb_Uploaded.Checked Then ' Uploaded Selection
                Dim rptDetails As New DSReport.JVReportDetailsDataTable

                Dim ForJVDetails = CType(Me.lstObjDetails, List(Of WESMBill))
                Dim ds As New DataSet
                StrLogs = "TransactionType: Uploaded"
                For Each itmBills In ForJVDetails.OrderBy(Function(x) x.InvoiceNumber)
                    Dim dr As DataRow
                    dr = rptDetails.NewRow
                    With itmBills
                        dr("ID") = .IDNumber
                        dr("SHORTNAME") = (From x In lstParticipants _
                                           Where x.IDNumber = CStr(.IDNumber) _
                                           Select x.ParticipantID).FirstOrDefault
                        dr("INVOICE_NO") = .InvoiceNumber
                        dr("INVOICE_DATE") = CDate(FormatDateTime(.InvoiceDate, DateFormat.ShortDate))
                        dr("DUE_DATE") = CDate(FormatDateTime(.DueDate, DateFormat.ShortDate))

                        Select Case .ChargeType
                            Case EnumChargeType.E
                                dr("CHARGETYPE") = "ENERGY"
                            Case EnumChargeType.EV
                                dr("CHARGETYPE") = "VAT ON ENERGY"
                            Case EnumChargeType.MF
                                dr("CHARGETYPE") = "MARKET FEES"
                            Case EnumChargeType.MFV
                                dr("CHARGETYPE") = "VAT ON MARKET FEES"
                        End Select

                        If .Amount > 0 Then
                            dr("APAMOUNT") = CDec(FormatNumber(.Amount, 2, TriState.True, TriState.True))
                            dr("ARAMOUNT") = CDec(FormatNumber(0, 2, TriState.True, TriState.True))
                        Else
                            dr("ARAMOUNT") = Math.Abs(CDec(FormatNumber(.Amount, 2, TriState.True, TriState.True)))
                            dr("APAMOUNT") = Math.Abs(CDec(FormatNumber(0, 2, TriState.True, TriState.True)))
                        End If
                    End With

                    dr("PREPARED_BY") = AMModule.FullName
                    dr("PREPARED_POSITION") = AMModule.Position

                    dr("CHECKED_BY") = JVDetailsSignatory.Signatory_1
                    dr("CHECKED_POSITION") = JVDetailsSignatory.Position_1

                    dr("APPROVED_BY") = JVDetailsSignatory.Signatory_2
                    dr("APPROVED_POSITION") = JVDetailsSignatory.Position_2

                    rptDetails.Rows.Add(dr)
                    rptDetails.AcceptChanges()
                Next

                ds.Tables.Add(rptDetails)

                'Added 20140814 - JCLP
                ds.Tables.Add(prticipantDetails)
                'End 20140814

                ProgressThread.Show("Please wait while processing.")
                Dim frmRPT As New frmReportViewer
                With frmRPT
                    .LoadJournalVoucherDetailsUploaded(ds, Me.txt_BillPeriod.Text, _
                                                       CInt(Me.curGPEntry.JVNumber), Me.curGPEntry.GPRefNo, _
                                                       Me.curGPEntry.BatchCode, Me.txt_ChargeType.Text)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            End If 'Uploaded Selection


            If rb_Offsetting.Checked Then ' For Offsetting Selection
                Dim rptDetails As New DSReport.JVReportDetailsDataTable
                Dim ForJVDetails = WBillHelper.GetJVSupportDetails(curGPEntry.BatchCode)
                Dim lstWESMBill = WBillHelper.GetWESMBills(curGPEntry.BillingPeriod)

                Dim ds As New DataSet
                StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Offsetting", " - Uploaded")
                For Each itmJVDetails In ForJVDetails
                    Dim dr As DataRow
                    dr = rptDetails.NewRow
                    With itmJVDetails
                        dr("ID") = .IDNumber
                        dr("SHORTNAME") = (From x In lstParticipants _
                                           Where x.IDNumber = .IDNumber _
                                           Select x.ParticipantID).FirstOrDefault
                        dr("INVOICE_NO") = .InvDMCMNo

                        dr("INVOICE_DATE") = CDate(FormatDateTime((From x In lstWESMBill _
                                              Where x.InvoiceNumber = .InvDMCMNo _
                                              And x.ChargeType = curGPEntry.Charge _
                                              Select x.InvoiceDate).FirstOrDefault, DateFormat.ShortDate))
                        dr("DMCM_NO") = Me.BFactory.GenerateBIRDocumentNumber(.DMCMNo, BIRDocumentsType.DMCM)

                        dr("DUE_DATE") = CDate(FormatDateTime((From x In lstWESMBill _
                                              Where x.InvoiceNumber = .InvDMCMNo _
                                              And x.ChargeType = curGPEntry.Charge _
                                              Select x.DueDate).FirstOrDefault, DateFormat.ShortDate))

                        dr("APAMOUNT") = CDec(FormatNumber(.APAmount, 2, TriState.True, TriState.True))
                        dr("ARAMOUNT") = CDec(FormatNumber(.ARAmount, 2, TriState.True, TriState.True))
                    End With

                    dr("PREPARED_BY") = AMModule.FullName
                    dr("PREPARED_POSITION") = AMModule.Position

                    dr("CHECKED_BY") = JVDetailsSignatory.Signatory_1
                    dr("CHECKED_POSITION") = JVDetailsSignatory.Position_1

                    dr("APPROVED_BY") = JVDetailsSignatory.Signatory_2
                    dr("APPROVED_POSITION") = JVDetailsSignatory.Position_2

                    rptDetails.Rows.Add(dr)
                    rptDetails.AcceptChanges()
                Next

                ds.Tables.Add(rptDetails)
                ProgressThread.Show("Please wait while processing.")
                Dim frmRPT As New frmReportViewer
                With frmRPT
                    .LoadJournalVoucherDetailsOffset(ds, Me.txt_BillPeriod.Text, _
                                                       CInt(Me.curGPEntry.JVNumber), Me.curGPEntry.GPRefNo, _
                                                       Me.curGPEntry.BatchCode, Me.txt_ChargeType.Text)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            End If ' End Offsetting Selection

            If rb_Collection.Checked Then ' Collection Selection

                Select Case curGPEntry.TransactionType
                    Case EnumPostedType.C     'Collection Allocation
                        'Dim frmColSummary As New frmCollectionSummary
                        'Dim rptDetails As New DataTable

                        'Dim TotalCash As Decimal = 0, TotalDrawdown As Decimal = 0

                        'StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Collection", " - Collection")

                        'With frmCollectionSummary
                        '    .frmCollectionSummary_Load(Nothing, Nothing)
                        '    rptDetails = .GenerateDataTable(curGPEntry.BatchCode, curGPEntry.JVNumber, TotalCash, TotalDrawdown)
                        'End With

                        'frmProgress.Show()

                        'Dim frmRPT As New frmReportViewer
                        'With frmRPT
                        '    .LoadCollectionSummaryPerJV(rptDetails, TotalCash, TotalDrawdown)
                        '    frmProgress.Close()
                        '    .ShowDialog()
                        'End With

                        ProgressThread.Show("Please wait while processing.")

                        Dim TotalCash As Decimal = 0, TotalDrawdown As Decimal = 0

                        'Get the date today
                        Dim DocumentDate As String = SystemDate.ToString("MM/dd/yyyy") 'SystemDate.ToString("MM/dd/yyyy") 

                        'Get the Signatories
                        Dim Signatories = WBillHelper.GetSignatories("CS").First()

                        'Get participants 
                        Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                        'Get collections
                        Dim listCollections = WBillHelper.GetCollections(curGPEntry.BatchCode)

                        'Get collection monitoring
                        Dim listCollectionMonitoring = WBillHelper.GetCollectionMonitoring(curGPEntry.BatchCode)

                        'Get the DMCM 
                        Dim listDMCM = WBillHelper.GetDebitCreditMemoMainFromJV(curGPEntry.JVNumber)

                        Dim listCollectionFinal = From x In listCollections Join y In listParticipants _
                                                  On x.IDNumber Equals y.IDNumber _
                                                  Select x, y.ParticipantID

                        'Generate collection summary for manual
                        Dim dt = BFactory.GenerateCollectionSummary(curGPEntry.BatchCode, curGPEntry.JVNumber, listCollections, _
                                                                     listCollectionMonitoring, listParticipants, _
                                                                     listDMCM, Signatories, TotalCash, TotalDrawdown, _
                                                                     New DSReport.CollectionSummaryDataTable, DocumentDate)
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadCollectionSummaryPerJV(dt, TotalCash, TotalDrawdown)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With


                    Case EnumPostedType.DC    'Daily Collection
                        'Get the Signatories
                        Dim Signatories = WBillHelper.GetSignatories("DCS").First()

                        'Get participants 
                        Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                        'Get collections
                        Dim listCollections = WBillHelper.GetCollectionsDaily(curGPEntry.BatchCode)

                        Dim dt As DataTable = Me.BFactory.GenerateDailyCollectionSummary(curGPEntry.BatchCode, curGPEntry.JVNumber, listCollections, _
                                                                                         listParticipants, Signatories, _
                                                                                         New DSReport.DailyCollectionSummaryDataTable)
                        ProgressThread.Show("Please wait while processing.")
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDailyCollectionSummary(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                End Select

            End If ' End of Collection Selection

            If rb_Payment.Checked Then ' Payment Selection
                'Dim PHeaders = CType(Me.lstObjDetails, List(Of Payment))
                Dim PymntHelper As New PaymentHelper
                Dim PaymentObject = WBillHelper.GetAMPaymentNew(curGPEntry.JVNumber)
                'PymntHelper.InitializeObjectOnGP(BFactory, WBillHelper, PaymentObject, curGPEntry.JVNumber)

                StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Payment", " - Payment")
                Select Case curGPEntry.TransactionType
                    Case EnumPostedType.P
                        Dim sFolderDialog As New FolderBrowserDialog
                        Dim FilePath As String = ""
                        ProgressThread.Show("Please wait while processing.")
                        PymntHelper.InitializeObjectOnPostedTypeP(BFactory, WBillHelper, PaymentObject)
                        ProgressThread.Close()
                        Try
                            With sFolderDialog
                                .ShowDialog()
                                If .SelectedPath.ToString.Trim.Length = 0 Then
                                    Exit Sub
                                Else
                                    FilePath = sFolderDialog.SelectedPath
                                End If
                            End With
                            frmProgress.Show()
                            PymntHelper.CreateCollAndPaySummReport(FilePath)
                            MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            ProgressThread.Close()
                            MessageBox.Show(ex.Message, "SystemMessage", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Case EnumPostedType.PA
                        Dim sFolderDialog As New FolderBrowserDialog
                        Dim FilePath As String = ""
                        PymntHelper.InitializeObjectOnPostedTypePA(BFactory, WBillHelper, PaymentObject)
                        Try
                            With sFolderDialog
                                .ShowDialog()
                                If .SelectedPath.ToString.Trim.Length = 0 Then
                                    Exit Sub
                                Else
                                    FilePath = sFolderDialog.SelectedPath
                                End If
                            End With
                            ProgressThread.Show("Please wait while processing DMCM Summary Doc.")
                            PymntHelper.CreateDMCMSummaryDoc(FilePath)
                            ProgressThread.Close()
                            MessageBox.Show("Successfully exported please see in targeted path.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As Exception
                            ProgressThread.Close()
                            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Case EnumPostedType.PEFT
                        Try
                            Dim DS As New DataSet
                            Dim tblRFPMain As New DSReport.RFPMainDataTable
                            Dim tblRFPColl As New DSReport.RFPDetailsCollectionDataTable
                            Dim tblRFPPay As New DSReport.RFPDetailsPaymentDataTable

                            ProgressThread.Show("Please wait while processing JV PEFT Report.")

                            PymntHelper.InitializeObjectOnPostedTypePEFT(BFactory, WBillHelper, PaymentObject)
                            DS = PymntHelper.GenerateRFP(tblRFPMain, tblRFPColl, tblRFPPay)

                            Dim RPTViewer As New frmReportViewer
                            With RPTViewer
                                .LoadRFP(DS)
                                ProgressThread.Close()
                                .ShowDialog()
                            End With
                        Catch ex As Exception
                            ProgressThread.Close()
                            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Try
                    Case EnumPostedType.PD

                End Select

            End If ' End of Payment Selection

            If rb_prudential.Checked Then


                Dim _PRSignatory = WBillHelper.GetSignatories("PR_SUMMARY").FirstOrDefault
                Dim listPRHistory = WBillHelper.GetParticipantsPrudentialHistory(curGPEntry.BatchCode)
                Dim dt As New DSReport.PRSummaryReportDataTable

                Dim ds = Me.BFactory.GeneratePrudentialReport(listPRHistory, curGPEntry.JVNumber, _PRSignatory, dt)

                Dim frmRPT As New frmReportViewer

                StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Prudential", " - Prudential")

                With frmRPT
                    If curGPEntry.PostType = EnumPostedType.PRR.ToString() Then
                        .LoadPrudentialSummaryReport(ds, EnumPrudentialTransType.Replenishment)
                    ElseIf curGPEntry.PostType = EnumPostedType.PRI.ToString() Then
                        .LoadPrudentialSummaryReport(ds, EnumPrudentialTransType.InterestAmount)
                    Else
                        .LoadPrudentialSummaryReport(ds, EnumPrudentialTransType.TransferInterestAmount)
                    End If
                    .ShowDialog()
                End With

            End If

            If rb_Adjustment.Checked Then ' For Adjustments created
                Dim _DMCMForReport = WBillHelper.GetDebitCreditMemoMainFromJV(curGPEntry.JVNumber)

                Dim rptTable As New DSReport.DebitCreditMemoDataTable

                StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Adjustment", " - Adjustment")
                For Each DMCMMain In _DMCMForReport
                    For Each itmDetails In DMCMMain.DMCMDetails
                        Dim dr As DataRow
                        dr = rptTable.NewRow
                        Dim _DMCMMain = DMCMMain
                        Dim _cParticipant = (From x In lstParticipants _
                                             Where x.IDNumber = _DMCMMain.IDNumber _
                                             Select x).FirstOrDefault

                        dr(rptTable.ADDRESSColumn) = _cParticipant.ParticipantAddress
                        dr(rptTable.DMCM_NOColumn) = Me.BFactory.GenerateBIRDocumentNumber(_DMCMMain.DMCMNumber, BIRDocumentsType.DMCM)
                        dr(rptTable.EWTColumn) = _DMCMMain.EWT
                        dr(rptTable.EWVColumn) = _DMCMMain.EWV
                        dr(rptTable.ID_NUMBERColumn) = _cParticipant.IDNumber
                        dr(rptTable.JV_NOColumn) = Me.BFactory.GenerateBIRDocumentNumber(_DMCMMain.JVNumber, BIRDocumentsType.JournalVoucher)
                        dr(rptTable.PARTICIPANT_IDColumn) = _cParticipant.ParticipantID
                        dr(rptTable.PARTICIPANT_NAMEColumn) = _cParticipant.FullName
                        dr(rptTable.BUSINESS_STYLEColumn) = _cParticipant.BusinessStyle
                        'dr(rptTable.PARTICIPANT_TINColumn) = _cParticipant.
                        dr(rptTable.PARTICULARSColumn) = _DMCMMain.Particulars
                        dr(rptTable.TOTAL_AMOUNT_DUEColumn) = _DMCMMain.TotalAmountDue
                        dr(rptTable.VAT_EXEMPT_SALEColumn) = _DMCMMain.VATExempt
                        dr(rptTable.VAT_ZEROColumn) = _DMCMMain.VatZeroRated
                        dr(rptTable.VATABLEColumn) = _DMCMMain.Vatable
                        dr(rptTable.VATColumn) = _DMCMMain.VAT
                        dr(rptTable.APPROVED_BYColumn) = _DMCMMain.ApprovedBy
                        dr(rptTable.PREPARED_BYColumn) = _DMCMMain.PreparedBy
                        dr(rptTable.CHECKED_BYColumn) = _DMCMMain.CheckedBy
                        dr(rptTable.ACCOUNT_CODEColumn) = itmDetails.AccountCode
                        dr(rptTable.CR_AMOUNTColumn) = itmDetails.Credit
                        dr(rptTable.DR_AMOUNTColumn) = itmDetails.Debit
                        rptTable.Rows.Add(dr)
                        rptTable.AcceptChanges()
                    Next
                Next

                Dim frmRPT As New frmReportViewer
                With frmRPT
                    .LoadDebitCreditMemo(rptTable)
                    .ShowDialog()
                End With
            End If ' End Of Adjustment Section

            If rb_nssInterest.Checked Then ' For NSS Interest created

            End If ' End of NSS Interest 

            If rb_SPA.Checked Then
                StrLogs = StrLogs & If(InStrRev(StrLogs, "TransactionType:") = 0, "TransactionType: Payment", " - Payment")
                Dim objSPAHelper As New SPAHelper
                Select Case curGPEntry.TransactionType
                    Case EnumPostedType.SPAC
                        Dim SPANumber As Long = WBillHelper.GetAMSPANumber(curGPEntry.JVNumber, EnumPostedType.SPAC)
                        objSPAHelper.InitializeViewing(SPANumber)
                    Case EnumPostedType.SPASU
                        Dim SPANumber As Long = WBillHelper.GetAMSPANumber(curGPEntry.JVNumber, EnumPostedType.SPASU)
                        objSPAHelper.InitializeViewing(SPANumber)
                End Select
                Dim DS As New DataSet
                DS = objSPAHelper.GeneratePaymentScheduleSummary(New DSReport.SPAMainDataTable, New DSReport.SPAInvoicesDataTable)
                'Get the datasource for the report
                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    .LoadSPAPaymentScheduleSummary(DS)
                    .ShowDialog()
                End With
            End If

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try
    End Sub

    Private Sub rb_EarnedInterest_CheckedChanged(sender As Object, e As EventArgs) Handles rb_EarnedInterest.CheckedChanged
        If rb_EarnedInterest.Checked Then
            gb_ChargeType.Enabled = False
            cmd_Details1.Enabled = False
        Else
            gb_ChargeType.Enabled = True
            cmd_Details1.Enabled = True
        End If
    End Sub

End Class