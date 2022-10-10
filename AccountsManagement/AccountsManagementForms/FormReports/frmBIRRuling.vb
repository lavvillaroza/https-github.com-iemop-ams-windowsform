Option Explicit On
Option Strict On
Imports System
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.Threading.Tasks
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports CrystalDecisions.Shared
Imports WESMLib.Auth.Lib

Public Class frmBIRRuling
    Private WBillHelper As WESMBillHelper
    Private BRHelper As BIRRulingHelper
    Private listOfBIRUling As List(Of BIRRuling)
    Private Sub frmBIRRuling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        BRHelper = New BIRRulingHelper()
        listOfBIRUling = New List(Of BIRRuling)
        Me.GetTransDateFrom()
    End Sub

    Private Sub GetTransDateFrom()
        listOfBIRUling = BRHelper.GetBIRRulingCRDateFromList

        If listOfBIRUling.Count = 0 Then
            MessageBox.Show("No available records! Please generate.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Me.ddl_TransDateFrom.Items.Clear()
        For Each item In listOfBIRUling
            Me.ddl_TransDateFrom.Items.Add(item.TransactionDateFrom.ToShortDateString)
        Next
    End Sub

    Private Sub UpdateProgress(_ProgressMsg As ProgressClass)
        ToolStripStatusLabelCR.Text = _ProgressMsg.ProgressMsg
        ctrl_statusStrip.Refresh()
    End Sub

    Private Sub chkbox_SelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_SelectAll.CheckedChanged
        For i As Integer = 0 To Me.chkLB_Participants.Items.Count - 1
            If chkbox_SelectAll.Checked = True Then
                Me.chkLB_Participants.SetItemChecked(i, True)
            Else
                Me.chkLB_Participants.SetItemChecked(i, False)
            End If
        Next
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If Me.ddl_TransDateFrom.SelectedIndex = -1 Then
            MessageBox.Show("Please selecte Transaction Date From", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim getDateFrom As Date = CDate(Me.ddl_TransDateFrom.SelectedItem)
        Dim getDateTo As Date = CDate(Me.txtBox_DateTo.Text)
        Try
            ProgressThread.Show("Please wait while searching.")
            BRHelper.ViewBIRRulingData(getDateFrom, getDateTo)
            Me.chkLB_Participants.Items.Clear()

            If BRHelper.BIRRuling Is Nothing Then
                ProgressThread.Close()
                MessageBox.Show("No available data! Please generate.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            For Each item In BRHelper.BIRRuling.SellerDetails
                Me.chkLB_Participants.Items.Add(item.Participant.IDNumber)
            Next
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub cmd_Generate_Click(sender As Object, e As EventArgs) Handles cmd_Generate.Click

        'Selection of allocation date
        Dim frm As New frmBRCollectionReportAdd
        With frm
            If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
        End With

        Dim getTransDateFrom As Date = CDate(FormatDateTime(frm.dtFrom.Value, DateFormat.ShortDate))
        Dim getTransDateTo As Date = CDate(FormatDateTime(frm.dtTo.Value, DateFormat.ShortDate))

        Try
            Dim msgAns As New MsgBoxResult

            msgAns = MsgBox("Do you really want to generate and save the New Collection Report?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                If BRHelper.CheckTransactionDateRange(getTransDateFrom, getTransDateTo) = True Then
                    MessageBox.Show("The selected dates are already exist! Please try again.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim progressIndicator As New Progress(Of ProgressClass)(AddressOf UpdateProgress)
                ProgressThread.Show("Please wait while processing.")
                Await Task.Run(Sub() BRHelper.GenerateBIRRuling(getTransDateFrom, getTransDateTo, progressIndicator))

                Me.listOfBIRUling.Add(BRHelper.BIRRuling)
                Me.GetTransDateFrom()

                Me.ddl_TransDateFrom.SelectedIndex = Me.ddl_TransDateFrom.Items.Count() - 1

                For Each item In BRHelper.BIRRuling.SellerDetails.OrderBy(Function(x) x.Participant.IDNumber).ToList
                    Me.chkLB_Participants.Items.Add(item.Participant.IDNumber)
                Next
            End If
            ProgressThread.Close()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_GeneratePDF_Click(sender As Object, e As EventArgs) Handles btn_GeneratePDF.Click
        Try
            Dim _fldrSelect As New FolderBrowserDialog
            Dim ans As New MsgBoxResult
            Dim fPathName As String = ""
            Dim getDateFrom As Date = CDate(Me.ddl_TransDateFrom.SelectedItem)
            Dim getDateTo As Date = CDate(Me.txtBox_DateTo.Text)


            ans = MsgBox("Do you really want to export the records as pdf Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If
            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If

                ProgressThread.Show("Please wait while preparing the file.")

                fPathName = .SelectedPath

                'Get the selected participants
                For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                    Dim selectedParticipant As String = Me.chkLB_Participants.CheckedItems(cnt).ToString()
                    Dim dataSource As New DataTable
                    dataSource = BRHelper.GenerateCollectionReportDatatable(New DSReport.BIRRulingMainDataTable, selectedParticipant)

                    Dim getCRNumber As String = (From x In dataSource.AsEnumerable() Select x.Field(Of String)("CR_NUMBER") Distinct).First()

                    Dim expReport = New RPTBIRCollectionReportSummary
                    expReport.SetDataSource(dataSource)
                    expReport.ExportToDisk(ExportFormatType.PortableDocFormat, fPathName & "\" &
                                           "CSR_IEMOP_" & selectedParticipant.Replace("_FIT", "").ToString & "_" & getCRNumber & "_" & getDateFrom.ToString("yyyyMMdd") & "-" & getDateTo.ToString("yyyyMMdd") & ".pdf")
                    expReport.Close()
                    expReport.Dispose()
                Next

                ProgressThread.Close()
                MsgBox("Successfully downloaded!", MsgBoxStyle.Information, "Downloaded")
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnShowReport_Click(sender As Object, e As EventArgs) Handles btnShowReport.Click
        Try
            Dim listOfSelectedParticipant As New List(Of String)
            ProgressThread.Show("Please wait while loading.")
            'Get the selected participants
            For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                listOfSelectedParticipant.Add(Me.chkLB_Participants.CheckedItems(cnt).ToString())
            Next
            ProgressThread.Close()
            Dim rptView As New frmReportViewer
            With rptView
                .LoadBIRCollectionReport(BRHelper.GenerateCollectionReportDatatable(New DSReport.BIRRulingMainDataTable, listOfSelectedParticipant), "DAILY")
                .ShowDialog()
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_ExportToExcel_Click(sender As Object, e As EventArgs) Handles btn_ExportToExcel.Click
        Try
            Dim _fldrSelect As New FolderBrowserDialog
            Dim ans As New MsgBoxResult
            Dim fPathName As String = ""
            Dim getDateFrom As Date = CDate(Me.ddl_TransDateFrom.SelectedItem)
            Dim getDateTo As Date = CDate(Me.txtBox_DateTo.Text)


            ans = MsgBox("Do you really want to export the records as xlsx Format?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Export records in Excel")
            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If
            With _fldrSelect
                .ShowDialog()
                If .SelectedPath = "" Then
                    Exit Sub
                End If

                ProgressThread.Show("Please wait while preparing the file.")

                fPathName = .SelectedPath

                'Get the selected participants
                For cnt As Integer = 0 To Me.chkLB_Participants.CheckedItems.Count - 1
                    Dim selectedParticipant As String = Me.chkLB_Participants.CheckedItems(cnt).ToString()
                    BRHelper.GenerateBIRCollectionReportExcel(selectedParticipant, fPathName, getDateFrom, getDateTo)
                Next

                ProgressThread.Close()
                MsgBox("Successfully downloaded!", MsgBoxStyle.Information, "Downloaded")
            End With

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ddl_TransDateFrom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_TransDateFrom.SelectedIndexChanged
        If ddl_TransDateFrom.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim selectedItemDate As Date = CDate(Me.ddl_TransDateFrom.SelectedItem)
        Dim getTransDateTo As Date = (From x In listOfBIRUling Where x.TransactionDateFrom = selectedItemDate Select x.TransactionDateTo).First
        Me.txtBox_DateTo.Text = getTransDateTo.ToShortDateString
    End Sub
End Class