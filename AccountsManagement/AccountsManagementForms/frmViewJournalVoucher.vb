'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmViewJournalVoucher
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     October 07, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing of Current and Previously generated Journal Vouchers
'Arguments/Parameters:  
'Files/Database Tables:  AM_JV, AM_JV_DETAILS, AM_ACCOUNTING_CODE
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description



Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmViewJournalVoucher
    Private WBillHelper As WESMBillHelper
    Private bFactory As BusinessFactory
    Private _SearchState As Integer
    Private Property SearchState() As Integer
        Get
            Return _SearchState
        End Get
        Set(ByVal value As Integer)
            Me._SearchState = value
        End Set
    End Property
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm        
        WBillHelper = WESMBillHelper.GetInstance()
        bFactory = BusinessFactory.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Dim AllJV = WBillHelper.GetJournalVoucher()
        cbo_JVNO.Items.Add("View All")
        For Each item In AllJV
            cbo_JVNO.Items.Add(item.JVNumber)
        Next
        cbo_JVNO.SelectedIndex = 0
        cmd_GenerateJV.Enabled = False
    End Sub

    Private Sub cmd_search_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_search.Click
        SearchState = 0
        Try           
            If Me.cbox_Date.Checked = False And Me.cbox_No.Checked = False Then
                MsgBox("Please select a filter then try again.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If CDate(FormatDateTime(dtp_from.Value, DateFormat.ShortDate)) > CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)) Then
                MsgBox("FROM date should not be greater than the TO date", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If


            'If Me.cbox_Date.Checked Then


            '    'MsgBox(CDate(FormatDateTime(dtp_from.Value, DateFormat.ShortDate)))
            '    'MsgBox(CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)))

            '    AllJVHeader = (From x In AllJVHeader Where _
            '                   x.JVDate >= CDate(FormatDateTime(dtp_from.Value, DateFormat.ShortDate)) _
            '                   And x.JVDate <= CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)) _
            '                   Select x).ToList
            'End If

            Dim AllJVHeader = WBillHelper.GetJournalVoucher(CDate(FormatDateTime(dtp_from.Value, DateFormat.ShortDate)), CDate(FormatDateTime(dtp_to.Value, DateFormat.ShortDate)))
            Dim AllGPPost = WBillHelper.GetWESMBillGPPosted()

            If Me.cbox_No.Checked Then
                If Not IsNumeric(Trim(cbo_JVNO.Text.ToString)) Then
                    If Not UCase(Trim(cbo_JVNO.Text.ToString)) = "VIEW ALL" Then
                        MsgBox("Please enter numeric values only in Journal Voucher Number", MsgBoxStyle.Critical, "Error!")
                        Exit Sub
                    End If
                Else
                    AllJVHeader = (From x In AllJVHeader Where _
                                   x.JVNumber = CDbl(Me.cbo_JVNO.Text.ToString) _
                                   Select x).ToList
                End If
            End If

            dgv_VoucherHeader.Rows.Clear()
            dgv_VoucherHeader.Columns.Clear()

            dgv_JournalDetails.Rows.Clear()
            dgv_JournalDetails.Columns.Clear()

            If AllJVHeader.Count = 0 Then
                MsgBox("No records found.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Search Journal Voucher")
                Exit Sub
            End If

            With dgv_VoucherHeader.Columns
                .Add("JVNumber", "Journal Voucher Number")
                .Add("JVGPRef", "GP Ref No")
                .Add("JVDate", "Journal Voucher Date")
                .Add("JVStatus", "Status")
                .Add("JVPrepared", "Prepared by")
                .Add("JVPrepared", "Checked by")
                .Add("JVApproved", "Approved by")
                .Add("JVUpdated", "Updated by")
                .Add("JVUpdated", "Updated Date")
            End With

            dgv_VoucherHeader.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            If UCase(cbo_JVNO.Text) = UCase("View All") Then
                For Each JVItems In AllJVHeader
                    Dim _JVItems = JVItems
                    Dim GPRef = (From x In AllGPPost _
                               Where x.BatchCode = _JVItems.BatchCode _
                               Select x.GPRefNo).FirstOrDefault
                    JVItems.GPRefNo = GPRef
                    dgv_VoucherHeader.Rows.Add(Me.bFactory.GenerateBIRDocumentNumber(JVItems.JVNumber, BIRDocumentsType.JournalVoucher), JVItems.GPRefNo, FormatDateTime(JVItems.JVDate, DateFormat.ShortDate), If(JVItems.Status = 1, "Active", "In-Active"), JVItems.PreparedBy _
                                                , JVItems.CheckedBy, JVItems.ApprovedBy, JVItems.UpdatedBy, FormatDateTime(JVItems.UpdatedDate, DateFormat.ShortDate))
                Next
            Else
                For Each JVItems In AllJVHeader
                    dgv_VoucherHeader.Rows.Add(Me.bFactory.GenerateBIRDocumentNumber(JVItems.JVNumber, BIRDocumentsType.JournalVoucher), JVItems.GPRefNo, FormatDateTime(JVItems.JVDate, DateFormat.ShortDate), If(JVItems.Status = 1, "Active", "In-Active"), JVItems.PreparedBy _
                                                , JVItems.CheckedBy, JVItems.ApprovedBy, JVItems.UpdatedBy, FormatDateTime(JVItems.UpdatedDate, DateFormat.ShortDate))
                Next
            End If

            If dgv_VoucherHeader.Rows.Count <> 0 Then
                dgv_VoucherHeader.Rows(0).Selected = True

                With dgv_JournalDetails.Columns
                    .Add("JVNumberDetails", "Journal Voucher Number")
                    .Add("JVDAccountCode", "Account Code")
                    .Add("JVDDebit", "Debit")
                    .Add("JVDCredit", "Credit")
                    .Add("JVUpdated", "Updated by")
                    .Add("JVUpdated", "Updated Date")
                End With

                dgv_JournalDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                Dim AllJVDetails = WBillHelper.GetJournalVoucherDetails()
                AllJVDetails = (From x In AllJVDetails Select x Order By x.JVNumber, x.AccountCode).ToList
                Dim JVnumber = CInt(Mid(dgv_VoucherHeader.Rows(dgv_VoucherHeader.CurrentRow.Index).Cells(0).Value.ToString, 5))
                AllJVDetails = (From x In AllJVDetails Where x.JVNumber = CDbl(JVnumber) Select x Order By x.JVNumber, x.AccountCode).ToList

                Dim ReferenceCode As String
               
                For Each JVdetails In AllJVDetails
                    ReferenceCode = "0000000" & JVdetails.JVNumber.ToString()
                    ReferenceCode = Mid(ReferenceCode, Len(JVdetails.JVNumber.ToString()) + 1, 7)
                    ReferenceCode = AMModule.JVNumberPrefix & ReferenceCode

                    dgv_JournalDetails.Rows.Add(ReferenceCode, JVdetails.AccountCode, FormatNumber(JVdetails.Debit, 2), FormatNumber(JVdetails.Credit, 2) _
                                                , JVdetails.UpdatedBy, FormatDateTime(JVdetails.UpdatedDate, DateFormat.ShortDate))
                Next
                cmd_GenerateJV.Enabled = True
            Else
                cmd_GenerateJV.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub cmd_GenerateJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateJV.Click
        If SearchState = 1 Then
            MsgBox("Filter Values have changed, please click Search first", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Try

            Dim AllJVHeader = WBillHelper.GetJournalVoucher()
            Dim AllJVDetails = WBillHelper.GetJournalVoucherDetails()
            Dim Accountingcode = WBillHelper.GetAccountingCodes()
            Dim JVNo = CLng(Mid(dgv_VoucherHeader.Rows(dgv_VoucherHeader.CurrentRow.Index).Cells(0).Value.ToString, 5))
            Dim JV_Signatories = WBillHelper.GetSignatories("JV").First
            Dim AllGPPosted = WBillHelper.GetWESMBillGPPosted()
            If dgv_VoucherHeader.SelectedRows.Count = 0 Then
                MsgBox("Please select a record!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim JVHeader = (From x In AllJVHeader Where x.JVNumber = CLng(JVNo.ToString) Select x).ToList
            Dim JVDetails = (From x In AllJVDetails Where x.JVNumber = CLng(JVNo.ToString) Select x).ToList

            If JVHeader.First.Status = EnumStatus.InActive Then
                MsgBox("Cannot print In-Active JV", MsgBoxStyle.Exclamation, "Denied")
                Exit Sub
            End If
            
            Dim JournalVoucherTable As New DataTable
            JournalVoucherTable.TableName = "JournalVoucher"
            With JournalVoucherTable.Columns
                .Add("JV_NO", GetType(String))
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
                .Add("BIRPermit", GetType(String))
            End With

            For Each item In JVHeader
                Dim dr As DataRow
                dr = JournalVoucherTable.NewRow
                Dim _item = item
                With item
                    dr("JV_NO") = Me.bFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                    dr("JV_DATE") = .JVDate
                    dr("BATCHCODE") = .BatchCode.ToString
                    'dr("OFFSETNUMBER") = .OffsetNumber.ToString
                    dr("STATUS") = .Status
                    dr("PREPAREDBY") = .PreparedBy
                    dr("CHECKEDBY") = JV_Signatories.Signatory_1
                    dr("APPROVEDBY") = JV_Signatories.Signatory_2
                    dr("UPDATEDBY") = .UpdatedBy
                    dr("UPDATEDDATE") = .UpdatedDate
                    dr("GPREF_NO") = (From x In AllGPPosted _
                                      Where x.BatchCode = _item.BatchCode _
                                      Select x.GPRefNo).FirstOrDefault
                    dr("REMARKS") = (From x In AllGPPosted _
                                      Where x.JVNumber = _item.JVNumber _
                                      Select x.Remarks).FirstOrDefault                    
                End With
                JournalVoucherTable.Rows.Add(dr)
            Next

            Dim JournalVoucherDetailsTable As New DataTable
            JournalVoucherDetailsTable.TableName = "JournalVoucherDetails"
            With JournalVoucherDetailsTable.Columns
                .Add("JV_NO", GetType(String))
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
                    jvRow("JV_NO") = Me.bFactory.GenerateBIRDocumentNumber(.JVNumber, BIRDocumentsType.JournalVoucher)
                    jvRow("ACCOUNTCODE") = .AccountCode
                    jvRow("CREDIT") = .Credit
                    jvRow("DEBIT") = .Debit
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

            ProgressThread.Show("Please wait while preparing JV.")
            Dim frmReport As New frmReportViewer
            With frmReport
                .LoadJournalVoucher(DS)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub cmd_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Refresh.Click
        Dim AllJV = WBillHelper.GetJournalVoucher()
        cbo_JVNO.Items.Clear()
        cbo_JVNO.Items.Add("View All")
        For Each item In AllJV
            cbo_JVNO.Items.Add(item.JVNumber)
        Next
        cbo_JVNO.SelectedIndex = 0
        dgv_VoucherHeader.Rows.Clear()
        dgv_JournalDetails.Rows.Clear()
        cmd_GenerateJV.Enabled = False
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub dgv_VoucherHeader_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_VoucherHeader.CellClick
        dgv_JournalDetails.Rows.Clear()
        dgv_JournalDetails.Columns.Clear()
        Try
            With dgv_JournalDetails.Columns
                .Add("JVNumberDetails", "Journal Voucher Header")
                .Add("JVDAccountCode", "Account Code")
                .Add("JVDDebit", "Debit")
                .Add("JVDCredit", "Credit")
                .Add("JVUpdated", "Updated by")
                .Add("JVUpdated", "Updated Date")
            End With
            dgv_JournalDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            Dim AllJVDetails = WBillHelper.GetJournalVoucherDetails()
            AllJVDetails = (From x In AllJVDetails Select x Order By x.JVNumber, x.AccountCode).ToList
            If AllJVDetails.Count <> 0 Then


                Dim JVnumber = CInt(Mid(dgv_VoucherHeader.Rows(dgv_VoucherHeader.CurrentRow.Index).Cells(0).Value.ToString, 5))
                AllJVDetails = (From x In AllJVDetails Where x.JVNumber = CDbl(JVnumber) Select x Order By x.JVNumber, x.AccountCode).ToList

                For Each JVdetails In AllJVDetails
                    dgv_JournalDetails.Rows.Add(Me.bFactory.GenerateBIRDocumentNumber(JVdetails.JVNumber, BIRDocumentsType.JournalVoucher), JVdetails.AccountCode, FormatNumber(JVdetails.Debit, 2), FormatNumber(JVdetails.Credit, 2) _
                                                , JVdetails.UpdatedBy, FormatDateTime(JVdetails.UpdatedDate, DateFormat.ShortDate))
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cbo_JVNO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_JVNO.SelectedIndexChanged
        SearchState = 1
    End Sub

    Private Sub cbo_JVNO_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_JVNO.TextChanged
        SearchState = 1
    End Sub

    Private Sub dgv_VoucherHeader_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_VoucherHeader.CellContentClick

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cbox_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_Date.CheckedChanged
        Me.dtp_from.Enabled = Me.cbox_Date.Checked
        Me.dtp_to.Enabled = Me.cbox_Date.Checked
    End Sub

    Private Sub cbox_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_No.CheckedChanged
        Me.cbo_JVNO.Enabled = Me.cbox_No.Checked
    End Sub
End Class