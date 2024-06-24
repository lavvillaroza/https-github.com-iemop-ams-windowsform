Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmWTAInstallmentMgtView
    Private _WTAInstallmentHelper As WTAInstallmentHelper
    Private _DicBPSTL As Dictionary(Of Integer, String)
    Public _ListofWTAInstallment As List(Of WTAInstallment)
    Private cts As CancellationTokenSource
    Private newProgress As New ProgressClass
    Private Sub frmWTAInstallmentMgtView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        _WTAInstallmentHelper = New WTAInstallmentHelper()
        Try
            Me.getListofCurrentWTAInstallment()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub getListofCurrentWTAInstallment()
        Try
            _DicBPSTL = Await _WTAInstallmentHelper.GetListofBPSTLWTAInstallmentAsync()
            _ListofWTAInstallment = New List(Of WTAInstallment)
            Me.cmbBillingPeriod.Items.Clear()
            For Each item In _DicBPSTL.Keys
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
        Dim getSTLRun() As String = _DicBPSTL.Item(selectedBP).Split(",")

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

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Try
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

            _ListofWTAInstallment = Await _WTAInstallmentHelper.GetListofWTAInstallmentAsync(selectedBP, selectedSTLRun)

            Me.DGridViewWTA.Rows.Clear()
            For Each item In _ListofWTAInstallment.OrderBy(Function(x) x.WTAINO).ThenBy(Function(x) x.IDNumber.IDNumber).ThenBy(Function(x) x.ChargeType)
                Dim totalTermAmount As Decimal = item.ListofWTAInstallmentTerm.Select(Function(x) x.TermAmount).Sum()
                Dim totalPaidAmount As Decimal = item.ListofWTAInstallmentTerm.Select(Function(x) x.PaidAmount).Sum()
                Dim ewt As Decimal = item.EnergyWithhold
                With Me.DGridViewWTA.Rows
                    .Add(item.IDNumber.IDNumber,
                         item.INVDMCMNo.ToString(),
                         item.ChargeType.ToString(),
                         FormatNumber(totalTermAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                         FormatNumber(totalPaidAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                         item.Status.ToString(),
                         "View Details")
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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
        Me.getListofCurrentWTAInstallment()
        Me.DGridViewWTA.Rows.Clear()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DGridViewWTA_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGridViewWTA.CellContentClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        Try
            If e.ColumnIndex = 6 Then
                Dim getTransactionNo As String = CStr(DGridViewWTA.Rows(e.RowIndex).Cells(1).Value)
                Dim getChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), CStr(DGridViewWTA.Rows(e.RowIndex).Cells(2).Value)), EnumChargeType)
                Dim getWTAInstallment As WTAInstallment = _ListofWTAInstallment.Where(Function(x) x.INVDMCMNo = getTransactionNo And x.ChargeType = getChargeType).FirstOrDefault()
                With frmWTAInstallmentTermsView
                    .setDGView(getWTAInstallment.ListofWTAInstallmentTerm)
                    .ShowDialog()
                End With
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        tsslbl_Msg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Async Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        Dim sFolderDialog As New FolderBrowserDialog
        Dim TargetPath As String = ""

        Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
        Try
            If DGridViewWTA.Rows.Count = 0 Then
                MessageBox.Show("Please selecte Billing Period and Settlement Run!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            With sFolderDialog
                .ShowDialog()
                If .SelectedPath.ToString.Trim.Length = 0 Then
                    Exit Sub
                Else
                    TargetPath = sFolderDialog.SelectedPath
                End If
            End With

            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            getTimeStart = DateTime.Now()


            cts = New CancellationTokenSource
            Me.Timer1.Start()

            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while preparing."}
            UpdateProgress(newProgress)

            Await Task.Run(Function() _WTAInstallmentHelper.GenerateWTAInstallmentSummaryReport(TargetPath, _ListofWTAInstallment, progressIndicator, cts.Token))

            getTimeEnd = DateTime.Now()
            cts = Nothing

            MessageBox.Show("Generating Reports are successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            Me.Timer1.Stop()
            Me.ctrl_statusStrip.Text = "Ready..."
        End Try
    End Sub
End Class