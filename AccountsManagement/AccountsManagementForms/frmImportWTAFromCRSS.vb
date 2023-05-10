Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib
Imports System.Threading.Tasks

Public Class frmImportWTAFromCRSS
    Private impWTACRSSHelper As New ImportWTAFromCRSSDBHelper
    Private ListOfSignatories As List(Of DocSignatories)
    Private HeaderCheckBox As CheckBox = Nothing
    Private IsHeaderCheckBoxClicked As Boolean = False
    Private TotalCheckBoxes As Integer = 0
    Private TotalCheckedCheckBoxes As Integer = 0
    Private cts As CancellationTokenSource
    Private Async Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim CountWESMBillExist As Integer = 0
        Dim ans As MsgBoxResult
        Try
            Panel_Head.Enabled = False
            tc_Viewer.Enabled = False
            'Check if there is row selected
            Dim ticked As Boolean = False
            For index As Integer = 0 To Me.dgv_WTAList.RowCount - 1
                If CBool(Me.dgv_WTAList.Rows(index).Cells("colChckBox").Value) Then
                    ticked = True
                    Exit For
                End If
            Next

            If Not ticked Then
                MsgBox("No row/s ticked", MsgBoxStyle.Exclamation, "Select WESM Bill")
                Exit Sub
            End If
            Dim billingPeriod As Integer
            Dim stlRun As String
            'Check if the WESM Bill selected is already posted
            For index As Integer = 0 To Me.dgv_WTAList.RowCount - 1
                With Me.dgv_WTAList.Rows(index)
                    If CBool(.Cells("colChckBox").Value) Then
                        billingPeriod = CInt(.Cells("colBP").Value)
                        stlRun = CStr(.Cells("colSTLRun").Value)
                        Dim isBillPosted = impWTACRSSHelper._WBillHelper.IsWESMBillPosted(billingPeriod, stlRun, EnumChargeType.E)
                        If isBillPosted = True Then
                            MsgBox("Billing Period = " & CStr(billingPeriod) & ", Settlement Run = " & stlRun &
                                   ", Charge Type = " & EnumChargeType.E.ToString() & " are already posted!", MsgBoxStyle.Critical, "Warning")
                            Exit Sub
                        End If
                    End If
                End With
            Next

            'Check if there are already existing record in database
            For index As Integer = 0 To Me.dgv_WTAList.RowCount - 1
                With Me.dgv_WTAList.Rows(index)
                    If CBool(.Cells("colChckBox").Value) Then
                        billingPeriod = CInt(.Cells("colBP").Value)
                        stlRun = CStr(.Cells("colSTLRun").Value)

                        'Check if there is existing records
                        CountWESMBillExist = impWTACRSSHelper._WBillHelper.GetWESMBillCount(billingPeriod, stlRun, EnumChargeType.E)
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
            Me.ListOfSignatories = impWTACRSSHelper._WBillHelper.GetSignatories()
            cts = New CancellationTokenSource
            For index As Integer = 0 To Me.dgv_WTAList.RowCount - 1
                With Me.dgv_WTAList.Rows(index)
                    If CBool(.Cells("colChckBox").Value) Then
                        billingPeriod = CInt(.Cells("colBP").Value)
                        stlRun = CStr(.Cells("colSTLRun").Value)
                        Dim getSelectedDueDate As Date = CDate(Me.cmb_DueDate.SelectedItem)
                        Dim groupID As Long = CLng(.Cells("colGroupID").Value)
                        Dim getWTACoverSummaryItem As ImportWTAFromCRSS = impWTACRSSHelper.newImportWTAFromCRSSList.Where(Function(x) x.BillingPeriod = billingPeriod And x.SettlementRun = stlRun And x.GroupID = groupID).First()

                        Dim itemJV As JournalVoucher = Me.GenerateJournalVoucherForEnergy(getWTACoverSummaryItem.WESMBillList, 0)
                        Dim getCalendarBP As CalendarBillingPeriod = (From x In impWTACRSSHelper._WBillHelper.GetCalendarBP() Where x.BillingPeriod = billingPeriod Select x).FirstOrDefault

                        Dim itemGP As WESMBillGPPosted = Me.GenerateGPPosted(getCalendarBP, stlRun, getSelectedDueDate, EnumChargeType.E, itemJV)

                        Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
                        'added by Lance 08/08/2021
                        Await Task.Run(Sub() impWTACRSSHelper.SaveUplodedWESMBill(getCalendarBP, stlRun, groupID, EnumChargeType.E, 0, itemJV, itemGP, CountWESMBillExist, progressIndicator, cts.Token))

                        'Updated By Lance 08/17/2014
                        _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString, "BP: " & billingPeriod & "; STLRun: " & stlRun & "; GroupID: " & groupID & "; DueDate: " & getSelectedDueDate.ToShortDateString(), "Uploading WESM Bills from CRSS DB", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUploaded.ToString, AMModule.UserName)
                        'Me.dgv_WTAList.Rows.RemoveAt(index)
                    End If
                End With
            Next

            cts = Nothing
            ProgressThread.Close()
            MsgBox("Successfully uploaded to Database", MsgBoxStyle.Information, "Success!")
        Catch ex As Exception
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPUploadWESMBillFetchFromCRSSDBWindow.ToString, ex.Message, "Uploading WESM Bills from CRSS DB", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)
        Finally
            Panel_Head.Enabled = True
            tc_Viewer.Enabled = True
        End Try
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatus_LabelMsg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub frmImportWTAFromCRSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Try
            Me.RefreshDueDateList()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshDueDateList()
        Me.cmb_DueDate.Items.Clear()
        For Each item In impWTACRSSHelper.NewWTADueDateList
            Me.cmb_DueDate.Items.Add(item.ToString("MM/dd/yyyy"))
        Next
        Me.dgv_WTAList.Rows.Clear()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.RefreshDueDateList()
    End Sub

#Region "Functions/Methods"
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
    Private Function GenerateJournalVoucherForEnergy(ByVal listItems As List(Of WESMBill), ByVal WithholdingTax As Decimal) As JournalVoucher
        Dim result As New JournalVoucher
        Try
            Dim jvDetails As New List(Of JournalVoucherDetails)
            Dim jvDetail As JournalVoucherDetails

            Dim signatories As New DocSignatories

            Try
                signatories = (From x In Me.ListOfSignatories
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

    Private Sub cmb_DueDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_DueDate.SelectedIndexChanged
        Try
            Dim getSelectedDueDate As Date = CDate(Me.cmb_DueDate.SelectedItem)
            ProgressThread.Show("Please wait while fetching...")

            impWTACRSSHelper.FillTheDGV(getSelectedDueDate)
            Me.dgv_WTAList.Rows.Clear()
            For Each item In impWTACRSSHelper.newImportWTAFromCRSSList
                Me.dgv_WTAList.Rows.Add(False, item.BillingPeriod, item.SettlementRun, item.GroupID, FormatNumber(item.TotalNetSales, 2, , TriState.True).ToString(), FormatNumber(item.TotalEWTSales, 2, , TriState.True).ToString(), FormatNumber(item.TotalNetPurchases, 2, , TriState.True).ToString(), FormatNumber(item.TotalEWTPurchases, 2, , TriState.True).ToString(), item.Remarks)
            Next
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgv_WTAList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WTAList.CellContentClick
        If (e.ColumnIndex = 0 AndAlso e.RowIndex >= 0) Then
            Try
                Dim value = DirectCast(dgv_WTAList(e.ColumnIndex, e.RowIndex).FormattedValue,
                                       Nullable(Of Boolean))
                If (value.HasValue AndAlso value = True) Then
                    Dim result = MessageBox.Show("Are you sure to uncheck item?", "",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (result = System.Windows.Forms.DialogResult.Yes) Then
                        dgv_WTAList(e.ColumnIndex, e.RowIndex).Value = False
                    End If
                Else
                    ProgressThread.Show("Please wait while fetching...")

                    dgv_WTAList(e.ColumnIndex, e.RowIndex).Value = True
                    Dim rowBP As Integer = CInt(dgv_WTAList(1, e.RowIndex).Value)
                    Dim rowSTLRun As String = CStr(dgv_WTAList(2, e.RowIndex).Value)
                    Dim rowGroupID As Long = CLng(dgv_WTAList(3, e.RowIndex).Value)
                    Dim getSelectedDueDate As Date = CDate(Me.cmb_DueDate.SelectedItem)
                    Dim totalNetSales As Decimal = CDec(dgv_WTAList(4, e.RowIndex).Value)
                    Dim totalEWTSales As Decimal = CDec(dgv_WTAList(5, e.RowIndex).Value)

                    Dim totalNetPurchases As Decimal = CDec(dgv_WTAList(6, e.RowIndex).Value)
                    Dim totalEWTPurchases As Decimal = CDec(dgv_WTAList(7, e.RowIndex).Value)

                    Dim diffTotal1 As Decimal = totalNetSales + totalNetPurchases
                    If Not totalNetSales = Math.Abs(totalNetPurchases) Then
                        ProgressThread.Close()
                        MessageBox.Show("There is difference between the Total Net Sales and Total Net Purchases!" & vbNewLine & "Difference: " & FormatNumber(diffTotal1, 2, , TriState.True).ToString(), "Uploading Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        dgv_WTAList(e.ColumnIndex, e.RowIndex).Value = False
                        Exit Sub
                    End If
                    Dim diffTotal2 As Decimal = totalNetSales + totalNetPurchases
                    If Not Math.Abs(totalEWTSales) = totalEWTPurchases Then
                        ProgressThread.Close()
                        MessageBox.Show("There is difference between the Total EWT Sales and Total EWT Purchases!" & vbNewLine & "Difference: " & FormatNumber(diffTotal2, 2, , TriState.True).ToString(), "Uploading Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        dgv_WTAList(e.ColumnIndex, e.RowIndex).Value = False
                        Exit Sub
                    End If

                    Me.impWTACRSSHelper.FetchSelectedData(rowBP, rowSTLRun, getSelectedDueDate, rowGroupID)
                    Dim getSelectedWTAItem As ImportWTAFromCRSS = Me.impWTACRSSHelper.newImportWTAFromCRSSList.Where(Function(x) x.BillingPeriod = rowBP And x.SettlementRun = rowSTLRun And x.GroupID = rowGroupID).FirstOrDefault

                    ProgressThread.Close()
                    If getSelectedWTAItem.ListOfError.Count <> 0 Then
                        MessageBox.Show("Error found in uploaded data.", "Uploading Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        frmViewDetails.ShowListOfErrorsForUploadingInCRSSDB(getSelectedWTAItem.ListOfError.Distinct.ToList)
                        frmViewDetails.gb_totalColl.Hide()
                        frmViewDetails.ShowDialog()
                        dgv_WTAList(e.ColumnIndex, e.RowIndex).Value = False
                    End If
                End If
            Catch ex As Exception
                ProgressThread.Close()
                MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
#End Region
    Private Sub frmImportWTAFromCRSS_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If cts IsNot Nothing Then
            ProgressThread.Close()
            If MessageBox.Show("Are you sure to cancel this process?", "Close",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                cts.Cancel()
                e.Cancel = True
                MessageBox.Show("Please try to close again!", "Close", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ProgressThread.Show("Please wait while continuing the process.")
                e.Cancel = True
                Me.Show()
            End If
        End If
    End Sub
End Class