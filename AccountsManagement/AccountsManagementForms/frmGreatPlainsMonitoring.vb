'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGreatPlainsMonitoring
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 16, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing and Posting WESM Bill Summaries and then Create Journal Voucher
'Arguments/Parameters:  
'Files/Database Tables:  AM_JV, AM_JV_DETAILS, AM_WESM_BILL_GP_POSTED
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   September 26, 2011      Juan Carlo L. Panopio               Date Format in Viewing is chaged to (MM/dd/yyyy)
'   September 26, 2011      Juan Carlo L. Panopio               Currency Format in viewing is chaged to (##,###.##)
'   September 26, 2011      Juan Carlo L. Panopio               Added the Updating and Viewing of Remarks
'   October   04, 2011      Juan Carlo L. Panopio               Added Generation of Journal Voucher

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmGreatPlainsMonitoring
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean

    Private Sub frmGreatPlainsMonitoring_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "Carlo"
        Dim cboBillingPd = WBillHelper.GetBillPdGP()
        cbo_BillPd.Items.Add("View All")
        cbo_BillPd.SelectedIndex = 0
        Dim CalendarBP = WBillHelper.GetCalendarBP()
        Dim ForCBOView = (From x In cboBillingPd Join y In CalendarBP On x Equals y.BillingPeriod _
                          Select y.BillingPeriod, y.StartDate, y.EndDate Order By BillingPeriod Descending)
        
        cbo_PostType.Items.Add(EnumPostType.Uploaded.ToString)
        cbo_PostType.Items.Add(EnumPostType.Offset.ToString)
        cbo_PostType.Items.Add(EnumPostType.Collection.ToString)
        cbo_PostType.Items.Add(EnumPostType.Payment.ToString)
        cbo_PostType.Items.Add(EnumPostType.Adjustment.ToString)
        'cbo_PostType.Items.Add(EnumPostType.EarnedInterest.ToString)

        If cbo_PostType.Items.Count > 0 Then
            cbo_PostType.SelectedIndex = 0
        End If

        Me.ChangeBillPdDDL()

        If cbo_BillPd.Items.Count > 0 Then
            cbo_BillPd.SelectedIndex = 0
        Else
            cbo_BillPd.Items.Add("View All")
            cbo_BillPd.SelectedIndex = 0
        End If

        rb_Open.Checked = True
        Me.Admin = False
        Me.LoadRecord()
        Me.ResizeRedraw = True
    End Sub

    Public Sub LoadRecord()

        Try
            Dim WBillGPPosted = WBillHelper.GetWESMBillGPPosted()
            Dim lstJVHeader = WBillHelper.GetJournalVoucher()
            If rb_Open.Checked Then
                WBillGPPosted = (From x In WBillGPPosted _
                                 Where x.Posted = 0 _
                                 Select x).ToList
            ElseIf rb_Posted.Checked Then
                WBillGPPosted = (From x In WBillGPPosted _
                                 Where x.Posted = 1 _
                                 Select x).ToList
            End If
            Select Case UCase(cbo_PostType.SelectedItem.ToString)
                Case "UPLOADED"
                    WBillGPPosted = (From x In WBillGPPosted Select x Where x.PostType = "U").ToList
                    Me.colControl(True, True, True)
                Case "OFFSET"
                    WBillGPPosted = (From x In WBillGPPosted Select x Where x.PostType = "O").ToList
                    Me.colControl(False, False, True)
                Case "COLLECTION"
                    WBillGPPosted = (From x In WBillGPPosted Select x Where x.PostType = "C").ToList
                    Me.colControl(False, False, False)
                Case "PAYMENT"
                    WBillGPPosted = (From x In WBillGPPosted Select x Where x.PostType = "P").ToList
                    Me.colControl(False, False, False)
                Case "ADJUSTMENT"
                    WBillGPPosted = (From x In WBillGPPosted Select x Where x.PostType = "A").ToList
                    Me.colControl(False, False, True)
            End Select
            dgv_summary.Rows.Clear()
            For Each item In WBillGPPosted

                If item.APamt <> 0 Or item.ARamt <> 0 Then
                    Dim cBatchCode = item.BatchCode
                    Dim cJVNo = (From x In lstJVHeader _
                                 Where x.BatchCode = cBatchCode _
                                 Select x.JVNumber).FirstOrDefault
                    dgv_summary.Rows.Add(item.BillingPeriod, item.SettlementRun, cJVNo, item.GPRefNo.ToString, item.BatchCode.ToString, 0, If(item.Charge = EnumChargeType.E, "Energy", If(item.Charge = EnumChargeType.EV, "VAT on Energy", If(item.Charge = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), If(item.APamt <> 0, item.APamt.ToString("#,###.##"), "0"), If(item.ARamt <> 0, _
                                        item.ARamt.ToString("#,###.##"), "0"), If(item.NSSAmount <> 0, item.NSSAmount.ToString("#,###.##"), "0"), _
                                        item.DueDate.ToString("MM/dd/yyyy"), If(item.Posted = 0, "For Posting", "Posted"), item.Remarks)
                    If UCase(CStr(dgv_summary.Rows(dgv_summary.Rows.Count - 1).Cells("Status").Value)) <> "POSTED" Then
                        dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Red
                        dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.White
                    Else
                        dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.Black
                    End If
                Else
                    Continue For
                End If
            Next

            If dgv_summary.Rows.Count > 0 Then
                If UCase(CStr(dgv_summary.Rows(dgv_summary.CurrentRow.Index).Cells("Status").Value)) = "POSTED" Then
                    btn_Post.Enabled = False
                    btn_Post.Text = "Already Posted"
                Else
                    btn_Post.Text = "Post"
                    btn_Post.Enabled = True
                End If
            ElseIf dgv_summary.Rows.Count = 0 Then
                btn_Post.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btn_TSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TSearch.Click
        Try
            If UCase(cbo_BillPd.SelectedIndex.ToString) = "0" Then
                Me.LoadRecord()
            Else
                Dim BillPeriod = CDbl(cbo_BillPd.SelectedItem.ToString)
                Dim WbillGPPosted = WBillHelper.GetWESMBillGPPosted()
                WbillGPPosted = (From x In WbillGPPosted _
                                 Where x.BillingPeriod = BillPeriod _
                                 Select x).ToList
                If rb_Open.Checked Then
                    WbillGPPosted = (From x In WbillGPPosted _
                                     Where x.Posted = 0 _
                                     Select x).ToList
                ElseIf rb_Posted.Checked Then
                    WbillGPPosted = (From x In WbillGPPosted _
                                     Where x.Posted = 1 _
                                     Select x).ToList
                End If
                Dim lstJVHeader = WBillHelper.GetJournalVoucher()
                dgv_summary.Rows.Clear()

                Select Case UCase(cbo_PostType.SelectedItem.ToString)
                    Case "UPLOADED"
                        WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "U").ToList
                        Me.colControl(True, True, True)
                    Case "OFFSET"
                        WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "O").ToList
                        Me.colControl(False, False, True)
                    Case "COLLECTION"
                        WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "C").ToList
                        Me.colControl(False, False, False)
                    Case "PAYMENT"
                        WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "P").ToList
                        Me.colControl(False, False, False)
                    Case "ADJUSTMENT"
                        WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "A").ToList
                        Me.colControl(False, False, True)
                End Select

                For Each item In WbillGPPosted
                    If item.APamt <> 0 Or item.ARamt <> 0 Then
                        Dim cBatchCode = item.BatchCode
                        Dim cJVNo = (From x In lstJVHeader _
                                     Where x.BatchCode = cBatchCode _
                                     Select x.JVNumber).FirstOrDefault
                        dgv_summary.Rows.Add(item.BillingPeriod, item.SettlementRun, cJVNo, item.GPRefNo.ToString, item.BatchCode.ToString, 0, _
                        If(item.Charge = EnumChargeType.E, "Energy", If(item.Charge = EnumChargeType.EV, "VAT on Energy", If(item.Charge = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), _
                        If(item.APamt <> 0, item.APamt.ToString("#,##0.00"), "0"), _
                        If(item.ARamt <> 0, item.ARamt.ToString("#,##0.00"), "0"), _
                        If(item.NSSAmount <> 0, item.NSSAmount.ToString("#,##0.00"), "0"), _
                        item.DueDate.ToString("MM/dd/yyyy"), If(item.Posted = 0, "For Posting", "Posted"), item.Remarks, _
                        item.GPRefNo.ToString)
                        If UCase(CStr(dgv_summary.Rows(dgv_summary.Rows.Count - 1).Cells("Status").Value)) <> "POSTED" Then
                            dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Red
                            dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.White
                        Else
                            dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.Black
                        End If
                    End If
                Next
                If dgv_summary.Rows.Count > 0 Then
                    If UCase(CStr(dgv_summary.Rows(dgv_summary.CurrentRow.Index).Cells("Status").Value)) = "POSTED" Then
                        btn_Post.Enabled = False
                        btn_Post.Text = "Already Posted"
                    Else
                        btn_Post.Text = "Post"
                        btn_Post.Enabled = True
                    End If
                ElseIf dgv_summary.Rows.Count = 0 Then
                    btn_Post.Enabled = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.LoadRecord()
    End Sub

    Private Sub btn_Post_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Post.Click
        Dim ToPost As New List(Of WESMBillGPPosted)
        Dim ans As MsgBoxResult

        Try
            ans = MsgBox("Do you want to post the selected record/s?", MsgBoxStyle.YesNo, "Post Record")

            If ans = MsgBoxResult.Yes Then
                If dgv_summary.SelectedRows.Count = 1 Then
                    Dim WESMBillGP As New WESMBillGPPosted
                    With dgv_summary.Rows(dgv_summary.CurrentRow.Index)
                        .Selected = True
                        WESMBillGP.BillingPeriod = CInt(.Cells("BillPd").Value.ToString)
                        WESMBillGP.SettlementRun = .Cells("STLRun").Value.ToString
                        WESMBillGP.Charge = If(.Cells("ChargeType").Value.ToString = "Energy", EnumChargeType.E, If(.Cells("ChargeType").Value.ToString = "VAT on Energy", EnumChargeType.EV, If(.Cells("ChargeType").Value.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                        WESMBillGP.ARamt = CDec(.Cells("APAmt").Value)
                        WESMBillGP.APamt = CDec(.Cells("ArAMT").Value)
                        WESMBillGP.NSSAmount = CDec(.Cells("NSSAmt").Value)
                        WESMBillGP.DueDate = CDate(.Cells("DUEDATE").Value)
                        WESMBillGP.Remarks = CStr(.Cells("Remarks").Value)
                        WESMBillGP.GPRefNo = CStr(.Cells("GPRefNo").Value)
                        WESMBillGP.PostType = CStr(.Cells("hPostType").Value)
                        WESMBillGP.BatchCode = CStr(.Cells("BatchNumber").Value)

                    End With
                    ToPost.Add(WESMBillGP)
                    WBillHelper.PostToGP(ToPost, True)
                ElseIf dgv_summary.SelectedRows.Count > 1 Then
                    For RowCtr = 0 To dgv_summary.SelectedRows.Count - 1
                        Dim WESMBillGP As New WESMBillGPPosted
                        With dgv_summary.SelectedRows.Item(RowCtr)
                            WESMBillGP.BillingPeriod = CInt(.Cells("BillPd").Value.ToString)
                            WESMBillGP.SettlementRun = .Cells("STLRun").Value.ToString
                            WESMBillGP.Charge = If(.Cells("ChargeType").Value.ToString = "Energy", EnumChargeType.E, If(.Cells("ChargeType").Value.ToString = "VAT on Energy", EnumChargeType.EV, If(.Cells("ChargeType").Value.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                            WESMBillGP.ARamt = CDec(.Cells("APAmt").Value)
                            WESMBillGP.APamt = CDec(.Cells("ArAMT").Value)
                            WESMBillGP.NSSAmount = CDec(.Cells("NSSAmt").Value)
                            WESMBillGP.DueDate = CDate(.Cells("DUEDATE").Value)
                            WESMBillGP.Remarks = CStr(.Cells("Remarks").Value)
                            WESMBillGP.GPRefNo = CStr(.Cells("GPRefNo").Value)
                            WESMBillGP.PostType = CStr(.Cells("hPostType").Value)
                            WESMBillGP.BatchCode = CStr(.Cells("BatchNumber").Value)
                        End With
                        ToPost.Add(WESMBillGP)
                    Next

                    Dim CheckPosted = (From x In ToPost Where x.Posted = 1).Count

                    If CheckPosted = 0 Then
                        WBillHelper.PostToGP(ToPost, True)
                    Else
                        MsgBox("Some records are already Posted", MsgBoxStyle.Critical, "For Posting in GP")
                        Exit Sub
                    End If

                End If
                MsgBox("Record Successfully Posted!", MsgBoxStyle.OkOnly, "Update Success")
                If cbo_BillPd.SelectedIndex = 0 Then
                    Me.LoadRecord()
                Else
                    Dim BillPeriod = cbo_BillPd.SelectedItem.ToString
                    Dim WbillGPPosted = WBillHelper.GetWESMBillGPPosted(CInt(BillPeriod), If(rb_Open.Checked = True, 2, If(rb_Posted.Checked = True, 1, 0)))
                    Dim lstJVHeader = WBillHelper.GetJournalVoucher()
                    If rb_Open.Checked Then
                        WbillGPPosted = (From x In WbillGPPosted _
                                         Where x.Posted = 0 _
                                         Select x).ToList
                    ElseIf rb_Posted.Checked Then
                        WbillGPPosted = (From x In WbillGPPosted _
                                         Where x.Posted = 1 _
                                         Select x).ToList
                    End If
                    dgv_summary.Rows.Clear()
                    Select Case UCase(cbo_PostType.SelectedItem.ToString)
                        Case "UPLOADED"
                            WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "U").ToList
                            Me.colControl(True, True, True)
                        Case "OFFSET"
                            WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "O").ToList
                            Me.colControl(False, False, True)
                        Case "COLLECTION"
                            WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "C").ToList
                            Me.colControl(False, False, False)
                        Case "PAYMENT"
                            WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "P").ToList
                            Me.colControl(False, False, False)
                        Case "ADJUSTMENT"
                            WbillGPPosted = (From x In WbillGPPosted Select x Where x.PostType = "A").ToList
                            Me.colControl(False, False, True)
                    End Select
                    For Each item In WbillGPPosted
                        If item.APamt <> 0 Or item.ARamt <> 0 Then
                            Dim cBatchCode = item.BatchCode
                            Dim cJVNo = (From x In lstJVHeader _
                                         Where x.BatchCode = cBatchCode _
                                         Select x.JVNumber).FirstOrDefault
                            dgv_summary.Rows.Add(item.BillingPeriod, item.SettlementRun, cJVNo, item.GPRefNo.ToString, item.BatchCode, 0, _
                            If(item.Charge = EnumChargeType.E, "Energy", If(item.Charge = EnumChargeType.EV, "VAT on Energy", If(item.Charge = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), _
                            If(item.APamt <> 0, item.APamt.ToString("#,##0.00"), "0"), _
                            If(item.ARamt <> 0, item.ARamt.ToString("#,##0.00"), "0"), _
                            item.NSSAmount.ToString("#,##0.00"), item.DueDate.ToString("MM/dd/yyyy"), If(item.Posted = 0, "For Posting", "Posted"), item.Remarks, item.GPRefNo)
                            If UCase(CStr(dgv_summary.Rows(dgv_summary.Rows.Count - 1).Cells("Status").Value)) <> "POSTED" Then
                                dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Red
                                dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.White
                            Else
                                dgv_summary.Rows(dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionForeColor = Color.Black
                            End If
                        End If
                    Next
                    If dgv_summary.Rows.Count > 0 Then
                        If UCase(CStr(dgv_summary.Rows(dgv_summary.CurrentRow.Index).Cells("Status").Value)) = "POSTED" Then
                            btn_Post.Enabled = False
                            btn_Post.Text = "Already Posted"
                        Else
                            btn_Post.Text = "Post"
                            btn_Post.Enabled = True
                        End If
                    ElseIf dgv_summary.Rows.Count = 0 Then
                        btn_Post.Enabled = False
                    End If
                End If
            Else
                MsgBox("Posting Cancelled", MsgBoxStyle.Critical, "Transaction Cancelled")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btn_Remarks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Remarks.Click
        Dim ToPost As New WESMBillGPPosted

        Try
            If dgv_summary.SelectedRows.Count = 1 Then
                With dgv_summary.Rows(dgv_summary.CurrentRow.Index)
                    .Selected = True
                    ToPost.BillingPeriod = CInt(.Cells("BillPd").Value.ToString)
                    ToPost.SettlementRun = .Cells("STLRun").Value.ToString
                    ToPost.Charge = If(.Cells("ChargeType").Value.ToString = "Energy", EnumChargeType.E, If(.Cells("ChargeType").Value.ToString = "VAT on Energy", EnumChargeType.EV, If(.Cells("ChargeType").Value.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                    ToPost.ARamt = CDec(.Cells("APAmt").Value)
                    ToPost.APamt = CDec(.Cells("ArAMT").Value)
                    ToPost.NSSAmount = CDec(.Cells("NSSAmt").Value)
                    ToPost.DueDate = CDate(.Cells("DUEDATE").Value)
                    ToPost.Remarks = CStr(.Cells("Remarks").Value)
                    ToPost.GPRefNo = CStr(.Cells("GPRefNo").Value)
                    ToPost.PostType = CStr(.Cells("hPostType").Value)
                    ToPost.BatchCode = CStr(.Cells("BatchNumber").Value)

                End With
                Dim a As New frmGreatPlainsMonitoringMgt
                With a
                    .State = "UPDATE"
                    .LoadPosting(ToPost)
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub
    Private Sub dgv_summary_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_summary.CellDoubleClick
        Dim ToPost As New WESMBillGPPosted
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        If dgv_summary.SelectedRows.Count = 1 Then
            With dgv_summary.Rows(dgv_summary.CurrentRow.Index)
                .Selected = True
                ToPost.BillingPeriod = CInt(.Cells("BillPd").Value.ToString)
                ToPost.SettlementRun = .Cells("STLRun").Value.ToString
                ToPost.Charge = If(.Cells("ChargeType").Value.ToString = "Energy", EnumChargeType.E, If(.Cells("ChargeType").Value.ToString = "VAT on Energy", EnumChargeType.EV, If(.Cells("ChargeType").Value.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                ToPost.ARamt = CDec(.Cells("ARAmt").Value)
                ToPost.APamt = CDec(.Cells("APAMT").Value)
                ToPost.NSSAmount = CDec(.Cells("NSSAmt").Value)
                ToPost.DueDate = CDate(.Cells("DUEDATE").Value)
                ToPost.Remarks = CStr(.Cells("Remarks").Value)
                ToPost.GPRefNo = CStr(.Cells("GPRefNo").Value)
                ToPost.PostType = CStr(.Cells("hPostType").Value)
                ToPost.BatchCode = CStr(.Cells("BatchNumber").Value)
            End With
            With frmGreatPlainsMonitoringMgt
                .LoadPosting(ToPost)
                .State = "VIEW"
                .ShowDialog()
            End With
        End If
    End Sub
    Private Sub dgv_summary_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv_summary.CellMouseUp
        If dgv_summary.Rows.Count > 0 Then
            If UCase(CStr(dgv_summary.Rows(dgv_summary.CurrentRow.Index).Cells("Status").Value)) = "POSTED" Then
                btn_Post.Text = "Already Posted"
                btn_Post.Enabled = False
            Else
                btn_Post.Text = "Post"
                btn_Post.Enabled = True
            End If
        End If
    End Sub

    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub cmd_GenerateReport_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        If dgv_summary.SelectedRows.Count = 1 Then
            'Load all WESMBill GP Posted Records
            Dim AllWESMBillGP = WBillHelper.GetWESMBillGPPosted()
            Dim _SignatoriesJV = WBillHelper.GetSignatories("JV").First
            Dim Accountingcode = WBillHelper.GetAccountingCodes()
            Accountingcode = (From x In Accountingcode Where x.Status = 1 Select x Order By x.AccountCode Ascending).ToList

            'Load all Journal Voucher Headers/Details
            Dim AllJV = WBillHelper.GetJournalVoucher()
            Dim AllJVDetails = WBillHelper.GetJournalVoucherDetails()

            'Get Batch Code/Offset No
            Dim ForFiltering As New WESMBillGPPosted
            If dgv_summary.SelectedRows.Count = 1 Then
                With dgv_summary.Rows(dgv_summary.CurrentRow.Index)
                    ForFiltering.SettlementRun = CStr(.Cells("STLRun").Value.ToString)
                    'ForFiltering.OffsetNo = CStr(.Cells("Offsetno").Value.ToString)
                    ForFiltering.BatchCode = CStr(.Cells("BatchNumber").Value.ToString)
                    ForFiltering.Charge = If(.Cells("ChargeType").Value.ToString = "Energy", EnumChargeType.E, If(.Cells("ChargeType").Value.ToString = "VAT on Energy", EnumChargeType.EV, If(.Cells("ChargeType").Value.ToString = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                End With
            End If

            ''Generate Report
            'If ForFiltering.SettlementRun = "" Then

            '    'Offseting in Summary
            '    Dim JVNum = (From x In AllJV Where x.OffsetNumber = ForFiltering.OffsetNo And x.Status = 1 Select x.JVNumber).FirstOrDefault.ToString
            '    If JVNum.Count = 0 Then
            '        MsgBox("Invalid Data Found")
            '        Exit Sub
            '    End If
            '    Dim JVHeader = (From x In AllJV Where x.OffsetNumber = ForFiltering.OffsetNo And x.Status = 1 Select x)
            '    Dim JVDetails = (From x In AllJVDetails Where x.JVNumber = CDbl(JVNum) Select x)

            '    Dim JournalVoucherTable As New DataTable
            '    JournalVoucherTable.TableName = "JournalVoucher"
            '    With JournalVoucherTable.Columns
            '        .Add("JV_NO", GetType(Long))
            '        .Add("JV_DATE", GetType(Date))
            '        .Add("BATCHCODE", GetType(String))
            '        .Add("OFFSETNUMBER", GetType(String))
            '        .Add("STATUS", GetType(Integer))
            '        .Add("PREPAREDBY", GetType(String))
            '        .Add("CHECKEDBY", GetType(String))
            '        .Add("APPROVEDBY", GetType(String))
            '        .Add("UPDATEDBY", GetType(String))
            '        .Add("UPDATEDDATE", GetType(Date))
            '        .Add("REMARKS", GetType(String))
            '    End With

            '    For Each item In JVHeader
            '        Dim dr As DataRow
            '        dr = JournalVoucherTable.NewRow
            '        With item
            '            dr("JV_NO") = .JVNumber
            '            dr("JV_DATE") = .JVDate
            '            dr("BATCHCODE") = .BatchCode.ToString
            '            dr("OFFSETNUMBER") = .OffsetNumber.ToString
            '            dr("STATUS") = .Status
            '            dr("PREPAREDBY") = _SignatoriesJV.PreparedBy
            '            dr("CHECKEDBY") = _SignatoriesJV.CheckedBy
            '            dr("APPROVEDBY") = _SignatoriesJV.ApprovedBy
            '            dr("UPDATEDBY") = .UpdatedBy
            '            dr("UPDATEDDATE") = .UpdatedDate
            '            Dim GetRemarks = (From x In AllWESMBillGP Where x.OffsetNo = .OffsetNumber Select x.Remarks).FirstOrDefault.ToString
            '            dr("REMARKS") = GetRemarks.ToString
            '        End With
            '        JournalVoucherTable.Rows.Add(dr)
            '    Next

            '    Dim JournalVoucherDetailsTable As New DataTable
            '    JournalVoucherDetailsTable.TableName = "JournalVoucherDetails"
            '    With JournalVoucherDetailsTable.Columns
            '        .Add("JV_NO", GetType(Long))
            '        .Add("ACCOUNTCODE", GetType(String))
            '        .Add("CREDIT", GetType(Decimal))
            '        .Add("DEBIT", GetType(Decimal))
            '        .Add("UPDATEDBY", GetType(String))
            '        .Add("UPDATEDDATE", GetType(Date))
            '    End With

            '    For Each rowJVDetails In JVDetails
            '        Dim jvRow As DataRow
            '        jvRow = JournalVoucherDetailsTable.NewRow
            '        With rowJVDetails
            '            jvRow("JV_NO") = .JVNumber
            '            jvRow("ACCOUNTCODE") = .AccountCode
            '            jvRow("CREDIT") = .Credit
            '            jvRow("DEBIT") = .Debit
            '            jvRow("UPDATEDBY") = WBillHelper.UserName '.UpdatedBy
            '            jvRow("UPDATEDDATE") = .UpdatedDate
            '        End With
            '        JournalVoucherDetailsTable.Rows.Add(jvRow)
            '    Next

            '    Dim AccountCodesTable As New DataTable
            '    AccountCodesTable.TableName = "AccountingCode"
            '    With AccountCodesTable.Columns
            '        .Add("ACCT_CODE", GetType(String))
            '        .Add("DESCRIPTION", GetType(String))
            '    End With

            '    For Each acctCodes In Accountingcode
            '        Dim acRow As DataRow
            '        acRow = AccountCodesTable.NewRow
            '        With acRow
            '            acRow("ACCT_CODE") = acctCodes.AccountCode
            '            acRow("DESCRIPTION") = acctCodes.Description
            '        End With
            '        AccountCodesTable.Rows.Add(acRow)
            '    Next
            '    Dim DS As New DataSet
            '    DS.Tables.Add(JournalVoucherTable)
            '    DS.Tables.Add(JournalVoucherDetailsTable)
            '    DS.Tables.Add(AccountCodesTable)
            '    frmProgress.Show()
            '    Dim frmReport As New frmReportViewer
            '    With frmReport
            '        .LoadJournalVoucher(DS)
            '        .ShowDialog()
            '    End With
            'Else

            'Non Offset Summary
            Dim JVNum = (From x In AllJV Where x.BatchCode = ForFiltering.BatchCode And x.Status = 1 Select x.JVNumber).FirstOrDefault.ToString
            If JVNum.Count = 0 Then
                MsgBox("Invalid Data Found")
                Exit Sub
            End If
            Dim JVHeader = (From x In AllJV Where x.BatchCode = ForFiltering.BatchCode And x.Status = 1 Select x)
            Dim JVDetails = (From x In AllJVDetails Where x.JVNumber = CDbl(JVNum) Select x)

            Dim JournalVoucherTable As New DataTable
            JournalVoucherTable.TableName = "JournalVoucher"
            With JournalVoucherTable.Columns
                .Add("JV_NO", GetType(Long))
                .Add("JV_DATE", GetType(Date))
                .Add("BATCHCODE", GetType(String))
                .Add("OFFSETNUMBER", GetType(String))
                .Add("STATUS", GetType(Integer))
                .Add("PREPAREDBY", GetType(String))
                .Add("CHECKEDBY", GetType(String))
                .Add("APPROVEDBY", GetType(String))
                .Add("UPDATEDBY", GetType(String))
                .Add("UPDATEDDATE", GetType(Date))
                .Add("REMARKS", GetType(String))
                .Add("GPREF_NO", GetType(String))
            End With

            For Each item In JVHeader
                Dim dr As DataRow
                dr = JournalVoucherTable.NewRow
                With item
                    dr("JV_NO") = .JVNumber
                    dr("JV_DATE") = .JVDate
                    dr("BATCHCODE") = .BatchCode.ToString
                    dr("OFFSETNUMBER") = .OffsetNumber.ToString
                    dr("STATUS") = .Status
                    dr("PREPAREDBY") = _SignatoriesJV.PreparedBy
                    dr("CHECKEDBY") = _SignatoriesJV.CheckedBy
                    dr("APPROVEDBY") = _SignatoriesJV.ApprovedBy
                    dr("UPDATEDBY") = .UpdatedBy
                    dr("UPDATEDDATE") = .UpdatedDate
                    dr("GPREF_NO") = dgv_summary.Rows(dgv_summary.CurrentRow.Index).Cells("GPRefNo").Value.ToString
                    Dim GetRemarks = (From x In AllWESMBillGP Where x.BatchCode = .BatchCode.ToString Select x.Remarks).FirstOrDefault.ToString
                    dr("REMARKS") = GetRemarks.ToString
                End With
                JournalVoucherTable.Rows.Add(dr)
            Next

            Dim JournalVoucherDetailsTable As New DataTable
            JournalVoucherDetailsTable.TableName = "JournalVoucherDetails"
            With JournalVoucherDetailsTable.Columns
                .Add("JV_NO", GetType(Long))
                .Add("ACCOUNTCODE", GetType(String))
                .Add("CREDIT", GetType(Decimal))
                .Add("DEBIT", GetType(Decimal))
                .Add("UPDATEDBY", GetType(String))
                .Add("UPDATEDDATE", GetType(Date))
            End With

            For Each rowJVDetails In JVDetails
                Dim jvRow As DataRow
                jvRow = JournalVoucherDetailsTable.NewRow
                With rowJVDetails
                    jvRow("JV_NO") = .JVNumber
                    jvRow("ACCOUNTCODE") = .AccountCode
                    jvRow("CREDIT") = Math.Abs(.Credit)
                    jvRow("DEBIT") = Math.Abs(.Debit)
                    jvRow("UPDATEDBY") = .UpdatedBy
                    jvRow("UPDATEDDATE") = .UpdatedDate
                End With
                JournalVoucherDetailsTable.Rows.Add(jvRow)
            Next

            Dim AccountCodesTable As New DataTable
            AccountCodesTable.TableName = "AccountingCode"
            With AccountCodesTable.Columns
                .Add("ACCT_CODE", GetType(String))
                .Add("DESCRIPTION", GetType(String))
            End With

            For Each acctCodes In Accountingcode
                Dim acRow As DataRow
                acRow = AccountCodesTable.NewRow
                With acRow
                    acRow("ACCT_CODE") = acctCodes.AccountCode
                    acRow("DESCRIPTION") = acctCodes.Description
                End With
                AccountCodesTable.Rows.Add(acRow)
            Next

            Dim DS As New DataSet
            DS.Tables.Add(JournalVoucherTable)
            DS.Tables.Add(JournalVoucherDetailsTable)
            DS.Tables.Add(AccountCodesTable)
            frmProgress.Show()

            Dim frmReport As New frmReportViewer
            With frmReport
                .LoadJournalVoucher(DS)
                .ShowDialog()
            End With
            'End If
        Else
        MsgBox("Please choose only one record when Generating Journal Voucher", MsgBoxStyle.Critical)
        End If
    End Sub



    Private Sub ChangeBillPdDDL() Handles cbo_PostType.SelectedValueChanged

        Dim WBillGPList = WBillHelper.GetWESMBillGPPosted()
        Select Case UCase(cbo_PostType.SelectedItem.ToString)
            Case "UPLOADED"
                'Get Billing Period 
                Dim uFilter = (From x In WBillGPList _
                               Where x.PostType = "U" _
                               Select x.BillingPeriod Distinct).ToList
                Me.lbl_BillPd.Text = "Bill Period:"
                cbo_BillPd.Items.Clear()
                cbo_BillPd.Items.Add("View All")
                For Each item In uFilter
                    cbo_BillPd.Items.Add(item)
                Next

            Case "OFFSETTING"
                Dim uFilter = (From x In WBillGPList _
                               Where x.PostType = "O" _
                               Select x.BillingPeriod Distinct).ToList
                Me.lbl_BillPd.Text = "Bill Period:"
                cbo_BillPd.Items.Clear()
                cbo_BillPd.Items.Add("View All")
                For Each item In uFilter
                    cbo_BillPd.Items.Add(item)
                Next

            Case "COLLECTION"
                Dim uFilter = (From x In WBillGPList _
                               Where x.PostType = "C" _
                               Select x.BatchCode Distinct).ToList
                Me.lbl_BillPd.Text = "Batch Code:"
                cbo_BillPd.Items.Clear()
                cbo_BillPd.Items.Add("View All")
                For Each item In uFilter
                    cbo_BillPd.Items.Add(item)
                Next

            Case "PAYMENT"
                Dim uFilter = (From x In WBillGPList _
                               Where x.PostType = "P" _
                               Select x.BatchCode Distinct).ToList
                Me.lbl_BillPd.Text = "Batch Code:"
                cbo_BillPd.Items.Clear()
                cbo_BillPd.Items.Add("View All")
                For Each item In uFilter
                    cbo_BillPd.Items.Add(item)
                Next

            Case "ADJUSTMENT"
                Dim uFilter = (From x In WBillGPList _
                               Where x.PostType = "A" _
                               Select x.BillingPeriod Distinct).ToList
                Me.lbl_BillPd.Text = "Bill Period:"
                cbo_BillPd.Items.Clear()
                cbo_BillPd.Items.Add("View All")
                For Each item In uFilter
                    cbo_BillPd.Items.Add(item)
                Next
        End Select
        
        If cbo_BillPd.Items.Count > 0 Then
            cbo_BillPd.SelectedIndex = 0
        Else
            cbo_BillPd.Items.Add("View All")
            cbo_BillPd.SelectedIndex = 0
        End If

    End Sub


    Private Sub colControl(ByVal colSTLRun As Boolean, ByVal colNSSAmt As Boolean, ByVal BillPeriod As Boolean)
        dgv_summary.Columns("STLRun").Visible = colSTLRun
        dgv_summary.Columns("NSSAmt").Visible = colNSSAmt
        dgv_summary.Columns("BillPd").Visible = BillPeriod
    End Sub

    Private Sub ChangeBillPdDDL(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_PostType.SelectedValueChanged

    End Sub

    Private Sub dgv_summary_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_summary.CellContentClick

    End Sub
End Class
