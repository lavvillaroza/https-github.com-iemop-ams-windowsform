'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmPaymentAllocationMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     February 7, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for Creating DM/CM For Adjustments
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description

Option Strict On
Option Explicit On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects



Public Class frmCreateDMCM
    Private WBillHelper As WESMBillHelper

    Private WESMSummary As New List(Of WESMBillSummary)
    Private WESMInvoices As New List(Of WESMBill)
    Private _dicParticipants As New Dictionary(Of String, String)
    Private dicChargeTypeStoWords As New Dictionary(Of EnumChargeType, String)
    Private dicWordsToChargeType As New Dictionary(Of String, EnumChargeType)
    Private AllParticipants As New List(Of AMParticipants)

    Private State As String
    Private Property _state() As String
        Get
            Return State
        End Get
        Set(ByVal value As String)
            State = value
        End Set
    End Property


    Private Sub frmCreateDMCM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            WESMInvoices = WBillHelper.GetWESMBills()

            If WESMInvoices.Count = 0 Then
                MsgBox("No WESM Bill Invoices found", MsgBoxStyle.Critical, "Create DM/CM")
                Exit Sub
            End If

            AllParticipants = WBillHelper.GetAMParticipantsAll()

            dicChargeTypeStoWords.Add(EnumChargeType.E, "ENERGY")
            dicChargeTypeStoWords.Add(EnumChargeType.EV, "VAT ON ENERGY")
            dicChargeTypeStoWords.Add(EnumChargeType.MF, "MARKET FEES")
            dicChargeTypeStoWords.Add(EnumChargeType.MFV, "VAT ON MARKET FEES")


            dicWordsToChargeType.Add("ENERGY", EnumChargeType.E)
            dicWordsToChargeType.Add("VAT ON ENERGY", EnumChargeType.EV)
            dicWordsToChargeType.Add("MARKET FEES", EnumChargeType.MF)
            dicWordsToChargeType.Add("VAT ON MARKET FEES", EnumChargeType.MFV)

            Me.ParticipantsToDic()
            Me.LoadBPtoCombo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        'Get Selected Participant
        Try

            If cbo_Participant.SelectedIndex = -1 Then
                MsgBox("No participant is selected", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            If cbo_BillPeriod.SelectedIndex = -1 Then
                MsgBox("No Billing Period is selected", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            Dim cParticipant As String = _dicParticipants(cbo_Participant.SelectedItem.ToString)
            Dim cBillPeriod As Integer = CInt(cbo_BillPeriod.SelectedItem.ToString)

            'Clear DGV
            dgv_ViewSummary.Rows.Clear()
            dgv_viewInvoice.Rows.Clear()

            If tControl_1.SelectedTab.Name = tcPerSummary.Name Then
                Me.PerInvoice(False)
                'ReInitialize WESM Bills Summary
                WESMSummary = WBillHelper.GetWESMBillSummary()

                If WESMSummary.Count = 0 Then
                    MsgBox("No WESM Bill Summaries found", MsgBoxStyle.Critical, "Create DM/CM")
                    Exit Sub
                End If

                'Filter WESM Bill Summary for Viewing
                Dim toViewSummary = (From x In WESMSummary _
                                     Where x.IDNumber.IDNumber = cParticipant _
                                     And x.BillPeriod = cBillPeriod _
                                     Select x).ToList

                'Load To DataGridView
                For Each item In toViewSummary
                    With item
                        dgv_ViewSummary.Rows.Add(.IDNumber.IDNumber, .SummaryType, .INVDMCMNo, .GroupNo, .IDNumber.ParticipantID, .IDType, _
                                          dicChargeTypeStoWords(.ChargeType), .DueDate, .NewDueDate, .BeginningBalance, .EndingBalance)
                    End With
                Next

                _state = "OK"
            End If

            If tControl_1.SelectedTab.Name = tcPerInvoice.Name Then
                Me.PerInvoice(True)
                WESMInvoices = WBillHelper.GetWESMBills(cBillPeriod)

                Dim toViewInvoices = (From x In WESMInvoices _
                                      Where CStr(x.IDNumber) = cParticipant _
                                      Select x).ToList



                For Each item In toViewInvoices
                    Dim pIDNumber = CStr(item.IDNumber)
                    Dim pIDName = (From x In AllParticipants _
                                   Where x.IDNumber = pIDNumber _
                                   Select x.ParticipantID).FirstOrDefault

                    With item
                        dgv_viewInvoice.Rows.Add(.BillingPeriod, .SettlementRun, .IDNumber, pIDName, _
                                                 .InvoiceNumber, .InvoiceDate, dicChargeTypeStoWords(.ChargeType), .DueDate, .Amount)
                    End With
                Next
                _state = "OK"
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub cmd_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Save.Click
        Dim SummaryListForSaving As New List(Of WESMBillSummary)
        Dim DMCMHeaders As New List(Of DebitCreditMemo)
        Dim ForGPPosting As New List(Of WESMBillGPPosted)
        Dim ForPostingJV As New List(Of JournalVoucher)

        Dim DMCMctr As Long = 1

        Try
            If _state <> "OK" Then
                MsgBox("Filters have changed, Please click search again", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If
            If tControl_1.SelectedTab.Name = Me.tcPerInvoice.Name Then
                If dgv_viewInvoice.Rows.Count = 0 Then
                    MsgBox("No Records found for adjustment", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                Dim P2CMap = WBillHelper.GetP2CMappingAllBP()
                Dim cP2CMapping = (From x In P2CMap _
                                   Where x.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                                   Select x).ToList

                'Save per invoice
                For index As Integer = 0 To dgv_viewInvoice.Rows.Count - 1
                    Dim ForSummary As New WESMBillSummary
                    With Me.dgv_viewInvoice.Rows(index)
                        'If There's Adjustment on the Invoice
                        'Determine Group No and ID Type
                        If CBool(.Cells("invAdjust").Value) = True Then
                            ForSummary.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString)

                            ForSummary.ChargeType = dicWordsToChargeType(.Cells("invChargeType").Value.ToString)
                            ForSummary.DueDate = CDate(.Cells("invDueDate").Value)
                            ForSummary.NewDueDate = CDate(.Cells("invDueDate").Value)
                            ForSummary.BeginningBalance = CDec(.Cells("invAmount").Value)
                            ForSummary.EndingBalance = CDec(.Cells("invAmount").Value)
                            ForSummary.SummaryType = EnumSummaryType.DMCM
                            ForSummary.Adjustment = 1
                            ForSummary.INVDMCMNo = CStr(DMCMctr)

                            'Original Inv/DMCM No
                            ForSummary.IsMFWTaxDeducted = CInt(.Cells("invNumber").Value)


                            'Group Number and ID Type
                            If cbo_destParticipant.SelectedItem.ToString = cbo_Participant.SelectedItem.ToString Then
                                ForSummary.IDType = "P"
                                'check if Participant Exists as Parent
                                Dim isParticipantExist = (From x In WESMSummary _
                                                          Where x.IDNumber.IDNumber = CStr(.Cells("invIDNumber").Value) _
                                                          And x.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                                                          And x.IDType = "P" _
                                                          Select x).ToList
                                ForSummary.IDNumber.IDNumber = _dicParticipants(cbo_destParticipant.SelectedItem.ToString)
                                If isParticipantExist.Count > 0 Then
                                    'If participant is already a parent
                                    ForSummary.GroupNo = isParticipantExist.First.GroupNo
                                Else
                                    'if Participant is not a parent
                                    ForSummary.GroupNo = WBillHelper.GetSequenceID("SEQ_AM_GROUP_NO")
                                End If
                            Else
                                ForSummary.IDType = "C"
                                'Get Group Number of Parent
                                Dim gGroupNo = (From x In WESMSummary _
                                                Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                                And x.IDNumber.IDNumber = _dicParticipants(cbo_destParticipant.SelectedItem.ToString) _
                                                And x.IDType = "P" _
                                                Select x.GroupNo).FirstOrDefault
                                ForSummary.IDNumber.IDNumber = _dicParticipants(cbo_destParticipant.SelectedItem.ToString)
                                If gGroupNo = 0 Then
                                    ForSummary.GroupNo = WBillHelper.GetSequenceID("SEQ_AM_GROUP_NO")
                                Else
                                    ForSummary.GroupNo = gGroupNo
                                End If
                            End If
                            SummaryListForSaving.Add(ForSummary)
                            DMCMctr += 1

                            'Get Participant's Original Parent
                            Dim origParticipantParent = (From x In cP2CMapping _
                                                         Where x.IDNumber = CStr(.Cells("invIDNumber").Value) _
                                                         And x.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                                                         Select x.PCNumber).FirstOrDefault

                            'Create Adjustment To Participant's Original Parent 
                            Dim curParentSummary = (From x In WESMSummary _
                                                    Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                                    And x.IDNumber.IDNumber = origParticipantParent _
                                                    Select x).FirstOrDefault

                            If curParentSummary Is Nothing Then
                                curParentSummary = (From x In WESMSummary _
                                                    Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                                    And x.IDNumber.IDNumber = _dicParticipants(cbo_Participant.SelectedItem.ToString) _
                                                    Select x).FirstOrDefault
                            End If
                            Dim ParentAdjustment As New WESMBillSummary
                            'ParentAdjustment = CType(curParentSummary.Clone, WESMBillSummary)
                            ParentAdjustment.ChargeType = dicWordsToChargeType(.Cells("invChargeType").Value.ToString)
                            ParentAdjustment.Adjustment = 1
                            ParentAdjustment.BeginningBalance = CDec(.Cells("invAmount").Value) * -1D
                            ParentAdjustment.EndingBalance = CDec(.Cells("invAmount").Value) * -1D
                            ParentAdjustment.INVDMCMNo = CStr(DMCMctr)
                            ParentAdjustment.GroupNo = curParentSummary.GroupNo
                            ParentAdjustment.IsMFWTaxDeducted = CInt(.Cells("invNumber").Value)
                            ParentAdjustment.SummaryType = EnumSummaryType.DMCM
                            ParentAdjustment.IDNumber = curParentSummary.IDNumber
                            ParentAdjustment.IDType = curParentSummary.IDType
                            ParentAdjustment.BillPeriod = curParentSummary.BillPeriod
                            ParentAdjustment.DueDate = curParentSummary.DueDate
                            ParentAdjustment.NewDueDate = curParentSummary.NewDueDate
                            SummaryListForSaving.Add(ParentAdjustment)
                            DMCMctr += 1
                        End If
                    End With
                Next
            ElseIf tControl_1.SelectedTab.Name = Me.tcPerSummary.Name Then
                If dgv_ViewSummary.Rows.Count = 0 Then
                    MsgBox("No Records found for adjustment", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If
                For index As Integer = 0 To dgv_ViewSummary.Rows.Count - 1
                    Dim ForSummary As New WESMBillSummary
                    With Me.dgv_ViewSummary.Rows(index)
                        'If There's Adjustment on the summary
                        If CBool(.Cells("hChkBox").Value) = True Then
                            ForSummary.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString)
                            ForSummary.IDNumber.IDNumber = CStr(.Cells("hIDNumber").Value)
                            ForSummary.IDType = CStr(.Cells("hIDType").Value)
                            ForSummary.ChargeType = dicWordsToChargeType(.Cells("hChargeType").Value.ToString)
                            ForSummary.DueDate = CDate(.Cells("hDueDate").Value)
                            ForSummary.NewDueDate = CDate(.Cells("hNewDueDate").Value)
                            ForSummary.BeginningBalance = CDec(.Cells("hAmount").Value)
                            ForSummary.EndingBalance = CDec(.Cells("hAmount").Value)
                            ForSummary.GroupNo = CLng(.Cells("hGroupNo").Value)
                            ForSummary.SummaryType = EnumSummaryType.DMCM
                            ForSummary.INVDMCMNo = CStr(DMCMctr)
                            ForSummary.Adjustment = 1
                            ForSummary.IsMFWTaxDeducted = CInt(.Cells("hINVDMCMNo").Value)
                            SummaryListForSaving.Add(ForSummary)
                            DMCMctr += 1
                        End If
                    End With
                Next
            End If
            

            If SummaryListForSaving.Count = 0 Then
                MsgBox("No adjustments made for Participant " & cbo_Participant.SelectedItem.ToString, MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to save the Adjustments?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Save Adjustments")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            'Create the List for Posting to GP
            ForGPPosting = Me.CreateForPosting(SummaryListForSaving)
            'Create List of Journal Voucher
            ForPostingJV = Me.CreateJournalVoucher(ForGPPosting)
            'Create DMCM For Each Summary for Saving
            If tControl_1.SelectedTab.Name = Me.tcPerSummary.Name Then
                DMCMHeaders = Me.CreateDMCM(SummaryListForSaving, 1, "WESM Bill Summary Adjustment for Billing Period " & cbo_BillPeriod.SelectedItem.ToString & _
                            " and Participant " & cbo_Participant.SelectedItem.ToString() & ".")
            ElseIf tControl_1.SelectedTab.Name = Me.tcPerInvoice.Name Then
                DMCMHeaders = Me.CreateDMCM(SummaryListForSaving, 1, "WESM Bill Invoice Adjustment for Billing Period " & cbo_BillPeriod.SelectedItem.ToString & _
                            " from " & cbo_Participant.SelectedItem.ToString() & " to " & cbo_destParticipant.SelectedItem.ToString & ".")
            End If

            WBillHelper.SaveCollectionAdjustment(SummaryListForSaving, DMCMHeaders, ForGPPosting, ForPostingJV)
            MsgBox("Adjustments are successfully saved.", MsgBoxStyle.OkOnly, "Save Success")
            'Automatically Refreshes the GridView
            cmd_Search_Click(Nothing, Nothing)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

#Region "Form Functions"
    'Load Billing Period From WESMBill Summary
    Private Sub LoadBPtoCombo()
        WESMSummary = WBillHelper.GetWESMBillSummary()
        Dim BP = (From x In WESMSummary _
                          Select x.BillPeriod Distinct).ToList
        cbo_BillPeriod.DataSource = BP
        If cbo_BillPeriod.Items.Count > 0 Then
            cbo_BillPeriod.SelectedItem = 0
        End If
    End Sub

    'Dynamically Load Participants based on Selected BP and Tab Shown
    Private Sub LoadParticipantsToCombo() Handles cbo_BillPeriod.SelectedIndexChanged
        Try

        
            Dim ParticipantList As New List(Of String)
            Dim currBillPeriod As Double = CDbl((cbo_BillPeriod.SelectedItem.ToString))
            WESMInvoices = WBillHelper.GetWESMBills(CInt(currBillPeriod))
            If tControl_1.SelectedTab.Name = Me.tcPerSummary.Name Then
                ParticipantList = (From x In WESMSummary _
                                   Where x.BillPeriod = currBillPeriod _
                                   Select x.IDNumber.ParticipantID Distinct Order By ParticipantID Ascending).ToList
                Me.PerInvoice(False)
                cbo_destParticipant.DataSource = Nothing
            ElseIf tControl_1.SelectedTab.Name = Me.tcPerInvoice.Name Then
                ParticipantList = (From x In WESMInvoices _
                                       Join y In AllParticipants _
                                       On CStr(x.IDNumber) Equals y.IDNumber _
                                       Where x.BillingPeriod = currBillPeriod _
                                       Select y.ParticipantID Distinct Order By ParticipantID Ascending).ToList
                Me.PerInvoice(True)
                Me.fillDestinationCBO()
            End If
            cbo_Participant.DataSource = ParticipantList
            If cbo_Participant.Items.Count > 0 Then
                cbo_Participant.SelectedItem = 0
            End If

            Me.dgv_viewInvoice.Rows.Clear()
            Me.dgv_ViewSummary.Rows.Clear()

        Catch ex As Exception

        End Try
    End Sub

    'Assign BillParticipants to dictionary
    Private Sub ParticipantsToDic()
        Dim participantList As New List(Of AMParticipants)
        participantList = WBillHelper.GetAMParticipantsAll()
        _dicParticipants.Add("0", "0")
        For Each item In participantList
            _dicParticipants.Add(item.ParticipantID, CStr((item.IDNumber)))
        Next
    End Sub

    'Create DMCM Header and Details
    Private Function CreateDMCM(ByRef WESMSummary As List(Of WESMBillSummary), ByVal startDMCMNumber As Integer, _
                                      ByVal Particulars As String) As List(Of DebitCreditMemo)
        Dim DMCMList As New List(Of DebitCreditMemo)
        Dim JVCtr As Integer = 1
        For Each item In WESMSummary
            Dim DMCM As New DebitCreditMemo
            With DMCM
                .IDNumber = item.IDNumber.IDNumber
                .BillingPeriod = CInt(item.BillPeriod)
                .ChargeType = item.ChargeType
                .DueDate = item.NewDueDate
                .Particulars = Particulars
                '.DMCMDetails = Me.CreateDMCMDetails(item.EndingBalance, CStr(item.INVDMCMNo), item.SummaryType, item.IsMFWTaxDeducted)
                .DMCMNumber = CInt(item.INVDMCMNo)
                .JVNumber = JVCtr
                item.IsMFWTaxDeducted = 0
                JVCtr += 1
            End With
            DMCMList.Add(DMCM)
        Next

        Return DMCMList
    End Function
    Private Function CreateDMCMDetails(ByVal DMCMAmount As Decimal, ByVal DMCMNo As String, ByVal SummaryType As EnumSummaryType) As List(Of DebitCreditMemoDetails)
        Dim DMCMDetails As New List(Of DebitCreditMemoDetails)

        'for Debit
        Dim DMCMDebit As New DebitCreditMemoDetails
        With DMCMDebit
            .AccountCode = DebitCode
            .Debit = Math.Abs(DMCMAmount)
            .Credit = 0
            '.DMCMNumber = DMCMNo
            .SummaryType = SummaryType
            '.InvDMCMNo = INVDMCMNo
        End With
        DMCMDetails.Add(DMCMDebit)

        'for Credit
        Dim DMCMCredit As New DebitCreditMemoDetails
        With DMCMCredit
            .AccountCode = CreditCode
            .Debit = 0
            .Credit = Math.Abs(DMCMAmount)
            '.DMCMNumber = DMCMNo
            .SummaryType = SummaryType
            '.InvDMCMNo = INVDMCMNo
        End With
        DMCMDetails.Add(DMCMCredit)

        Return DMCMDetails
    End Function

    'Create ForPosting to GP Entry
    Private Function CreateForPosting(ByVal WESMBills As List(Of WESMBillSummary)) As List(Of WESMBillGPPosted)
        Dim PostingToGPList As New List(Of WESMBillGPPosted)
        Dim ctr As Integer = 1
        For Each summary In WESMBills
            Dim PostingToGP As New WESMBillGPPosted
            Dim cChargeType As String = CStr(IIf(summary.ChargeType = EnumChargeType.E, "Energy", _
                                                 IIf(summary.ChargeType = EnumChargeType.EV, "VAT on Energy", _
                                                     IIf(summary.ChargeType = EnumChargeType.MF, "Market Fees", "VAT on Martket Fees"))))

            With PostingToGP
                '.APamt = Math.Abs(CDec(summary.EndingBalance.ToString("#,##0.00")))
                '.ARamt = Math.Abs(CDec(Math.Abs(summary.EndingBalance).ToString("#,##0.00")))
                .BillingPeriod = summary.BillPeriod
                .Charge = summary.ChargeType
                .DueDate = summary.DueDate
                .PostType = "A"
                .Remarks = "Adjustment Entry for Participant: " & summary.IDNumber.ParticipantID & ", " & _
                            "for Billing Period = " & summary.BillPeriod & ", with Due Date: " & summary.DueDate & ", " & _
                            "and Charge Type: " & cChargeType & "."
                .BatchCode = CStr(ctr)
                ctr += 1
            End With
            PostingToGPList.Add(PostingToGP)
        Next
        Return PostingToGPList
    End Function

    'Create Journal Voucher
    Private Function CreateJournalVoucher(ByVal GPEntry As List(Of WESMBillGPPosted)) As List(Of JournalVoucher)
        Dim JVHeaderList As New List(Of JournalVoucher)
        Dim JVNoCtr As Integer = 1
        For Each item In GPEntry
            Dim JVHeader As New JournalVoucher

            With item
                JVHeader.BatchCode = item.BatchCode
                JVHeader.JVNumber = JVNoCtr
                JVHeader.PostedType = "A"
                JVHeader.Status = 1
            End With

            'JVHeader.JVDetails.Add(New JournalVoucherDetails(JVNoCtr, DebitCode, Math.Abs(item.APamt), 0))
            'JVHeader.JVDetails.Add(New JournalVoucherDetails(JVNoCtr, CreditCode, 0, Math.Abs(item.APamt)))

            JVNoCtr += 1
            JVHeaderList.Add(JVHeader)
        Next


        Return JVHeaderList
    End Function

    'Clear/Refresh Form elements
    Private Sub cmd_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        dgv_ViewSummary.Rows.Clear()
        Me.LoadBPtoCombo()
    End Sub

    'Close form
    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    'Dynamic DatagridView for Summary
    Sub dgv_View_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_ViewSummary.CurrentCellDirtyStateChanged
        If dgv_ViewSummary.IsCurrentCellDirty Then
            dgv_ViewSummary.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub dgv_View_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_ViewSummary.CellValueChanged
        If dgv_ViewSummary.Columns(e.ColumnIndex).Name = "hChkBox" Then
            If CBool(dgv_ViewSummary.Rows(e.RowIndex).Cells("hChkBox").Value) = True Then
                Dim InpBox = InputBox("Enter Adjusted amount: ", "Adjusted Amount")
                Dim adjAmount As Decimal = 0

                'dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").

                If Len(Trim(InpBox)) = 0 Then
                    InpBox = "0.00"
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hChkBox").Value = False
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").Value = adjAmount
                    Exit Sub
                End If

                If IsNumeric(InpBox) = False Then
                    MsgBox("Only Accepts Numeric Inputs", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, MsgBoxStyle), "Error")
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hChkBox").Value = False
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").Value = adjAmount
                    Exit Sub
                End If

                adjAmount = CDec(InpBox)

                If adjAmount = 0 Then
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hChkBox").Value = False
                    dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").Value = adjAmount
                End If

                dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").Value = adjAmount
            Else
                dgv_ViewSummary.Rows(e.RowIndex).Cells("hAmount").Value = 0D
            End If
        End If
    End Sub

    'Dynamic DatagridView for Invoices
    Sub dgv_ViewInvoices_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_viewInvoice.CurrentCellDirtyStateChanged
        If dgv_viewInvoice.IsCurrentCellDirty Then
            dgv_viewInvoice.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub
    Private Sub dgv_ViewInvoices_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_viewInvoice.CellValueChanged
        If dgv_viewInvoice.Columns(e.ColumnIndex).Name = "invAdjust" Then
            If CBool(dgv_viewInvoice.Rows(e.RowIndex).Cells("invAdjust").Value) = True Then
                dgv_viewInvoice.Rows(e.RowIndex).Cells("invAdjustmentAmt").Value = dgv_viewInvoice.Rows(e.RowIndex).Cells("invAmount").Value
            Else
                dgv_viewInvoice.Rows(e.RowIndex).Cells("invAdjustmentAmt").Value = 0D
            End If
        End If
    End Sub

    Private Sub changeStateParticipant() Handles cbo_Participant.SelectedIndexChanged
        _state = "CHANGED"
    End Sub

    Private Sub changeStateBillingPeriod() Handles cbo_BillPeriod.SelectedIndexChanged
        _state = "CHANGED"
    End Sub

    Private Sub changeStateTab() Handles tControl_1.SelectedIndexChanged
        Me.LoadParticipantsToCombo()
    End Sub

    Private Sub PerInvoice(ByVal isVisible As Boolean)
        Me.cbo_destParticipant.Visible = isVisible
        Me.lbl_DestParticipant.Visible = isVisible
    End Sub

    'fill Destination Participant Combo Box
    Private Sub fillDestinationCBO()
        Dim lstParticipant = (From x In AllParticipants _
                              Select x.ParticipantID).ToList
        cbo_destParticipant.DataSource = lstParticipant
    End Sub
#End Region

    Private Sub tControl_1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tControl_1.DrawItem

        'Firstly we'll define some parameters.
        Dim CurrentTab As TabPage = tControl_1.TabPages(e.Index)
        Dim ItemRect As Rectangle = tControl_1.GetTabRect(e.Index)
        Dim FillBrush As New SolidBrush(Color.White)
        Dim TextBrush As New SolidBrush(System.Drawing.Color.FromArgb(CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(105, Byte), Integer)))
        Dim sf As New StringFormat
        sf.Alignment = StringAlignment.Center
        sf.LineAlignment = StringAlignment.Center

        'If we are currently painting the Selected TabItem we'll 
        'change the brush colors and inflate the rectangle.
        If CBool(e.State And DrawItemState.Selected) Then
            FillBrush.Color = Color.White
            TextBrush.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
            ItemRect.Inflate(2, 2)
        End If

        'Set up rotation for left and right aligned tabs
        If tControl_1.Alignment = TabAlignment.Left Or tControl_1.Alignment = TabAlignment.Right Then
            Dim RotateAngle As Single = 90
            If tControl_1.Alignment = TabAlignment.Left Then RotateAngle = 270
            Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
            e.Graphics.TranslateTransform(cp.X, cp.Y)
            e.Graphics.RotateTransform(RotateAngle)
            ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
        End If

        'Next we'll paint the TabItem with our Fill Brush
        e.Graphics.FillRectangle(FillBrush, ItemRect)

        'Now draw the text.
        e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

        'Reset any Graphics rotation
        e.Graphics.ResetTransform()

        'Finally, we should Dispose of our brushes.
        FillBrush.Dispose()
        TextBrush.Dispose()

    End Sub
End Class