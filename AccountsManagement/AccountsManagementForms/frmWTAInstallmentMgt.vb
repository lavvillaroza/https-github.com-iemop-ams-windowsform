Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmWTAInstallmentMgt
    Private _WTAInstallmentHelper As WTAInstallmentHelper
    Private _ListofWTASummaryForInstallment As List(Of WTAInstallment)
    Private cts As CancellationTokenSource
    Private stopWatch As New Diagnostics.Stopwatch
    Private newProgress As New ProgressClass

    Private Sub frmWTAInstallmentMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        _WTAInstallmentHelper = New WTAInstallmentHelper()
        _ListofWTASummaryForInstallment = New List(Of WTAInstallment)
        Try
            Me.getWTASummarySubjectForInstallment()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        tsslbl_Msg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Async Sub getWTASummarySubjectForInstallment()
        Try
            _ListofWTASummaryForInstallment = Await _WTAInstallmentHelper.GetWESMBillSummaryAsync()
            Dim getDistinctBP As List(Of Integer) = _ListofWTASummaryForInstallment.Select(Function(x) x.BillPeriod).Distinct().OrderByDescending(Function(y) y).ToList()

            Me.cmbBillingPeriod.Items.Clear()
            For Each item In getDistinctBP
                Me.cmbBillingPeriod.Items.Add(item)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub cmbBillingPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBillingPeriod.SelectedIndexChanged
        If cmbBillingPeriod.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim selectedBP As Integer = CInt(cmbBillingPeriod.SelectedItem)
        Dim getSTLRun As List(Of String) = _ListofWTASummaryForInstallment.Where(Function(x) x.BillPeriod = selectedBP).Select(Function(x) x.STLRun).Distinct.ToList()
        Dim listofSTL As List(Of Tuple(Of String, Integer)) = New List(Of Tuple(Of String, Integer))

        For Each item In getSTLRun
            If item.Length = 1 Then
                Dim itemSTL As Tuple(Of String, Integer) = New Tuple(Of String, Integer)(item, 0)
                listofSTL.Add(itemSTL)
            Else
                Dim itemSTL As Tuple(Of String, Integer) = New Tuple(Of String, Integer)(item, CInt(item.Replace("F", "").Replace("R", "")))
                listofSTL.Add(itemSTL)
            End If
        Next
        Me.cmbSettlementRun.Text = ""

        Me.cmbSettlementRun.Items.Clear()
        Dim sortedListofSTL As List(Of String) = listofSTL.OrderBy(Function(x) x.Item2).Select(Function(x) x.Item1).ToList()
        For Each item In sortedListofSTL
            Me.cmbSettlementRun.Items.Add(item)
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If cmbBillingPeriod.SelectedIndex = -1 Then
            MessageBox.Show("Please selecte Billing Period!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If cmbSettlementRun.SelectedIndex = -1 Then
            MessageBox.Show("Please selecte Settlement Run!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim selectedBP As Integer = CInt(Me.cmbBillingPeriod.SelectedItem)
        Dim selectedSTLRun As String = CStr(Me.cmbSettlementRun.SelectedItem)

        Dim getListofWTASummary As List(Of WTAInstallment) = _ListofWTASummaryForInstallment.Where(Function(x) x.BillPeriod = selectedBP And x.STLRun = selectedSTLRun).OrderBy(Function(x) x.IDNumber.IDNumber).ToList()

        Me.DGridViewWTA.Rows.Clear()
        For Each item In getListofWTASummary
            Dim netEnergy As Decimal = item.EndingBalance - item.EnergyWithhold
            Dim ewt As Decimal = item.EnergyWithhold
            With Me.DGridViewWTA.Rows
                .Add(item.IDNumber.IDNumber,
                     item.INVDMCMNo.ToString(),
                     item.ChargeType.ToString(),
                     FormatNumber(netEnergy.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                     FormatNumber(ewt.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                     False)
            End With
        Next
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cmbSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cmbSelectAll.CheckedChanged
        If cmbSelectAll.Checked = True Then
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewWTA.Rows
                If CBool(row.Cells("colSelect").Value) = False Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No available row to check!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to check all?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            For Each row As DataGridViewRow In DGridViewWTA.Rows
                If CBool(row.Cells("colSelect").Value) = False Then
                    row.Cells("colSelect").Value = True
                End If
            Next
        Else
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewWTA.Rows
                If CBool(row.Cells("colSelect").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No available row to uncheck!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to uncheck all?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            For Each row As DataGridViewRow In DGridViewWTA.Rows
                If CBool(row.Cells("colSelect").Value) = True Then
                    row.Cells("colSelect").Value = False
                End If
            Next

        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.refreshData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub refreshData()
        Me.cmbBillingPeriod.Text = ""
        Me.cmbSettlementRun.Text = ""
        Me.cmbSelectAll.Checked = False
        Me.getWTASummarySubjectForInstallment()
        Me.DGridViewWTA.Rows.Clear()
    End Sub

    Private Async Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim rowCounter As Integer = 0
        Dim listofWTASelected As New List(Of WTAInstallment)


        Try
            For Each row As DataGridViewRow In DGridViewWTA.Rows
                If CBool(row.Cells("colSelect").Value) = True Then
                    rowCounter += 1
                    Dim transNo As String = row.Cells("colTransactionNo").Value
                    Dim chargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(row.Cells("colChargeType").Value)), EnumChargeType)

                    Dim getSelectedWTA = _ListofWTASummaryForInstallment.Where(Function(x) x.INVDMCMNo = transNo And x.ChargeType = chargeType).FirstOrDefault
                    If Not getSelectedWTA Is Nothing Then
                        listofWTASelected.Add(getSelectedWTA)
                    End If
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No selected row to add!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            cts = New CancellationTokenSource
            With New frmWTAInstallmentTerms
                ._ListofWTASummaryForInstallment = listofWTASelected
                ._WTAInstallerHelper = _WTAInstallmentHelper
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Await _WTAInstallmentHelper.SaveWTAInstallment(._ListofWTASummaryForInstallment, ._DicTerms, progressIndicator, cts.Token)
                    MessageBox.Show("The selected WTA data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.refreshData()
                End If
            End With

        Catch ex As OperationCanceledException
            newProgress = New ProgressClass With {.ProgressMsg = "Process has been canceled as requested."}
            UpdateProgress(newProgress)
            cts = Nothing
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            newProgress = New ProgressClass With {.ProgressMsg = "Process has been canceled due to error."}
            UpdateProgress(newProgress)
            cts = Nothing
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.tsslbl_Msg.Text = "Ready"
            Me.tsslbl_Timer.Text = Me.DGridViewWTA.RowCount.ToString("N0") & " records"
        End Try
    End Sub


End Class