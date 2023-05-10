'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmGreatPlainsMonitoringMgt
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 26, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing and Posting WESM Bill Summaries and then Create Journal Voucher
'Arguments/Parameters:  
'Files/Database Tables:  AM_WESM_BILL_GP_POSTED
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description

Option Strict On
Option Explicit On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Public Class frmGreatPlainsMonitoringMgt
    Dim WBillHelper As WESMBillHelper
    Private _State As String
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property

    Public Sub LoadPosting(ByVal ForPosting As WESMBillGPPosted)
        With ForPosting
            xBillPeriod.Text = .BillingPeriod.ToString
            xChargeType.Text = If(.Charge = EnumChargeType.E, "Energy", If(.Charge = EnumChargeType.EV, "VAT on Energy", If(.Charge = EnumChargeType.MF, "Market Fees", "VAT on Market Fees")))
            xDueDate.Text = .DueDate.ToString("MM/dd/yyyy")
            xSTLRun.Text = .SettlementRun.ToString
            xARAmount.Text = .ARamt.ToString("#,##0.#0")
            xAPAmount.Text = .APamt.ToString("#,##0.#0")
            xNSSAmount.Text = .NSSAmount.ToString("#,##0.#0")
            xPosted.Text = If(CDbl(.Posted.ToString) = 0, "Not Posted", "Posted")
            If .GPRefNo = "0" Or .GPRefNo = "" Then
                TXT_GPRefNo.Text = "No GP Reference No. Set"
            Else
                TXT_GPRefNo.Text = .GPRefNo
            End If

            lbl_BatchOffset.Text = ForPosting.BatchCode.ToString

            If CDbl(.Posted.ToString) = 1 Then
                xPosted.ForeColor = Color.Red
            Else
                xPosted.ForeColor = Color.Black
            End If
            txt_Remarks.Text = .Remarks
        End With
    End Sub


    Private Sub frmGreatPlainsMonitoringMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = "JCLP"
        If State = "VIEW" Then
            txt_Remarks.Enabled = False
            cmd_Post.Visible = False
            TXT_GPRefNo.Enabled = False
        Else
            txt_Remarks.Enabled = False
            cmd_Post.Visible = True
            TXT_GPRefNo.Enabled = True
            TXT_GPRefNo.SelectAll()
            TXT_GPRefNo.Focus()
        End If
    End Sub

    Private Sub cmd_Post_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Post.Click
        Dim _UpdateRemarks As New List(Of WESMBillGPPosted)
        Dim UpdateRemarks As New WESMBillGPPosted
        Try

            If Trim(txt_Remarks.Text) = "" Then
                MsgBox("Please enter your Remarks.", MsgBoxStyle.Critical)
            Else
                With UpdateRemarks
                    .BillingPeriod = CInt(xBillPeriod.Text)
                    .Charge = If(xChargeType.Text = "Energy", EnumChargeType.E, If(xChargeType.Text = "VAT on Energy", EnumChargeType.EV, If(xChargeType.Text = "Market Fees", EnumChargeType.MF, EnumChargeType.MFV)))
                    .DueDate = CDate(xDueDate.Text)
                    .SettlementRun = xSTLRun.Text
                    .ARamt = If(xARAmount.Text = "", 0, CDec(xARAmount.Text))
                    .APamt = If(xAPAmount.Text = "", 0, CDec(xAPAmount.Text))
                    .NSSAmount = If(xNSSAmount.Text = "", 0, CDec(xNSSAmount.Text))
                    .Posted = If(CStr(xPosted.Text) = "Posted", 1, 0)
                    .Remarks = txt_Remarks.Text
                    .GPRefNo = TXT_GPRefNo.Text
                    .BatchCode = lbl_BatchOffset.Text.ToString
                    'If frmGreatPlainsMonitoring.rb_Uploaded.Checked Then
                    '    Dim BatchCodes = WBillHelper.GetWESMBillGPPosted()
                    '    Dim ParticipantBatchCode As String
                    '    With UpdateRemarks
                    '        ParticipantBatchCode = (From x In BatchCodes Where x.BillingPeriod = .BillingPeriod And x.SettlementRun = .SettlementRun _
                    '                                     And x.Charge = .Charge And x.OffsetNo = "0" Select x.BatchCode).FirstOrDefault.ToString
                    '    End With
                    '    UpdateRemarks.BatchCode = ParticipantBatchCode
                    'ElseIf frmGreatPlainsMonitoring.rb_Offset.Checked Then
                    '    Dim OffsetNumber = WBillHelper.GetWESMBillGPPosted()
                    '    Dim ParticipantOffsetNo As String
                    '    With UpdateRemarks
                    '        ParticipantOffsetNo = (From x In OffsetNumber Where x.BillingPeriod = .BillingPeriod And x.Charge = .Charge _
                    '                                    Select x.OffsetNo).FirstOrDefault.ToString()
                    '    End With
                    '    UpdateRemarks.OffsetNo = ParticipantOffsetNo
                    'End If
                End With
                _UpdateRemarks.Add(UpdateRemarks)
                WBillHelper.PostToGP(_UpdateRemarks, False)
                MsgBox("Record successfully Updated!", MsgBoxStyle.OkOnly, "Success")
                CloseForm()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub CloseForm()
        Me.Close()
        If UCase(frmGreatPlainsMonitoring.cbo_BillPd.SelectedIndex.ToString) = "0" Then
            frmGreatPlainsMonitoring.LoadRecord()
        Else
            Dim WbillGPPosted = WBillHelper.GetWESMBillGPPosted(CInt(frmGreatPlainsMonitoring.cbo_BillPd.SelectedItem.ToString), If(frmGreatPlainsMonitoring.rb_Open.Checked = True, 2, If(frmGreatPlainsMonitoring.rb_Posted.Checked = True, 1, 0)))
            frmGreatPlainsMonitoring.dgv_summary.Rows.Clear()
            For Each item In WbillGPPosted
                frmGreatPlainsMonitoring.dgv_summary.Rows.Add(item.BillingPeriod, item.SettlementRun, If(item.Charge = EnumChargeType.E, "Energy", If(item.Charge = EnumChargeType.EV, "VAT on Energy", If(item.Charge = EnumChargeType.MF, "Market Fees", "VAT on Market Fees"))), item.ARamt.ToString("#,###.##"), item.APamt.ToString("#,###.##"), item.NSSAmount.ToString("#,###.##"), item.DueDate.ToString("MM/dd/yyyy"), If(item.Posted = 0, "For Posting", "Posted"), item.Remarks)
                If UCase(CStr(frmGreatPlainsMonitoring.dgv_summary.Rows(frmGreatPlainsMonitoring.dgv_summary.Rows.Count - 1).Cells(7).Value)) = "POSTED" Then
                    frmGreatPlainsMonitoring.dgv_summary.Rows(frmGreatPlainsMonitoring.dgv_summary.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Red
                    frmGreatPlainsMonitoring.dgv_summary.Rows(frmGreatPlainsMonitoring.dgv_summary.Rows.Count - 1).DefaultCellStyle.SelectionBackColor = Color.Red
                Else
                    frmGreatPlainsMonitoring.dgv_summary.Rows(frmGreatPlainsMonitoring.dgv_summary.Rows.Count - 1).DefaultCellStyle.BackColor = Color.White
                End If
            Next
            If frmGreatPlainsMonitoring.dgv_summary.Rows.Count > 0 Then
                If UCase(CStr(frmGreatPlainsMonitoring.dgv_summary.Rows(frmGreatPlainsMonitoring.dgv_summary.CurrentRow.Index).Cells(7).Value)) = "POSTED" Then
                    frmGreatPlainsMonitoring.btn_Post.Enabled = False
                    frmGreatPlainsMonitoring.btn_Post.Text = "Already Posted"
                    frmGreatPlainsMonitoring.btn_Post.Text = "Post"
                    frmGreatPlainsMonitoring.btn_Post.Enabled = True
                End If
            ElseIf frmGreatPlainsMonitoring.dgv_summary.Rows.Count = 0 Then
                frmGreatPlainsMonitoring.btn_Post.Enabled = False
            End If
        End If
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub CMD_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Details.Click
        'If frmGreatPlainsMonitoring.rb_Uploaded.Checked Then
        '    Dim BatchCode As String = lbl_BatchOffset.Text.ToString
        '    Dim BillDetails = WBillHelper.GetWESMBills()
        '    Dim forDetails = (From x In BillDetails Where x.BatchCode = BatchCode).ToList
        '    With frmGreatPlainsDetails
        '        frmGreatPlainsDetails.LoadReportUploaded(forDetails)
        '        .ShowDialog()
        '    End With
        'ElseIf frmGreatPlainsMonitoring.rb_Offset.Checked Then
        '    Dim OffsetNo As String = lbl_BatchOffset.Text.ToString
        '    Dim BillDetails = WBillHelper.GetWESMBillOffsetDetails()
        '    Dim forDetails = (From x In BillDetails Where x.OffsetNumber = OffsetNo).ToList
        '    With frmGreatPlainsDetails
        '        .LoadReportOffset(forDetails)
        '        .ShowDialog()
        '    End With
        'End If




        Dim DetailsForm As New frmGreatPlainsDetails
        Select Case frmGreatPlainsMonitoring.cbo_PostType.SelectedItem.ToString.ToUpper
            Case "UPLOADED"
                Dim BatchCode As String = lbl_BatchOffset.Text.ToString
                Dim BillDetails = WBillHelper.GetWESMBills()
                Dim forDetails = (From x In BillDetails Where x.BatchCode = BatchCode).ToList
                With DetailsForm
                    .LoadReportUploaded(forDetails)
                    .ShowDialog()
                End With
            Case "OFFSET"
                Dim OffsetNo As String = lbl_BatchOffset.Text.ToString
                Dim offsetDetails = WBillHelper.GetJVSupportDetails(OffsetNo)
                Dim getInvoices = WBillHelper.GetWESMBills()
                Dim getName = WBillHelper.GetAMParticipantsAll()
                Dim Datedue As New Date
                For Each item In offsetDetails
                    Dim cParticipant = item.IDNumber
                    Dim cInvoice = item.InvDMCMNo
                    Dim GetInvoiceDetails = (From x In getInvoices _
                                                 Where x.InvoiceNumber = cInvoice _
                                                 Select x).FirstOrDefault
                    If item.InvDMCMNo <> 0 Then
                        Datedue = GetInvoiceDetails.DueDate
                        item.InvoiceDate = GetInvoiceDetails.InvoiceDate
                    End If
                    item.DueDate = Datedue

                    item.ParticipantName = (From x In getName _
                                           Where x.IDNumber = cParticipant _
                                           Select x.ParticipantID).FirstOrDefault
                Next

                With DetailsForm
                    .LoadReportOffset(offsetDetails, xBillPeriod.Text, lbl_BatchOffset.Text, xChargeType.Text)
                    .ShowDialog()
                End With
            Case "COLLECTION"
                Dim CollBatchcode As String = lbl_BatchOffset.Text.ToString
                Dim BillDetails = WBillHelper.GetCollectionAllocation()
                Dim forDetails = (From x In BillDetails Where x.BatchCode = CollBatchcode).ToList
                With DetailsForm
                    .LoadReportCollection(forDetails)
                    .ShowDialog()
                End With
            Case "PAYMENT"
                Dim CollBatchcode As String = lbl_BatchOffset.Text.ToString
                Dim BillDetails = WBillHelper.GetPaymentAllocationParticipant()
                Dim forDetails = (From x In BillDetails Where x.PaymentBatchCode = CollBatchcode).ToList
                With DetailsForm
                    .LoadReportPayment(forDetails)
                    .ShowDialog()
                End With

            Case "ADJUSTMENT"
                Dim Batch As String = lbl_BatchOffset.Text.ToString
                Dim GetSummary = WBillHelper.GetAdjustedDetails(Batch)
                Dim BillPd = (From x In GetSummary Select x.BillPeriod).FirstOrDefault
                With DetailsForm
                    .LoadReportAdjustment(GetSummary, BillPd, Batch)
                    .ShowDialog()
                End With

        End Select




        'cbo_PostType.Items.Add("Uploaded")
        'cbo_PostType.Items.Add("Offset")
        'cbo_PostType.Items.Add("Collection")
        'cbo_PostType.Items.Add("Payment")
        'cbo_PostType.Items.Add("Adjustment")
    End Sub
End Class