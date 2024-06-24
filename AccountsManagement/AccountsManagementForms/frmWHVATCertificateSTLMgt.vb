Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Windows.Forms.VisualStyles
Imports System.Threading.Tasks
Imports System.Threading


Public Class frmWHVATCertificateSTLMgt
    Private _WHVCSHelper As WHVATCertificateSTLHelper
    Private cts As CancellationTokenSource
    Private stopWatch As New Diagnostics.Stopwatch
    Private newProgress As New ProgressClass

    Private Sub frmWHVatCertificatesSTLMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Me.DGridViewCollection.Rows.Clear()
        Me._WHVCSHelper = New WHVATCertificateSTLHelper
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        tsslbl_Msg.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try

            newProgress = New ProgressClass With {.ProgressMsg = "Adding New Certificate."}
            UpdateProgress(newProgress)

            Dim frmWTCertColTag As New frmWHVATCertificateSTLDetails
            With frmWTCertColTag
                ._WHVCSHelper = _WHVCSHelper
                .TabControl1.TabPages(1).Hide()
                .ShowDialog()
            End With

            Dim addedWHVATCertif As WHVATCertificateSTL = _WHVCSHelper.NewWHVATCertifStl
            If addedWHVATCertif Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("No added data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If addedWHVATCertif.CertificateNo = 0 Then
                ProgressThread.Close()
                MessageBox.Show("Data is not save!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            With DGridViewCollection
                .Rows.Add(addedWHVATCertif.CertificateNo.ToString("d7"),
                          addedWHVATCertif.RemittanceDate.ToString("MM/dd/yyyy"),
                          addedWHVATCertif.BillingIDNumber.IDNumber,
                          FormatNumber(addedWHVATCertif.CollectedAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True))
                .RowsDefaultCellStyle.ForeColor = Drawing.Color.Black
            End With
            '_WHVCSHelper._NewWHVATCertifStl = New WHVATCertificateSTL
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            newProgress = New ProgressClass With {.ProgressMsg = "Ready"}
            UpdateProgress(newProgress)
        End Try
    End Sub

    Private Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            If Me.DGridViewCollection.Rows.Count = 0 Then
                MessageBox.Show("No available data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Me.DGridViewCollection.CurrentRow.Index = -1 Then
                MessageBox.Show("No selected data!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim i As Integer = Me.DGridViewCollection.CurrentRow.Index
            Dim certifNo As Long = CLng(Me.DGridViewCollection.Rows(i).Cells(0).Value)

            ProgressThread.Show("Please wait while loding...")
            Dim frmWTCertColTag As New frmWHVATCertificateSTLDetails
            With frmWTCertColTag
                ._WHVCSHelper = _WHVCSHelper
                ._ViewCertificate = True
                ._CertificateNo = certifNo
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnAllocateToAP_Click(sender As Object, e As EventArgs) Handles btnAllocateToAP.Click
        Try
            Me.gbMenu1.Enabled = False
            Me.gbMenu2.Enabled = False
            Me.chkbox_SelectAll.Enabled = False

            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            Dim listofCertNo As List(Of Long) = New List(Of Long)
            Dim rowCounter As Integer = 0

            Dim remittanceDateDic As New Dictionary(Of Date, Date)
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    rowCounter += 1

                    Dim remittanceDate As Date = CDate(row.Cells("colRemittanceDate").Value)
                    Dim certificateNo As Long = CLng(row.Cells("colCertificateNo").Value)
                    listofCertNo.Add(certificateNo)

                    If remittanceDateDic.ContainsKey(remittanceDate) Then
                        remittanceDateDic.Add(remittanceDate, remittanceDate)
                    End If
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No selected row!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If remittanceDateDic.Count > 1 Then
                MessageBox.Show("Multiple remittance dates are not permitted to allocate!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to allocate the selected Withholding VAT Certificate/s?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            getTimeStart = DateTime.Now()

            cts = New CancellationTokenSource
            Me.Timer1.Start()
            Me.stopWatch.Start()

            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while preparing."}
            UpdateProgress(newProgress)

            Await Task.Run(Sub() _WHVCSHelper.AllocateListOfTaggedWHVATCertAsync(listofCertNo, remittanceDateDic.Keys.First(), progressIndicator, cts.Token))
            Await Task.Run(Sub() _WHVCSHelper.SaveSTLNoticeAsync(remittanceDateDic.Keys.First(), progressIndicator, cts.Token))

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    row.ReadOnly = True
                End If
            Next

            getTimeEnd = DateTime.Now()
            ProgressThread.Close()
            MessageBox.Show("The processed data have been successfully allocated and saved." & vbNewLine _
                          & "Date Time Start: " & getTimeStart.ToString("MM/dd/yyyy hh:mm") & vbNewLine _
                          & "Date Time End: " & getTimeEnd.ToString("MM/dd/yyyy hh:mm"), "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
            Me.stopWatch.Stop()
            Me.stopWatch.Reset()

            Me.tsslbl_Msg.Text = "Ready"
            Me.tsslbl_Timer.Text = Me.DGridViewCollection.RowCount.ToString("N0") & " records"

            Me.gbMenu1.Enabled = True
            Me.gbMenu2.Enabled = True
            Me.chkbox_SelectAll.Enabled = True
        End Try
    End Sub

    Private Async Sub btnUntag_Click(sender As Object, e As EventArgs) Handles btnUntag.Click
        Try
            Me.gbMenu1.Enabled = False
            Me.gbMenu2.Enabled = False
            Me.chkbox_SelectAll.Enabled = False

            Dim rowCounter As Integer = 0
            cts = New CancellationTokenSource
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colUntag").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No selected row to execute untagging.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim ask = MessageBox.Show("Do you really want to untag the selected rows?", "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ask = DialogResult.No Then
                Exit Sub
            End If

            ProgressThread.Show("Please wait while untagging...")
            Dim getListOfRemittanceDate As New Date
            Dim getTimeStart As New DateTime
            Dim getTimeEnd As New DateTime
            Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colUntag").Value) = True Then
                    Dim certifNo As Long = CLng(row.Cells("colCertificateNo").Value)
                    getListOfRemittanceDate = CDate(row.Cells("colRemittanceDate").Value)
                    Await Task.Run(Sub() _WHVCSHelper.UntagEWTSelected(certifNo, progressIndicator, cts.Token))
                    row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                    row.ReadOnly = True
                End If
            Next
            cts = Nothing
            ProgressThread.Close()
            MessageBox.Show("Untagging successfully executed.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.gbMenu1.Enabled = True
            Me.gbMenu2.Enabled = True
            Me.chkbox_SelectAll.Enabled = True
        End Try
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        If cts IsNot Nothing Then
            cts.Cancel()
            newProgress = New ProgressClass With {.ProgressIndicator = 0, .ProgressMsg = "Please wait while canceling..."}
            Me.UpdateProgress(newProgress)
            btn_Close.Enabled = False
        Else
            Me.Close()
        End If
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            MainPanel.Enabled = False
            newProgress = New ProgressClass With {.ProgressMsg = "Please wait while searching..."}
            UpdateProgress(newProgress)
            Dim dateFrom As Date = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
            Dim dateTo As Date = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))
            Dim notAllocated As Boolean = CBool(chkbox_Allocated.Checked)
            Dim notUntagged As Boolean = CBool(chkbox_Untagged.Checked)

            Dim listofWHVATCert As List(Of WHVATCertificateSTL) = Await _WHVCSHelper.GetListOfWHVATCertAsync(dateFrom, dateTo, notAllocated, notUntagged)

            If listofWHVATCert.Count <> 0 Then
                PutDataInDisplayGrid(listofWHVATCert)
            Else
                MessageBox.Show("No available data found!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            chkbox_SelectAll.Checked = False
        Catch ex As Exception
            MainPanel.Enabled = True
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            MainPanel.Enabled = True
            newProgress = New ProgressClass With {.ProgressMsg = "Ready"}
            UpdateProgress(newProgress)
        End Try
    End Sub

    Private Sub PutDataInDisplayGrid(ByVal whTaxCertifCollectionList As List(Of WHVATCertificateSTL))
        DGridViewCollection.Rows.Clear()
        For Each item In whTaxCertifCollectionList
            With DGridViewCollection
                .Rows.Add(item.CertificateNo.ToString("d7"), item.RemittanceDate.ToString("MM/dd/yyyy"), item.BillingIDNumber.IDNumber,
                          FormatNumber(item.CollectedAmount.ToString("0.00"), UseParensForNegativeNumbers:=TriState.True),
                          If(item.AllocatedToAP.ToUpper.Equals("YES"), True, False), If(item.UntagWVAT.ToUpper.Equals("YES"), True, False))
            End With
        Next
        Me.PutColorRedInAllocatedToAPRow()
    End Sub

    Private Sub PutColorRedInAllocatedToAPRow()
        For Each row As DataGridViewRow In DGridViewCollection.Rows
            If CBool(row.Cells("colAllocatedToAP").Value) = True Or CBool(row.Cells("colUntag").Value) = True Then
                row.DefaultCellStyle.ForeColor = Drawing.Color.Red
                row.ReadOnly = True
            Else
                row.DefaultCellStyle.ForeColor = Drawing.Color.Black
                row.Cells("colUntag").ReadOnly = True
            End If
        Next
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        If chkbox_SelectAll.Checked = True Then
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = False Then
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

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = False Then
                    row.Cells("colAllocatedToAP").Value = True
                End If
            Next
        Else
            Dim rowCounter As Integer = 0
            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    rowCounter += 1
                End If
            Next

            If rowCounter = 0 Then
                MessageBox.Show("No available row to uncheck!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For Each row As DataGridViewRow In DGridViewCollection.Rows
                If row.DefaultCellStyle.ForeColor = Drawing.Color.Black And CBool(row.Cells("colAllocatedToAP").Value) = True Then
                    row.Cells("colAllocatedToAP").Value = False
                End If
            Next
        End If
    End Sub
End Class