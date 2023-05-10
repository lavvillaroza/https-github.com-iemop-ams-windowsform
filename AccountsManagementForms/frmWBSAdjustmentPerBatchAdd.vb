Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSAdjustmentPerBatchAdd
    Public mainForm As frmWBSAdjustmentPerBatchMain
    Dim WBSAdjOnWTaxHelper As WBSAdjOnWTAXHelper
    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub frmWESMBillSummaryAdjustmentPerBatchAddView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            WBSAdjOnWTaxHelper = New WBSAdjOnWTAXHelper
            WBSAdjOnWTaxHelper.ResetObject()
            Me.cmb_BillingPeriodNo.Items.Clear()
            For Each item In WBSAdjOnWTaxHelper.ListOfWESMBillNo
                Me.cmb_BillingPeriodNo.Items.Add(item)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmd_BillingPeriodNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BillingPeriodNo.SelectedIndexChanged
        Try
            Me.cmb_ChargeType.ResetText()
            Me.cmb_ChargeType.Items.Clear()
            Me.cmb_ChargeType.Items.Add("Energy")
            'Me.cmb_ChargeType.Items.Add("MarketFees")
            Me.cmb_BillingType.ResetText()
            Me.cmb_BillingBatchNo.ResetText()
            Me.dgv_WBSWhTAX.Rows.Clear()
            Me.txtbox_TotalBeginningBalance.Text = "0.00"
            Me.txtbox_TotalEndingBalance.Text = "0.00"
            Me.txtbox_ARAmountAdjusted.Text = "0.00"
            Me.txtbox_APAmountAdjusted.Text = "0.00"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmb_ChargeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ChargeType.SelectedIndexChanged
        Try
            Me.cmb_BillingType.ResetText()
            Me.cmb_ChargeType.ResetText()
            Me.cmb_BillingBatchNo.ResetText()
            Me.cmb_BillingType.Items.Clear()
            Me.cmb_BillingType.Items.Add(EnumBalanceType.AR.ToString)
            Me.cmb_BillingType.Items.Add(EnumBalanceType.AP.ToString)
            Me.dgv_WBSWhTAX.Rows.Clear()
            Me.txtbox_TotalBeginningBalance.Text = "0.00"
            Me.txtbox_TotalEndingBalance.Text = "0.00"
            Me.txtbox_ARAmountAdjusted.Text = "0.00"
            Me.txtbox_APAmountAdjusted.Text = "0.00"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmb_BillingType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BillingType.SelectedIndexChanged
        Try
            Dim GetSelectedBPNo As Integer = CInt(cmb_BillingPeriodNo.SelectedItem)
            Dim getSelectedChargeType As New EnumChargeType
            If Me.cmb_ChargeType.SelectedItem = "Energy" Then
                getSelectedChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), "E"), EnumChargeType)
            Else
                getSelectedChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), "MF"), EnumChargeType)
            End If
            WBSAdjOnWTaxHelper.GetWESMBillSummaryUsingBPNo(GetSelectedBPNo, getSelectedChargeType)
            Me.cmb_BillingBatchNo.Items.Clear()
            For Each item In (From x In WBSAdjOnWTaxHelper.ListOfWESMBillSummary Select x.WESMBillBatchNo Distinct Order By WESMBillBatchNo Descending)
                Me.cmb_BillingBatchNo.Items.Add(item)
            Next
            Me.dgv_WBSWhTAX.Rows.Clear()
            Me.txtbox_TotalBeginningBalance.Text = "0.00"
            Me.txtbox_TotalEndingBalance.Text = "0.00"
            Me.txtbox_ARAmountAdjusted.Text = "0.00"
            Me.txtbox_APAmountAdjusted.Text = "0.00"
            Me.cmb_BillingBatchNo.ResetText()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmb_BillingBatchNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_BillingBatchNo.SelectedIndexChanged
        Dim sumofBeginningBalance As Decimal = 0D
        Dim sumofEndingBalance As Decimal = 0D
        Dim getWBSList As New List(Of WESMBillSummary)
        Dim getSelectedBPNo As Integer = CInt(cmb_BillingBatchNo.SelectedItem)
        Dim getSelectedBillingType As EnumBalanceType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(cmb_BillingType.SelectedItem)), EnumBalanceType)
        Dim getChargeType As EnumChargeType
        If cmb_ChargeType.SelectedItem = "Energy" Then
            getChargeType = EnumChargeType.E
        Else
            getChargeType = EnumChargeType.MF
        End If
        getWBSList = Me.WBSAdjOnWTaxHelper.GetWESMBIllSummaryList(getSelectedBPNo, getSelectedBillingType, getChargeType)
        If getWBSList.Count = 0 Then
            MessageBox.Show("No available data for adjustment!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.dgv_WBSWhTAX.Rows.Clear()
        Me.dgv_WBSWhTAX.Columns(5).DefaultCellStyle.ForeColor = Color.Red
        For Each item In getWBSList
            Me.dgv_WBSWhTAX.Rows.Add(item.INVDMCMNo, item.DueDate.ToShortDateString, item.NewDueDate.ToShortDateString, item.IDNumber.IDNumber, item.IDNumber.ParticipantID, FormatNumber(item.EnergyWithhold, UseParensForNegativeNumbers:=TriState.True), FormatNumber(item.BeginningBalance, UseParensForNegativeNumbers:=TriState.True), FormatNumber(item.EndingBalance, UseParensForNegativeNumbers:=TriState.True), "0.00")
            sumofBeginningBalance += item.BeginningBalance
            sumofEndingBalance += item.EndingBalance
        Next
        Me.txtbox_TotalBeginningBalance.Text = FormatNumber(sumofBeginningBalance, UseParensForNegativeNumbers:=TriState.True)
        Me.txtbox_TotalEndingBalance.Text = FormatNumber(sumofEndingBalance, UseParensForNegativeNumbers:=TriState.True)
        Me.txtbox_ARAmountAdjusted.Text = "0.00"
        Me.txtbox_APAmountAdjusted.Text = "0.00"
        Me.DataGridViewFormat(dgv_WBSWhTAX)
    End Sub

#Region "DataGridView Format for TransferToPR"
    Private Sub DataGridViewFormat(ByVal dgv As DataGridView)
        For i As Int32 = 0 To dgv.ColumnCount - 1
            If i = 8 Then
                dgv.Columns(i).ReadOnly = False
            Else
                dgv.Columns(i).ReadOnly = True
            End If
        Next
    End Sub
#End Region

    Private Sub dgv_wbsAR_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv_WBSWhTAX.CellEndEdit
        Try
            Select Case e.ColumnIndex
                Case 8
                    If Not IsNumeric(Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                        MessageBox.Show("Amount Adjusted is not numeric.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                    Else
                        If Me.cmb_ChargeType.Text = "Energy" And CDec(Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                            MessageBox.Show("Amount Adjusted on Energy should not be negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "0.00"
                        Else
                            Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FormatNumber(Me.dgv_WBSWhTAX.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, UseParensForNegativeNumbers:=TriState.True)
                        End If
                        Me.FormatTextBox()
                    End If
                Case Else
                    Me.dgv_WBSWhTAX.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormatTextBox()
        Dim sumofARAmountAdjusted As Decimal = 0D
        Dim sumofAPAmountAdjusted As Decimal = 0D
        For Each row As DataGridViewRow In dgv_WBSWhTAX.Rows
            If CDec(row.Cells("AmountAdjusted").Value) < 0 Then
                sumofARAmountAdjusted += CDec(row.Cells("AmountAdjusted").Value)
            ElseIf CDec(row.Cells("AmountAdjusted").Value) > 0 Then
                sumofAPAmountAdjusted += CDec(row.Cells("AmountAdjusted").Value)
            End If
        Next
        Me.txtbox_ARAmountAdjusted.Text = FormatNumber(sumofARAmountAdjusted, UseParensForNegativeNumbers:=TriState.True)
        Me.txtbox_APAmountAdjusted.Text = FormatNumber(sumofAPAmountAdjusted, UseParensForNegativeNumbers:=TriState.True)
    End Sub

    Private Sub btn_Create_Click(sender As Object, e As EventArgs) Handles btn_Create.Click
        If CDec(Me.txtbox_ARAmountAdjusted.Text) = 0D And CDec(Me.txtbox_APAmountAdjusted.Text) = 0D Then
            MessageBox.Show("Please add a amount for adjustment.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim getSelectedBPNo As Integer = CInt(cmb_BillingPeriodNo.SelectedItem)
        Dim getSelectedBBNo As Integer = CInt(cmb_BillingBatchNo.SelectedItem)
        Dim getSelectedBillingType As EnumBalanceType = CType(System.Enum.Parse(GetType(EnumBalanceType), CStr(cmb_BillingType.SelectedItem)), EnumBalanceType)
        Dim getTotalARAmountAdj As Decimal = CDec(txtbox_ARAmountAdjusted.Text)
        Dim getTotalAPAmountAdj As Decimal = CDec(txtbox_APAmountAdjusted.Text)
        Dim getChargeType As EnumChargeType
        If cmb_ChargeType.SelectedItem = "Energy" Then
            getChargeType = EnumChargeType.E
        Else
            getChargeType = EnumChargeType.MF
        End If

        Dim newWBSAmountAdj As WBSAmountAdjustmentInWHTAX = New WBSAmountAdjustmentInWHTAX(1, getSelectedBPNo, getSelectedBBNo, getSelectedBillingType, getChargeType, getTotalARAmountAdj, getTotalAPAmountAdj, 0)

        Dim ListofInvoices As New Dictionary(Of String, Decimal)
        For Each row As DataGridViewRow In dgv_WBSWhTAX.Rows
            Dim InvoiceNo As String = row.Cells("InvoiceNumber").Value
            Dim AmountAdjusted As Decimal = CDec(row.Cells("AmountAdjusted").Value)
            If AmountAdjusted <> 0D Then
                If Not ListofInvoices.ContainsKey(InvoiceNo) Then
                    ListofInvoices.Add(InvoiceNo, AmountAdjusted)
                End If
            End If
        Next

        Me.WBSAdjOnWTaxHelper.GetWESMBillSummaryAdjustment(newWBSAmountAdj, ListofInvoices)

        With frmWBSAdjustmentPerBatchView
            .mainForm = Me.mainForm
            .addForm = Me
            .WBSAdjOnWTaxHelper = Me.WBSAdjOnWTaxHelper
            .isView = False
            .ShowDialog()
        End With

    End Sub


End Class