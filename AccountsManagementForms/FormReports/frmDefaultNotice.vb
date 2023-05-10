'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmDefaultNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     October 10, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for viewing and generation of Default Notice with Attached SOA per Participant
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description

'
Option Explicit On
Option Strict On

Imports AccountsManagementLogic
Imports AccountsManagementObjects

Imports System.IO
Imports System.Windows.Forms
Imports CrystalDecisions.Shared

Public Class frmDefaultNotice

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private _BillParticipants As New List(Of AMParticipants)
    Private _WESMSummary As New List(Of WESMBillSummary)
    Private _CalendarBP As New List(Of CalendarBillingPeriod)
    Private _TransactionDate As Date

    Private _DefaultNoticeList As New List(Of DefaultNotice)
    Private _rptDefaultNotice As New DSReport.DefaultNoticeDataTable

    Private Sub frmDefaultNotice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm

            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            BFactory = BusinessFactory.GetInstance()
            _DefaultNoticeList = WBillHelper.GetDefaultNotice()
            _BillParticipants = WBillHelper.GetAMParticipantsAll()
            If _DefaultNoticeList.Count > 0 Then
                cbo_ParticipantID.DataSource = (From x In _DefaultNoticeList Select x.Participant.ParticipantID Distinct).ToList
                cmb_TransactionDate.DataSource = (From x In _DefaultNoticeList Select x.TransactionDate Distinct).ToList
                Me.cbox_TransDate.Checked = True
            Else
                MsgBox("No available report found!", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "System Message!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub GenerateAndSaveReports(ByVal isOverwrite As Boolean, ByVal ListofDNNumber As List(Of Long))
        Try
            ProgressThread.Show("Please wait while processing.")
            Dim _cnRefno As Long = 1

            'Create Lists
            Dim _DefaultNoticeList As New List(Of DefaultNotice)
            _WESMSummary = (From x In WBillHelper.GetWESMBillSummaryExcludeWTax() _
                            Where x.EndingBalance < 0 _
                            And x.EndingBalance <> x.EnergyWithhold _
                            And x.NoSOA = False _
                            And (x.ChargeType = EnumChargeType.E _
                                 Or x.ChargeType = EnumChargeType.MF _
                                 Or x.ChargeType = EnumChargeType.MFV) _
                            Select x).ToList

            Dim _dsParticipants = (From x In _WESMSummary _
                                   Select x.IDNumber.IDNumber Distinct).ToList

            _CalendarBP = WBillHelper.GetCalendarBP()
            _TransactionDate = CDate(dtp_TransactionDate.Value.ToShortDateString())
            For Each itmParticipant In _dsParticipants

                Dim _itmParticipant = itmParticipant

                Dim _DefaultNotice As New DefaultNotice
                Dim cParticipant = (From x In _BillParticipants _
                                    Where x.IDNumber = _itmParticipant _
                                    Select x).FirstOrDefault

                Dim getdWESMBillsPerParticipant = (From x In _WESMSummary _
                                                   Join z In _CalendarBP On x.BillPeriod Equals z.BillingPeriod _
                                                   Where x.IDNumber.IDNumber = _itmParticipant _
                                                   Select x, z.StartDate, z.EndDate).ToList()

                Dim GroupByList = (From x In getdWESMBillsPerParticipant _
                                   Group By x.x.IDNumber, x.x.BillingRemarks, x.StartDate, x.EndDate _
                                   Into TotalAmount = Sum(If(x.x.EndingBalance = x.x.BeginningBalance, x.x.EndingBalance, x.x.EndingBalance - x.x.EnergyWithhold)) _
                                   Select IDNumber, BillingRemarks, StartDate, EndDate, TotalAmount).ToList

                'Create Default notice
                With _DefaultNotice
                    .Participant = cParticipant
                    .TransactionDate = _TransactionDate
                    .UpdatedBy = AMModule.UserName
                    .TransactionDate = _TransactionDate
                    For Each itmWESMBillSummary In GroupByList
                        Dim itmDNDetails As New DefaultNoticeDetails
                        With itmDNDetails
                            .BillingPeriod = itmWESMBillSummary.StartDate.ToString("dd MMM.") & " to " & itmWESMBillSummary.EndDate.ToString("dd MMM. yyyy")
                            .Particulars = itmWESMBillSummary.BillingRemarks
                            .Amount = itmWESMBillSummary.TotalAmount
                        End With
                        .ListofDefaulNoticeDetails.Add(itmDNDetails)
                    Next
                End With
                _DefaultNoticeList.Add(_DefaultNotice)
                _cnRefno += 1
            Next

            Dim GenListSQL As List(Of String) = GenerateSQLForSaving(_DefaultNoticeList)

            WBillHelper.SaveDefaultNotice(GenListSQL, isOverwrite, ListofDNNumber, _TransactionDate)
            ProgressThread.Close()
            MsgBox("Report is successfully Saved to the database", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Success!")

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Function GenerateSQLForSaving(lstDefaultNotice As List(Of DefaultNotice)) As List(Of String)
        Dim ListSQL As New List(Of String)

        For Each item In lstDefaultNotice
            Dim DNSequence As Long = WBillHelper.GetSequenceID("SEQ_AM_DEFAULT_NOTICE_NO")
            Dim SQL As String = "INSERT INTO AM_DEFAULT_NOTICE(ID_NUMBER, UPDATED_DATE, DN_NUMBER, UPDATED_BY, TRANSACTION_DATE) " & _
                                "VALUES('" & item.Participant.IDNumber & "', SYSDATE, " & DNSequence & ", '" & AMModule.UserName & "', TO_DATE('" & item.TransactionDate & ",','mm/dd/yyyy hh24:mi:ss'))"
            ListSQL.Add(SQL)
            For Each oitem In item.ListofDefaulNoticeDetails
                Dim oSQL As String = "INSERT INTO AM_DEFAULT_NOTICE_DETAILS(BILLING_PERIOD, PARTICULARS, AMOUNT, DN_NUMBER) " & _
                                    "VALUES('" & oitem.BillingPeriod & "', '" & oitem.Particulars & "', " & oitem.Amount & ", " & DNSequence & ")"
                ListSQL.Add(oSQL)
            Next
        Next
        Return ListSQL
    End Function

    Private Sub GenerateReports()
        Dim _DNSignatories = WBillHelper.GetSignatories("DEFNOTICE").FirstOrDefault
        _rptDefaultNotice = New DSReport.DefaultNoticeDataTable

        'Get Participants checked
        Dim _lstParticipant As New List(Of String)
        For x = 0 To dgv_ViewDetails.Rows.Count - 1
            'get Participants for Default Notice
            If CBool(dgv_ViewDetails.Rows(x).Cells("chkReport").Value) = True Then
                _lstParticipant.Add(CStr(dgv_ViewDetails.Rows(x).Cells("IDNumber").Value.ToString))
            End If
        Next

        For Each itmParticipant In _lstParticipant
            Dim ListPerParticipant As List(Of DefaultNotice) = (From x In _DefaultNoticeList _
                                                          Where x.Participant.IDNumber = itmParticipant _
                                                          Select x).ToList()
            For Each item In ListPerParticipant
                With item
                    Dim GetParticipantInfo As AMParticipants = (From x In _BillParticipants Where x.IDNumber = item.Participant.IDNumber Select x).FirstOrDefault
                    For Each oitem In item.ListofDefaulNoticeDetails
                        Dim dnRow As DataRow
                        dnRow = _rptDefaultNotice.NewRow
                        dnRow("DEFAULT_DATE") = .TransactionDate
                        dnRow("ID_NUMBER") = GetParticipantInfo.IDNumber
                        dnRow("PARTICIPANT_ADDRESS") = GetParticipantInfo.ParticipantAddress
                        dnRow("PARTICIPANT_NAME") = GetParticipantInfo.FullName & " (" & GetParticipantInfo.ParticipantID.ToUpper & ")"
                        dnRow("PARTICIPANT_ID") = GetParticipantInfo.ParticipantID
                        dnRow("REP_POSITION") = GetParticipantInfo.Representative.Position
                        dnRow("REP_FULLNAME") = GetParticipantInfo.Representative.GetFullName
                        dnRow("REP_TSURNAME") = GetParticipantInfo.Representative.GetTitleSurname
                        dnRow("SIGNATORIES_1") = _DNSignatories.Signatory_1
                        dnRow("SIG1_POSITION") = _DNSignatories.Position_1
                        dnRow("SIGNATORIES_2") = _DNSignatories.Signatory_2
                        dnRow("SIG2_POSITION") = _DNSignatories.Position_2
                        dnRow("BILL_PERIOD") = oitem.BillingPeriod
                        dnRow("PARTICULARS") = oitem.Particulars
                        dnRow("AMOUNT") = oitem.Amount
                        _rptDefaultNotice.Rows.Add(dnRow)
                    Next
                End With
                _rptDefaultNotice.AcceptChanges()
                'End Default Notice Table
            Next
            'For Default Notice Table            
        Next

    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_GenerateDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateDefault.Click
        Try
            Dim ds As New DataSet
            Dim frmRPT As New frmReportViewer

            ProgressThread.Show("Please wait while generating.")
            Me.GenerateReports()

            If _rptDefaultNotice.Rows.Count = 0 Then
                ProgressThread.Close()
                MsgBox("Please select a record to Display.", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            ds.Tables.Clear()
            ds.Tables.Add(_rptDefaultNotice)
            Dim SystemDate As Date = WBillHelper.GetSystemDate

            With frmRPT
                .LoadDefaultNotice(ds, 0D, SystemDate)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim filesDestination As String
        Dim fOpen As New FolderBrowserDialog
        fOpen.ShowDialog()

        If fOpen.SelectedPath = "" Then
            MessageBox.Show("Please select file destination!", "Invalid Files Destination!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            filesDestination = fOpen.SelectedPath
        End If

        ProgressThread.Show("Please wait while extracting to " & filesDestination)

        Dim _DNSignatories = WBillHelper.GetSignatories("DEFNOTICE").FirstOrDefault
        _rptDefaultNotice = New DSReport.DefaultNoticeDataTable

        'Get Participants checked
        Dim _lstParticipant As New List(Of String)
        For x = 0 To dgv_ViewDetails.Rows.Count - 1
            'get Participants for Default Notice
            If CBool(dgv_ViewDetails.Rows(x).Cells("chkReport").Value) = True Then
                _lstParticipant.Add(CStr(dgv_ViewDetails.Rows(x).Cells("IDNumber").Value.ToString))
            End If
        Next

        For Each itmParticipant In _lstParticipant
            Dim ListPerParticipant As List(Of DefaultNotice) = (From x In _DefaultNoticeList _
                                                          Where x.Participant.IDNumber = itmParticipant _
                                                          Select x).ToList()
            Dim dtMain As New DSReport.DefaultNoticeDataTable

            For Each item In ListPerParticipant
                With item
                    Dim GetParticipantInfo As AMParticipants = (From x In _BillParticipants Where x.IDNumber = item.Participant.IDNumber Select x).FirstOrDefault
                    For Each oitem In item.ListofDefaulNoticeDetails
                        Dim dnRow As DataRow
                        dnRow = dtMain.NewRow
                        dnRow("DEFAULT_DATE") = .TransactionDate
                        dnRow("ID_NUMBER") = GetParticipantInfo.IDNumber
                        dnRow("PARTICIPANT_ADDRESS") = GetParticipantInfo.ParticipantAddress
                        dnRow("PARTICIPANT_NAME") = GetParticipantInfo.FullName & " (" & GetParticipantInfo.ParticipantID.ToUpper & ")"
                        dnRow("PARTICIPANT_ID") = GetParticipantInfo.ParticipantID
                        dnRow("REP_POSITION") = GetParticipantInfo.Representative.Position
                        dnRow("REP_FULLNAME") = GetParticipantInfo.Representative.GetFullName
                        dnRow("REP_TSURNAME") = GetParticipantInfo.Representative.GetTitleSurname
                        dnRow("SIGNATORIES_1") = _DNSignatories.Signatory_1
                        dnRow("SIG1_POSITION") = _DNSignatories.Position_1
                        dnRow("SIGNATORIES_2") = _DNSignatories.Signatory_2
                        dnRow("SIG2_POSITION") = _DNSignatories.Position_2
                        dnRow("BILL_PERIOD") = oitem.BillingPeriod
                        dnRow("PARTICULARS") = oitem.Particulars
                        dnRow("AMOUNT") = oitem.Amount
                        dtMain.Rows.Add(dnRow)
                    Next
                End With
                dtMain.AcceptChanges()
                Dim dsReport = New DataSet()
                dsReport.Tables.Add(dtMain)

                Dim selectedDate As Date = CDate(item.TransactionDate)
                Dim expReport = New RPTDefaultNotice
                expReport.SetDataSource(dsReport)
                expReport.SetParameterValue("paramCompanyAccount", AMModule.CompanyShortName.ToString)
                expReport.SetParameterValue("paramCompanyShortName", AMModule.CompanyBDOAccountName.ToString)
                expReport.ExportToDisk(ExportFormatType.PortableDocFormat, filesDestination & "\" & _
                                       item.Participant.IDNumber & "_DN_" & selectedDate.ToString("MMMddyyyy") & ".pdf")
                expReport.Close()
                expReport.Dispose()

                'End Default Notice Table
            Next
            'For Default Notice Table            
        Next
        ProgressThread.Close()
        MessageBox.Show("The DN Reports exported successfully.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmd_GenSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenSave.Click
        'Check if Reports are already existing in the database
        Try
            Dim _chkReports As List(Of DefaultNotice) = WBillHelper.GetDefaultNotice(dtp_TransactionDate.Value)
            Dim _isOverwrite As Boolean = False

            Dim ans1 As MsgBoxResult = MsgBox("Do you want to save the Default Notice as of " & FormatDateTime(dtp_TransactionDate.Value, DateFormat.ShortDate) & "?", CType(MsgBoxStyle.Information + MsgBoxStyle.YesNo, MsgBoxStyle), "Default Notice")
            If ans1 = MsgBoxResult.No Then
                Exit Sub
            End If

            If _chkReports.Count > 0 Then
                ProgressThread.Close()
                Dim ans As MsgBoxResult = MsgBox("There are already existing records as of Date " & FormatDateTime(dtp_TransactionDate.Value, DateFormat.ShortDate) & ", Do you want to overwrite?", CType(MsgBoxStyle.Information + MsgBoxStyle.YesNo, MsgBoxStyle), "Existing records found")
                If ans = MsgBoxResult.Yes Then                    
                    _isOverwrite = True
                Else
                    Exit Sub
                End If
            End If

            Dim listofDNNumbers As List(Of Long) = (From x In _chkReports Select x.DNNumber).Distinct.ToList()

            Me.GenerateAndSaveReports(_isOverwrite, listofDNNumbers)

            _DefaultNoticeList = WBillHelper.GetDefaultNotice(_TransactionDate)

            cBox_Participant.Checked = False
            cbox_TransDate.Checked = False

            Me.FillDataGrid()
        Catch ex As Exception            
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Function SearchRecords() As Boolean
        'Clear list first
        _DefaultNoticeList.Clear()

        Dim retBool As Boolean = False
        If Me.cbox_TransDate.Checked = True Then
            'Transaction Date Filter is selected
            If Me.cBox_Participant.Checked = True Then
                'If Both Filters are checked
                Dim _pIDNumber As String = (From x In _BillParticipants _
                                            Where x.ParticipantID = cbo_ParticipantID.SelectedItem.ToString _
                                            Select x.IDNumber).FirstOrDefault
                _DefaultNoticeList = Me.WBillHelper.GetDefaultNotice(_pIDNumber, CDate(cmb_TransactionDate.Text))

            Else
                'If Filter of Transaction date To and From is only Selected
                _DefaultNoticeList = Me.WBillHelper.GetDefaultNotice(CDate(cmb_TransactionDate.Text))
            End If

        ElseIf Me.cbox_AllParticipants.Checked = True Then
            Dim _pIDNumber As String = (From x In _BillParticipants _
                                        Where x.ParticipantID = cbo_ParticipantID.SelectedItem.ToString _
                                        Select x.IDNumber).FirstOrDefault
            'Transaction Date filter is not selected
            _DefaultNoticeList = Me.WBillHelper.GetDefaultNotice(_pIDNumber)
        End If

        If _DefaultNoticeList.Count > 0 Then
            retBool = True
        End If

        Return retBool
    End Function

    Private Sub cbox_TransDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_TransDate.CheckedChanged
        If Me.cbox_TransDate.Checked = True Then
            If cmb_TransactionDate.Items.Count = 0 Then
                MsgBox("No record available. Please generate first.")
                Exit Sub
            End If
            cmb_TransactionDate.Enabled = True
        Else
            cmb_TransactionDate.Enabled = False
        End If
    End Sub

    Private Sub cBox_Participant_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cBox_Participant.CheckedChanged
        If Me.cBox_Participant.Checked = True Then
            If cmb_TransactionDate.Items.Count = 0 Then
                MsgBox("No record available. Please generate first.")
                Exit Sub
            End If
            Me.cbo_ParticipantID.Enabled = True
        Else
            Me.cbo_ParticipantID.Enabled = False
        End If
    End Sub

    Private Sub cmd_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Search.Click
        Try
            If Me.cBox_Participant.Checked = False And Me.cbox_TransDate.Checked = False Then
                MsgBox("Please check the filters set then try again!", MsgBoxStyle.Critical, "Error!")
                Exit Sub
            End If

            If Me.SearchRecords = True Then
                Me.cbox_AllParticipants.Checked = False
                Me.FillDataGrid()
            Else
                MsgBox("There are no records found!", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try

    End Sub

    Private Sub FillDataGrid()
        dgv_ViewDetails.Rows.Clear()
        If _DefaultNoticeList.Count = 0 Then
            MsgBox("There are no records found!", MsgBoxStyle.Critical, "Error")
        Else
            cbo_ParticipantID.DataSource = (From x In _DefaultNoticeList Select x.Participant.ParticipantID Distinct).ToList
            'cmb_TransactionDate.DataSource = (From x In _DefaultNoticeList Select x.TransactionDate Distinct).ToList
            For Each itmDefault In _DefaultNoticeList
                Dim GrandTotal As Decimal = (From x In itmDefault.ListofDefaulNoticeDetails Select x.Amount).Sum()

                With itmDefault
                    dgv_ViewDetails.Rows.Add(.TransactionDate, .Participant.IDNumber, _
                                             .Participant.ParticipantID, FormatNumber(GrandTotal, 2, TriState.True, TriState.True), .DNNumber)
                End With
            Next
        End If
    End Sub

    Private Sub cbox_AllParticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_AllParticipants.CheckedChanged
        If Me.cbox_AllParticipants.Checked = True Then
            If Me.dgv_ViewDetails.RowCount <> 0 Then
                For x = 0 To Me.dgv_ViewDetails.RowCount - 1
                    Me.dgv_ViewDetails.Rows(x).Cells("chkReport").Value = True
                Next
            End If
        Else
            If Me.dgv_ViewDetails.RowCount <> 0 Then
                For x = 0 To Me.dgv_ViewDetails.RowCount - 1
                    Me.dgv_ViewDetails.Rows(x).Cells("chkReport").Value = False
                Next
            End If
        End If
    End Sub


End Class