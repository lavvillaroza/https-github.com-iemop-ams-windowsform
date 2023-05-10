'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmWESMBillSummary
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 19, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for the WESM Bills Summary and it's details if any
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_SUMMARY, AM_OFFSET_P2PC2C_DETAILS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   September 19, 2011      Juan Carlo L. Panopio           Finished form GUI and functionalities but no
'                                                           Generation of Report yet.(Viewing Only)
'   September 20, 2011      Juan Carlo L. Panopio           Completed Filtering for WESM Bill Summaries
'   September 21, 2011      Juan Carlo L. Panopio           Removed AM Code in Viewing
'   September 21, 2011      Juan Carlo L. Panopio           Completed Summary Report and Summary with Details Report

Option Strict On
Option Explicit On

Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmWESMBillSummary
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean
    Private _SearchState As Integer
    Private Property SearchState() As Integer
        Get
            Return _SearchState
        End Get
        Set(ByVal value As Integer)
            Me._SearchState = value
        End Set
    End Property
    Private Sub frmWESMBillSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Me.Admin = False
        Me.ResizeRedraw = True
        cbox_All.Checked = True
        Try
            Dim InitSummary = WBillHelper.GetWESMBillSummary()
            Dim BillingPeriod = (From x In InitSummary Select x.BillPeriod Distinct Order By BillPeriod Descending).ToList()
            Dim ParticipantIDs = WBillHelper.GetAMParticipantsAll()
            Dim FilterParticipant = (From y In ParticipantIDs Select y.ParticipantID).ToList
            cbo_BillPeriod.DataSource = (From x In InitSummary Select x.BillPeriod Distinct Order By BillPeriod Descending).ToList()
            FilterParticipant.Insert(0, ("View All"))
            cbo_IDNumber.DataSource = FilterParticipant
            cb_ShowDetails.Checked = True
            'Me.rb_ViewAll.Checked = True
            cmd_generateReport.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Close.Click
        Me.Close()
    End Sub
    Private Sub cbox_All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_All.CheckedChanged
        If cbox_All.Checked Then
            cbox_E.Checked = True
            cbox_MF.Checked = True
            cbox_EV.Checked = True
            cbox_MFV.Checked = True
        End If
    End Sub

    Private Sub cbox_E_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_E.CheckedChanged
        If cbox_All.Checked Then
            If Not cbox_E.Checked Then
                cbox_All.Checked = False
            End If
        ElseIf cbox_E.Checked And cbox_EV.Checked And cbox_MF.Checked And cbox_MFV.Checked Then
            cbox_All.Checked = True
        End If
    End Sub

    Private Sub cbox_EV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_EV.CheckedChanged
        If cbox_All.Checked Then
            If Not cbox_EV.Checked Then
                cbox_All.Checked = False
            End If
        ElseIf cbox_E.Checked And cbox_EV.Checked And cbox_MF.Checked And cbox_MFV.Checked Then
            cbox_All.Checked = True
        End If
    End Sub

    Private Sub cbox_MFV_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_MFV.CheckedChanged
        If cbox_All.Checked Then
            If Not cbox_MFV.Checked Then
                cbox_All.Checked = False
            End If
        ElseIf cbox_E.Checked And cbox_EV.Checked And cbox_MF.Checked And cbox_MFV.Checked Then
            cbox_All.Checked = True
        End If
    End Sub

    Private Sub cbox_MF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_MF.CheckedChanged
        If cbox_All.Checked Then
            If Not cbox_MF.Checked Then
                cbox_All.Checked = False
            End If
        ElseIf cbox_E.Checked And cbox_EV.Checked And cbox_MF.Checked And cbox_MFV.Checked Then
            cbox_All.Checked = True
        End If
    End Sub

    Private Sub cmd_Search_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        SearchState = 0
        Dim WithOffset As Boolean = False

        Try
            Dim InitSummary = WBillHelper.GetWESMBillSummary()
            InitSummary = (From x In InitSummary Select x Order By x.ChargeType Ascending).ToList

            If cbo_IDNumber.SelectedIndex = -1 Then
                MsgBox("Invalid Participant ID, please try again!", MsgBoxStyle.Critical, "Error Enoucntered")
                Exit Sub
            End If

            If Not cbox_All.Checked And Not cbox_E.Checked And Not cbox_EV.Checked _
                And Not cbox_MF.Checked And Not cbox_MFV.Checked Then
                MsgBox("Please select Charge Type", MsgBoxStyle.Exclamation, "Error!")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim FillSummary = (From x In InitSummary Where x.BillPeriod = CInt(Me.cbo_BillPeriod.SelectedItem.ToString) _
                                Select x).ToList
            Dim ChargeFilter As String = ""
            If cbo_DueDate.SelectedIndex <> 0 Then
                FillSummary = (From x In FillSummary Where x.DueDate = CDate(cbo_DueDate.SelectedItem.ToString)).ToList
            End If

            If cbo_IDNumber.SelectedIndex <> 0 Then
                FillSummary = (From x In FillSummary Where x.IDNumber.ParticipantID = Me.cbo_IDNumber.SelectedItem.ToString).ToList
            End If

            If Not cbox_All.Checked Then
                If cbox_E.Checked Then
                    ChargeFilter &= "E,"
                End If
                If cbox_EV.Checked Then
                    ChargeFilter &= "EV,"
                End If
                If cbox_MF.Checked Then
                    ChargeFilter &= "MF,"
                End If
                If cbox_MFV.Checked Then
                    ChargeFilter &= "MFV,"
                End If

                If Trim(ChargeFilter) <> "" Then
                    ChargeFilter = Trim(Mid(ChargeFilter, 1, Len(ChargeFilter) - 1))
                    Dim ChargeTypeConditions() As String = Split(ChargeFilter, ",")
                    Dim FilterCharge = WBillHelper.GetWESMBillSummary(ChargeTypeConditions)

                    FillSummary = (From x In FilterCharge Where x.BillPeriod = CInt(Me.cbo_BillPeriod.SelectedItem.ToString) _
                                    Select x).ToList

                    If cbo_IDNumber.SelectedIndex <> 0 Then
                        FillSummary = (From x In FillSummary Where x.IDNumber.ParticipantID = Me.cbo_IDNumber.SelectedItem.ToString).ToList
                    End If

                    If cbo_DueDate.SelectedIndex <> 0 Then
                        FillSummary = (From x In FillSummary Where x.DueDate = CDate(cbo_DueDate.SelectedItem.ToString)).ToList
                    End If
                End If
            End If

            'If rb_WithOffset.Checked Then
            '    Dim GetDetails = WBillHelper.GetWESMBillOffsetDetails()
            '    FillSummary = (From x In FillSummary Join y In GetDetails On x.GroupNo Equals y.GroupNo Where y.DMCMNumber <> 0 _
            '                   Select x Distinct).ToList
            '    WithOffset = True
            'End If

            Grid_Summary.Rows.Clear()

            For Each item In FillSummary
                With Grid_Summary.Rows
                    .Add(item.GroupNo, IIf(item.SummaryType = EnumSummaryType.DMCM, "DebitCredit Memo", "Invoice"), item.INVDMCMNo, _
                        IIf(item.IDType = "P", "Parent", "Child"), item.IDNumber.ParticipantID, item.BillPeriod, If(item.ChargeType = EnumChargeType.E, "Energy", _
                        If(item.ChargeType = EnumChargeType.EV, "VAT on Energy", If(item.ChargeType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), _
                        item.DueDate, _
                        FormatNumber(item.BeginningBalance, 2, TriState.True, TriState.True), _
                        FormatNumber(item.EndingBalance, 2, TriState.True, TriState.True), _
                        item.WESMBillSummaryNo, IIf(item.Adjustment = 1, "Yes", "No"))
                End With
            Next
            Me.Cursor = Cursors.Default
            If Grid_Summary.Rows.Count = 0 Then
                MsgBox("No Records found!", MsgBoxStyle.Critical)
                cmd_generateReport.Enabled = False
                cmd_ViewDetails.Enabled = False
                'Exit Sub
            Else
                cmd_ViewDetails.Enabled = True
                Dim SearchDetails = WBillHelper.GetWESMBillOffsetDetails(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells(0).Value.ToString, WithOffset)
                Dim getWESMBills = WBillHelper.GetWESMBills()
                Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
                Dim SelectedParticipant = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("IDNumber").Value.ToString

                Dim ParticipantNumber = CDbl((From x In AllParticipants Where x.ParticipantID = SelectedParticipant Select x.IDNumber).FirstOrDefault.ToString)

                If SearchDetails.Count = 0 Then
                    SearchDetails = WBillHelper.GetWESMBillOffsetDetails()
                    SearchDetails = (From x In SearchDetails Where x.WESMBillSummaryNo = CDbl(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells(0).Value.ToString)).ToList
                End If
                cmd_generateReport.Enabled = True

            End If
            If Grid_Summary.Rows.Count <> 0 Then
                Grid_Summary.Rows(0).Selected = True
            End If

            'Compute AP/AR/NSS
            txtAPEnergyMF.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.MF) And x.BeginningBalance > 0 _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)
            txtAREnergyMF.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.MF) And x.BeginningBalance < 0 _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)
            txtNSSEnergyMF.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.MF) _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)

            txtAPVat.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.EV Or x.ChargeType = EnumChargeType.MFV) And x.BeginningBalance > 0 _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)
            txtARVat.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.EV Or x.ChargeType = EnumChargeType.MFV) And x.BeginningBalance < 0 _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)
            txtNSSVat.Text = FormatNumber((From x In FillSummary Where x.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                  And (x.ChargeType = EnumChargeType.EV Or x.ChargeType = EnumChargeType.MFV) _
                                  Select x.BeginningBalance).Sum, 2, TriState.True, TriState.True)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_clear.Click
        If cbo_BillPeriod.Items.Count > 0 Then
            cbo_BillPeriod.SelectedIndex = 0
        End If
        If cbo_DueDate.Items.Count > 0 Then
            cbo_DueDate.SelectedIndex = 0
        End If
        If cbo_IDNumber.Items.Count > 0 Then
            cbo_IDNumber.SelectedIndex = 0
        End If
        cbox_All.Checked = True
        cmd_ViewDetails.Enabled = False
        cmd_generateReport.Enabled = False
        Me.txt_SearchNo.Text = "Enter Invoice/DMCM No Here"
        Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
        Me.txt_SearchNo.ForeColor = Color.Gray
        Grid_Summary.Rows.Clear()
    End Sub

    Private Sub Grid_Summary_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Grid_Summary.CellMouseClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim AllOffset As Boolean = False
            'If rb_ViewAll.Checked = True Then
            '    AllOffset = False
            'Else
            '    AllOffset = True
            'End If

            Dim SearchDetails = WBillHelper.GetWESMBillOffsetDetails(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells(0).Value.ToString, AllOffset)
            Dim getWESMBills = WBillHelper.GetWESMBills()
            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            Dim SelectedParticipant = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("IDNumber").Value.ToString

            Dim ParticipantNumber = CDbl((From x In AllParticipants Where x.ParticipantID = SelectedParticipant Select x.IDNumber).FirstOrDefault.ToString)

            If SearchDetails.Count = 0 Then
                SearchDetails = WBillHelper.GetWESMBillOffsetDetails()
                SearchDetails = (From x In SearchDetails Where x.WESMBillSummaryNo = CDbl(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells(0).Value.ToString)).ToList
            End If

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_ParticipantReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ParticipantReport.Click
        Dim Charges As String = ""
        Dim Participants As String = ""
        Dim DueDate As String = ""
        Dim dt As New DataTable
        Dim BillPd As Integer

        Try
            BillPd = CInt(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("BillPeriod").Value.ToString)
            Charges = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("ChargeType").Value.ToString
            Participants = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("IDNumber").Value.ToString
            DueDate = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("DueDate").Value.ToString
            Dim GroupNumber As Double = CDbl(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("GroupNo").Value.ToString)

            Dim gOffsetDetails = WBillHelper.GetWESMBillOffsetDetails()
            Dim forDetailsReport = (From x In gOffsetDetails Where x.WESMBillSummaryNo = GroupNumber Select x).ToList

            dt.TableName = "AM_WESM_BILL_SUMMARY_DETAILS"
            With dt.Columns
                .Add("TransNumber", GetType(String))
                .Add("TransDate", GetType(Date))
                .Add("InvNumber", GetType(String))
                .Add("Debit", GetType(Decimal))
                .Add("Credit", GetType(Decimal))
            End With

            For Each item In forDetailsReport
                Dim row As DataRow
                row = dt.NewRow
                row("TransNumber") = item.WESMBillSummaryNo
                row("TransDate") = item.TransDate
                row("InvNumber") = item.InvoiceNumber
                row("Debit") = item.Debit
                row("Credit") = item.Credit
                dt.Rows.Add(row)
            Next

            frmProgress.Show()
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadWESMBillReportSummaryDetails(dt, CStr(BillPd), Charges, Participants, DueDate)
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cbo_BillPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_BillPeriod.SelectedIndexChanged
        Dim InitSummary = WBillHelper.GetWESMBillSummary()
        Dim FillDuedate = (From x In InitSummary Where x.BillPeriod = CInt(Me.cbo_BillPeriod.Text) Select CStr(x.DueDate) Distinct).ToList()
        FillDuedate.Insert(0, ("View All"))
        cbo_DueDate.DataSource = FillDuedate
        cbo_DueDate.SelectedIndex = 0
        SearchState = 1
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_generateReport.Click
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim Charges As String = ""
        Dim participants As String = ""
        Dim AllParticipants = WBillHelper.GetAMParticipantsAll()

        If SearchState = 1 Then
            MsgBox("Filter Values have changed, please click Search first", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Grid_Summary.RowCount = 0 Then
            MsgBox("No records found", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If Not cbox_E.Checked And Not cbox_EV.Checked And Not cbox_MF.Checked And Not cbox_MFV.Checked Then
            MsgBox("Please Select Charge Types", MsgBoxStyle.Critical)
            Exit Sub
        End If

        If cbox_E.Checked Then
            Charges &= "ENERGY,"
        End If
        If cbox_EV.Checked Then
            Charges &= "VAT ON ENERGY,"
        End If
        If cbox_MF.Checked Then
            Charges &= "MARKET FEES,"
        End If
        If cbox_MFV.Checked Then
            Charges &= "VAT ON MARKET FEES,"
        End If

        Charges = Trim(Mid(Charges, 1, (Len(Charges) - 1)))
        Dim DateDue = Grid_Summary.Rows(Grid_Summary.RowCount - 1).Cells("DUEDATE").Value.ToString
        If cbo_IDNumber.SelectedItem.ToString = "View All" Then
            participants = "All Participants"
        Else
            participants = cbo_IDNumber.SelectedItem.ToString
        End If
        Try
            dt.TableName = "WESMBILLSUMMARYONLY"
            With dt.Columns
                .Add("PARTICIPANT_ID", GetType(String))
                .Add("BILLING_PERIOD", GetType(Integer))
                .Add("CHARGE_TYPE", GetType(String))
                .Add("DUE_DATE", GetType(Date))
                .Add("BEGINNING_BALANCE", GetType(Decimal))
                .Add("ENDING_BALANCE", GetType(Decimal))
                .Add("SUMMARY_NO", GetType(Long))
                .Add("INVDMCM_NO", GetType(Long))
                .Add("SUMMARY_TYPE", GetType(String))
            End With

            For CTR = 1 To CInt(Grid_Summary.Rows.Count)
                Dim row As DataRow
                row = dt.NewRow
                row("PARTICIPANT_ID") = Grid_Summary.Rows(CTR - 1).Cells("IDNUMBER").Value.ToString
                row("BILLING_PERIOD") = Grid_Summary.Rows(CTR - 1).Cells("BILLPERIOD").Value.ToString
                row("CHARGE_TYPE") = Grid_Summary.Rows(CTR - 1).Cells("CHARGETYPE").Value.ToString
                row("DUE_DATE") = Grid_Summary.Rows(CTR - 1).Cells("DUEDATE").Value.ToString
                row("BEGINNING_BALANCE") = CDec(FormatNumber(Grid_Summary.Rows(CTR - 1).Cells("BEGINNINGBALANCE").Value.ToString, 2, TriState.True, TriState.False, TriState.True))
                row("ENDING_BALANCE") = CDec(FormatNumber(Grid_Summary.Rows(CTR - 1).Cells("ENDINGBALANCE").Value.ToString, 2, TriState.True, TriState.False, TriState.True))
                row("SUMMARY_NO") = Grid_Summary.Rows(CTR - 1).Cells("SUMMARYNO").Value.ToString
                row("INVDMCM_NO") = Grid_Summary.Rows(CTR - 1).Cells("hINVDMCMNo").Value.ToString
                row("SUMMARY_TYPE") = IIf(UCase(Grid_Summary.Rows(CTR - 1).Cells("hSummaryType").Value.ToString) = "INVOICE", "INVOICE", "DM/CM")
                dt.Rows.Add(row)
            Next
            ds.Tables.Add(dt)

            Dim dtDetails As New DataTable
            dtDetails.TableName = "WESMBILLSUMMARYDETAILS"
            With dtDetails.Columns
                .Add("PARTICIPANT_ID", GetType(String))
                .Add("BILLING_PERIOD", GetType(Integer))
                .Add("CHARGE_TYPE", GetType(String))
                .Add("DUE_DATE", GetType(Date))
                .Add("BEGINNING_BALANCE", GetType(Decimal))
                .Add("ENDING_BALANCE", GetType(Decimal))
                .Add("SUMMARY_NO", GetType(Long))
                .Add("INVDMCM_NO", GetType(Long))
                .Add("SUMMARY_TYPE", GetType(String))
            End With

            Dim WESMInvoices = WBillHelper.GetWESMBills()
            Dim DMCMDetails = WBillHelper.GetDebitCreditMemoDetails()
            Dim OffsetDetails = WBillHelper.GetWESMBillOffsetDetails()

            Dim dicChargeType As New Dictionary(Of String, EnumChargeType)
            dicChargeType.Add("ENERGY", EnumChargeType.E)
            dicChargeType.Add("VAT ON ENERGY", EnumChargeType.EV)
            dicChargeType.Add("MARKET FEES", EnumChargeType.MF)
            dicChargeType.Add("VAT ON MARKET FEES", EnumChargeType.MFV)

            If cb_ShowDetails.Checked Then

                For CTR = 1 To CInt(Grid_Summary.Rows.Count)
                    Dim GroupNumber = Grid_Summary.Rows(CTR - 1).Cells("GroupNo").Value.ToString

                    If CDbl(GroupNumber) = 0 Then
                        Continue For
                    End If

                    Dim SummaryType = UCase(Grid_Summary.Rows(CTR - 1).Cells("hSummaryType").Value.ToString)
                    Dim INVDMCM = Grid_Summary.Rows(CTR - 1).Cells("hINVDMCMNo").Value.ToString
                    Dim ChargeType As EnumChargeType = dicChargeType(UCase(Grid_Summary.Rows(CTR - 1).Cells("ChargeType").Value.ToString))
                    Dim lstDetailsInvoices = Me.GetDetailsOfInvoices(WESMInvoices, DMCMDetails, _
                                                              OffsetDetails, CLng(GroupNumber), SummaryType, CLng(INVDMCM), _
                                                              ChargeType)

                    For Each item In lstDetailsInvoices
                        Dim dRow As DataRow
                        Dim cParticipant = item.IDNumber
                        dRow = dtDetails.NewRow
                        dRow("PARTICIPANT_ID") = (From x In AllParticipants Where x.IDNumber = CStr(cParticipant) Select x.ParticipantID).FirstOrDefault
                        dRow("BILLING_PERIOD") = item.BillingPeriod
                        dRow("CHARGE_TYPE") = IIf(item.ChargeType = EnumChargeType.E, "ENERGY", IIf(item.ChargeType = EnumChargeType.EV, "VAT ON ENERGY", IIf(item.ChargeType = EnumChargeType.MF, "MARKET FEES", "VAT ON MARKET FEES")))
                        dRow("DUE_DATE") = item.DueDate
                        dRow("BEGINNING_BALANCE") = item.Amount
                        dRow("ENDING_BALANCE") = item.Amount
                        dRow("SUMMARY_NO") = Grid_Summary.Rows(CTR - 1).Cells("SUMMARYNO").Value.ToString
                        dRow("INVDMCM_NO") = item.InvoiceNumber
                        dRow("SUMMARY_TYPE") = "INVOICE"
                        dtDetails.Rows.Add(dRow)
                    Next
                Next

            Else
                For CTR = 1 To CInt(Grid_Summary.Rows.Count)
                    Dim dRow As DataRow
                    dRow = dtDetails.NewRow
                    dRow("PARTICIPANT_ID") = Grid_Summary.Rows(CTR - 1).Cells("IDNUMBER").Value.ToString
                    dRow("BILLING_PERIOD") = Grid_Summary.Rows(CTR - 1).Cells("BILLPERIOD").Value.ToString
                    dRow("CHARGE_TYPE") = Grid_Summary.Rows(CTR - 1).Cells("CHARGETYPE").Value.ToString
                    dRow("DUE_DATE") = Grid_Summary.Rows(CTR - 1).Cells("DUEDATE").Value.ToString
                    dRow("BEGINNING_BALANCE") = CDec(Grid_Summary.Rows(CTR - 1).Cells("BEGINNINGBALANCE").Value.ToString)
                    dRow("ENDING_BALANCE") = CDec(Grid_Summary.Rows(CTR - 1).Cells("ENDINGBALANCE").Value.ToString)
                    dRow("SUMMARY_NO") = Grid_Summary.Rows(CTR - 1).Cells("SUMMARYNO").Value.ToString
                    dRow("INVDMCM_NO") = Grid_Summary.Rows(CTR - 1).Cells("hINVDMCMNo").Value.ToString
                    dRow("SUMMARY_TYPE") = IIf(UCase(Grid_Summary.Rows(CTR - 1).Cells("hSummaryType").Value.ToString) = "INVOICE", "INVOICE", "DM/CM")
                    dtDetails.Rows.Add(dRow)
                Next
            End If
            If dtDetails.Rows.Count = 0 Then
            Else
                ds.Tables.Add(dtDetails)
            End If

            frmProgress.Show()
            Dim frmViewer As New frmReportViewer()

            With frmViewer
                .LoadWESMBillReportSummary(ds, Charges, participants, DateDue)
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cbox_All_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbox_All.Click
        If Not cbox_All.Checked Then
            cbox_E.Checked = False
            cbox_MF.Checked = False
            cbox_EV.Checked = False
            cbox_MFV.Checked = False
        End If
    End Sub

    Private Sub cbo_DueDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_DueDate.SelectedIndexChanged
        SearchState = 1
    End Sub

    Private Sub cbo_IDNumber_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_IDNumber.SelectedIndexChanged
        SearchState = 1
    End Sub

    Private Function ToDatatable(ByVal InvoiceDetails As List(Of WESMBill)) As DataTable
        Dim dt As New DataTable
        Dim AllParticipants = WBillHelper.GetAMParticipantsAll()

        dt.TableName = "WESMBills"
        With dt.Columns
            .Add("BillingPeriod")
            .Add("ParticipantID")
            .Add("ParticipantName")
            .Add("InvoiceNumber")
            .Add("InvoiceDate")
            .Add("DueDate")
            .Add("Amount")
        End With

        For Each item In InvoiceDetails
            Dim dr As DataRow
            dr = dt.NewRow
            Dim cParticipant = item.IDNumber
            Dim ParticipantName = (From x In AllParticipants _
                                   Where x.IDNumber = CStr(cParticipant) _
                                   Select x.ParticipantID).FirstOrDefault

            With item
                dr("BillingPeriod") = .BillingPeriod
                dr("ParticipantID") = .IDNumber
                dr("ParticipantName") = ParticipantName
                dr("InvoiceNumber") = .InvoiceNumber
                dr("InvoiceDate") = .InvoiceDate.ToString("M/dd/yyyy")
                dr("DueDate") = .DueDate.ToString("M/dd/yyyy")
                dr("Amount") = FormatNumber(.Amount, 2, TriState.True, TriState.True)
            End With

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        Return dt
    End Function

    Private Sub cmd_ViewDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewDetails.Click
        Try

        
            Dim AllOffset As Boolean = False
            'If rb_ViewAll.Checked = True Then
            '    AllOffset = False
            'Else
            '    AllOffset = True
            'End If

            Dim LstInvDetails As New List(Of WESMBill)
            Dim GetInvoiceNos As New List(Of Long)

            'initialize values
            Dim WESMInvoices = WBillHelper.GetWESMBills()
            Dim DMCMDetails = WBillHelper.GetDebitCreditMemoDetails()
            Dim OffsetDetails = WBillHelper.GetWESMBillOffsetDetails()
            Dim GroupNumber = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("GroupNo").Value.ToString
            Dim SummaryType = UCase(Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("hSummaryType").Value.ToString)
            Dim INVDMCM = Grid_Summary.Rows(Grid_Summary.CurrentRow.Index).Cells("hINVDMCMNo").Value.ToString

            Dim dicChargeType As New Dictionary(Of String, EnumChargeType)
            dicChargeType.Add("ENERGY", EnumChargeType.E)
            dicChargeType.Add("VAT ON ENERGY", EnumChargeType.EV)
            dicChargeType.Add("MARKET FEES", EnumChargeType.MF)
            dicChargeType.Add("VAT ON MARKET FEES", EnumChargeType.MFV)

            Dim ChargeType As EnumChargeType = dicChargeType(UCase(Grid_Summary.CurrentRow.Cells("ChargeType").Value.ToString))

            LstInvDetails = Me.GetDetailsOfInvoices(WESMInvoices, DMCMDetails, OffsetDetails, CLng(GroupNumber), _
                                                    SummaryType, CLng(INVDMCM), ChargeType)

            Dim ToDetails = Me.ToDatatable(LstInvDetails)
            With frmViewDetails
                .ShowSummaryDetails(ToDetails)
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Refresh.Click
        Try
            Dim InitSummary = WBillHelper.GetWESMBillSummary()
            Dim BillingPeriod = (From x In InitSummary Select x.BillPeriod Distinct Order By BillPeriod Descending).ToList()
            Dim ParticipantIDs = WBillHelper.GetAMParticipantsAll()
            Dim FilterParticipant = (From y In ParticipantIDs Select y.ParticipantID).ToList
            cbo_BillPeriod.DataSource = (From x In InitSummary Select x.BillPeriod Distinct Order By BillPeriod Descending).ToList()
            FilterParticipant.Insert(0, ("View All"))
            cbo_IDNumber.DataSource = FilterParticipant
            cb_ShowDetails.Checked = True
            'Me.rb_ViewAll.Checked = True
            cmd_generateReport.Enabled = False
            cmd_ViewDetails.Enabled = False
            Me.SearchState = 1
            Me.txt_SearchNo.Text = "Enter Invoice/DMCM No Here"
            Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
            Me.txt_SearchNo.ForeColor = Color.Gray
            Me.Grid_Summary.Rows.Clear()
            Me.txtAPEnergyMF.Text = "0.00"
            Me.txtAPVat.Text = "0.00"
            Me.txtAREnergyMF.Text = "0.00"
            Me.txtARVat.Text = "0.00"
            Me.txtNSSEnergyMF.Text = "0.00"
            Me.txtNSSVat.Text = "0.00"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Function GetDetailsOfInvoices(ByVal lstWESMInvoices As List(Of WESMBill), ByVal lstDMCMDetails As List(Of DebitCreditMemoDetails), _
                                ByVal lstOffsetDetails As List(Of OffsetP2PC2CDetails), ByVal GroupNumber As Long, _
                                ByVal SummaryType As String, ByVal InvDMCM As Long, ByVal ChargeType As EnumChargeType) As List(Of WESMBill)
        Dim retLstLong As New List(Of WESMBill)
        Dim InvNumbers As List(Of Long)

        If SummaryType = "INVOICE" Then
            Dim offsetDetails = WBillHelper.GetWESMBillOffsetDetails()
            Dim SearchDetails = (From x In offsetDetails _
                                 Where x.WESMBillSummaryNo = CDbl(GroupNumber) _
                                 And x.InvoiceNumber = CDbl(InvDMCM) _
                                 Select x.DMCMNumber).FirstOrDefault
            InvNumbers = (From x In lstDMCMDetails _
                                 Where x.DMCMNumber = SearchDetails _
                                 Select x.InvDMCMNo).ToList

            'if DMCM Number in AM_OFFSET_P2PC2C_DETAILS is Zero
            'If the invoice is not offset to any other invoice
            If SearchDetails = 0 Then
                InvNumbers.Add(CLng(InvDMCM))
            End If

            If lstWESMInvoices.Count <> 0 Then
                For Each item In InvNumbers
                    Dim invDetails As New WESMBill
                    Dim _invNo = CDbl(item.ToString)
                    invDetails = (From x In lstWESMInvoices _
                                  Where x.InvoiceNumber = _invNo _
                                  And x.ChargeType = ChargeType _
                                  Select x).FirstOrDefault
                    retLstLong.Add(invDetails)
                Next
            Else
                Throw New ApplicationException("No Available details for the selected WESM Bill")
            End If
        Else
            'For DMCM Summary Type
            InvNumbers = (From x In lstDMCMDetails _
                                 Where x.DMCMNumber = CLng(InvDMCM) _
                                 And x.InvDMCMNo <> 0 _
                                 Select x.InvDMCMNo Distinct).ToList

            For Each item In InvNumbers
                Dim invDetails As New WESMBill
                Dim _invNo = CDbl(item.ToString)
                Dim INVAmount As Decimal = 0
                Dim getAmountFromDMCM = (From x In lstDMCMDetails _
                                     Where x.DMCMNumber = CLng(InvDMCM) And _
                                     x.InvDMCMNo = _invNo _
                                     Select x).FirstOrDefault

                invDetails = (From x In lstWESMInvoices _
                              Where x.InvoiceNumber = _invNo _
                              And x.ChargeType = ChargeType _
                              Select x).FirstOrDefault
                If getAmountFromDMCM.Debit = 0 Then
                    If getAmountFromDMCM.AccountCode = DebitCode Then
                        INVAmount = getAmountFromDMCM.Credit
                    Else
                        INVAmount = getAmountFromDMCM.Credit * -1D
                    End If
                Else
                    If getAmountFromDMCM.AccountCode = DebitCode Then
                        INVAmount = getAmountFromDMCM.Debit
                    Else
                        INVAmount = getAmountFromDMCM.Debit * -1D
                    End If
                End If

                With invDetails
                    .Amount = CDec(FormatNumber(INVAmount, 2, TriState.True, TriState.True))
                End With

                retLstLong.Add(invDetails)
            Next
        End If

        Return retLstLong
    End Function

    Private Sub btnSearchDMCM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDMCM.Click
        If Me.txt_SearchNo.Text = "Enter Invoice/DMCM No Here" Then
            MsgBox("Please enter a valid Number on the search field.", MsgBoxStyle.Critical, "Error Encountered")
            Exit Sub
        Else

            Dim _lstSearch As New List(Of Long)
            If IsNumeric(Me.txt_SearchNo.Text) Then
                _lstSearch.Add(CLng(Me.txt_SearchNo.Text))
            Else
                MsgBox("Please enter a valid Invoice/DMCM Number", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If

            Dim _lstSummary As New List(Of WESMBillSummary)
            _lstSummary = Me.WBillHelper.GetWESMBillSummary(_lstSearch, True)

            If rb_DMCM.Checked Then
                _lstSummary = (From x In _lstSummary _
                               Where x.SummaryType = EnumSummaryType.DMCM _
                               Select x).ToList
            Else
                _lstSummary = (From x In _lstSummary _
                               Where x.SummaryType = EnumSummaryType.INV _
                               Select x).ToList
            End If

            If _lstSummary.Count = 0 Then
                MsgBox("No Records found!", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If
            SearchState = 0
            Me.Grid_Summary.Rows.Clear()
            For Each item In _lstSummary
                With Grid_Summary.Rows
                    .Add(item.GroupNo, IIf(item.SummaryType = EnumSummaryType.DMCM, "DebitCredit Memo", "Invoice"), item.INVDMCMNo, _
                        IIf(item.IDType = "P", "Parent", "Child"), item.IDNumber.ParticipantID, item.BillPeriod, If(item.ChargeType = EnumChargeType.E, "Energy", _
                        If(item.ChargeType = EnumChargeType.EV, "VAT on Energy", If(item.ChargeType = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), _
                        item.DueDate, _
                        FormatNumber(item.BeginningBalance, 2, TriState.True, TriState.True), _
                        FormatNumber(item.EndingBalance, 2, TriState.True, TriState.True), _
                        item.WESMBillSummaryNo, IIf(item.Adjustment = 1, "Yes", "No"))
                End With
            Next

            Me.cmd_generateReport.Enabled = True
            Me.cmd_ViewDetails.Enabled = True

            End If
    End Sub

    Private Sub txt_SearchNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SearchNo.GotFocus
        Me.txt_SearchNo.Text = ""
        Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Regular)
        Me.txt_SearchNo.ForeColor = Color.Black
    End Sub

    Private Sub txt_SearchNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SearchNo.LostFocus
        If Me.txt_SearchNo.Text.Trim.Length = 0 Then
            Me.txt_SearchNo.Text = "Enter Invoice/DMCM No Here"
            Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Italic)
            Me.txt_SearchNo.ForeColor = Color.Gray
        End If
    End Sub
End Class