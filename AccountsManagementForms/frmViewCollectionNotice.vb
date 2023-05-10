'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmViewCollectionNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     October 06, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for Viewing of the past and current Collection Notices per participant.
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS, AM_COLLECTION_NOTICE, AM_COLLECTION_NOTICE_DETAILS, AM_WESM_BILL
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   October   07, 2011      Juan Carlo L. Panopio               Added code in order to show full report even there's no
'                                                               previous balances found.
'   October   04, 2011      Juan Carlo L. Panopio               Added MF/MFV Fields in the Columns and report.

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmViewCollectionNotice
    Private WBillHelper As WESMBillHelper
    Private _InterestRate As Dictionary(Of Date, Decimal)
    Private _SearchState As Integer
    Private _ForReport As New List(Of CollectionNotice)
    Private _ForReportDetails As New List(Of CollectionNoticeDetails)

    Private Property SearchState() As Integer
        Get
            Return _SearchState
        End Get
        Set(ByVal value As Integer)
            Me._SearchState = value
        End Set
    End Property

    Private Sub frmViewCollectionNotice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        Try
            Dim CNParticipants = WBillHelper.GetCollectionNotice()
            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            Dim InterestRAtes = WBillHelper.GetDailyInterestRate()
            Dim ParticipantIds = (From x In AllParticipants Join y In CNParticipants On x.IDNumber Equals y.IDNumber Select x.ParticipantID Distinct).ToList
            If ParticipantIds.Count = 0 Then
                MsgBox("No Statement of Accounts found", MsgBoxStyle.Exclamation, "No Records Found")
                Me.Close()
            End If
            cbo_ParticipantId.DataSource = ParticipantIds
            If cbo_ParticipantId.Items.Count = 0 Then
                cmd_genReport.Enabled = False
            Else
                cmd_genReport.Enabled = True
            End If
            cmd_genReport.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cbo_ParticipantId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_ParticipantId.SelectedIndexChanged
        'Get Participant ID from Name
        SearchState = 1
        Try
            Dim ParticipantIds = WBillHelper.GetAMParticipantsAll()
            Dim ParticipantId = (From x In ParticipantIds Where x.ParticipantID = cbo_ParticipantId.SelectedItem.ToString Select x.IDNumber)
            Dim AllRefNo = WBillHelper.GetCollectionNotice()
            Dim ParticipantRefNo = (From x In AllRefNo Where x.IDNumber = CStr(ParticipantId.FirstOrDefault) Order By x.ReferenceNo Descending Select x.ReferenceNo).ToList
            cbo_RefNo.DataSource = ParticipantRefNo
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmd_search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_search.Click
        SearchState = 0
        Try
            'Get All Notices/Details
            Dim AllNotices = WBillHelper.GetCollectionNotice()
            Dim AllCollectionDetails = WBillHelper.GetCollectionNoticeDetails()

            Dim ForViewPrevious = (From x In AllCollectionDetails Where x.ReferenceNo = CLng(cbo_RefNo.SelectedItem.ToString) And _
                                   x.TransactionType = "P" Select x Order By x.BillingPeriod Ascending).ToList

            Dim ForViewCurrent = (From x In AllCollectionDetails Where x.ReferenceNo = CLng(cbo_RefNo.SelectedItem.ToString) And _
                                  x.TransactionType = "C" Select x).ToList

            Dim ForCNHeader = (From x In AllNotices _
                               Where x.ReferenceNo = CLng(cbo_RefNo.SelectedItem.ToString) _
                               Select x).FirstOrDefault

            DGV_Current.Rows.Clear()
            DGV_Previous.Rows.Clear()

            If ForViewCurrent.Count = 0 And ForViewCurrent.Count = 0 Then
                MsgBox("No Records found", MsgBoxStyle.Critical)
                cmd_genReport.Enabled = False
                Exit Sub
            Else
                cmd_genReport.Enabled = True
            End If

            _ForReport.Add(ForCNHeader)
            _ForReportDetails.AddRange(ForViewCurrent)
            _ForReportDetails.AddRange(ForViewPrevious)

            For Each prevItem In ForViewPrevious
                With prevItem
                    DGV_Previous.Rows.Add(.BillingPeriod, .INVDMCM, .InvoiceDate.ToString("MM/dd/yyyy"), .DueDate.ToString("MM/dd/yyyy"), .Energy, .VATEnergy, (CDec(.MF.ToString) + CDec(.VATMF.ToString)), .Total, .UpdatedBy, .UpdatedDate.ToString("MM/dd/yyyy"))
                End With
            Next

            For Each currItem In ForViewCurrent
                With currItem
                    DGV_Current.Rows.Add(.BillingPeriod, .INVDMCM, .InvoiceDate.ToString("MM/dd/yyyy"), .DueDate.ToString("MM/dd/yyyy"), .Energy, .VATEnergy, (CDec(.MF.ToString) + CDec(.VATMF.ToString)), .Total, .UpdatedBy, .UpdatedDate.ToString("MM/dd/yyyy"))
                End With
            Next


            cmd_genReport.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Refresh.Click
        DGV_Current.Rows.Clear()
        DGV_Previous.Rows.Clear()
        Dim CNParticipants = WBillHelper.GetCollectionNotice()
        Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
        Dim ParticipantIds = (From x In AllParticipants Join y In CNParticipants On x.IDNumber Equals y.IDNumber Select x.ParticipantID Distinct).ToList
        cbo_ParticipantId.DataSource = ParticipantIds
        cmd_genReport.Enabled = False
    End Sub

    Private Sub cmd_genReport_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_genReport.Click
        If SearchState = 1 Then
            MsgBox("Filter Values have changed, please click Search first", MsgBoxStyle.Critical)
            Exit Sub
        End If
        Try
            Dim Participants = WBillHelper.GetAMParticipantsAll()

            _InterestRate = WBillHelper.GetDailyInterestRate()

            Dim GetCollectionNoticedetails = WBillHelper.GetCollectionNoticeDetails(CLng(Me.cbo_RefNo.SelectedItem.ToString))
            Dim PreviousCollectionNoticeDetails = (From x In _ForReportDetails Where x.TransactionType = "P" _
                                                   Order By x.BillingPeriod).ToList
            Dim CurrentCollectionNoticeDetails = (From x In _ForReportDetails Where x.TransactionType = "C" _
                                                   Order By x.BillingPeriod).ToList
            Dim ForReportCurrentNotice = (From x In _ForReport Select x).ToList

            Dim _ParticipantID = (From x In ForReportCurrentNotice Select x.IDNumber).FirstOrDefault.ToString
            Participants = (From x In Participants _
                            Where x.IDNumber = CStr(_ParticipantID) _
                            Select x).ToList

            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(1)
            ParticipantMapping = (From x In ParticipantMapping Where x.IDNumber = CStr(_ParticipantID) And x.ParentFlag = 1).ToList

            'Assign Participant ID in current Notice
            'For Report
            For Each item In CurrentCollectionNoticeDetails
                item.ParticipantID = item.ParticipantID
                item.ParentId = CStr(_ParticipantID)
            Next

            For Each item In PreviousCollectionNoticeDetails
                item.ParticipantID = item.ParticipantID
                item.ParentId = CStr(_ParticipantID)
            Next

            'insert dummy data on List to be sent to Crystal Reports
            For Each item In ForReportCurrentNotice
                Dim tmpReferenceNo = item.ReferenceNo
                Dim tmpParticipantId = item.IDNumber
                Dim CheckIfPrevious = (From x In GetCollectionNoticedetails Where x.ReferenceNo = tmpReferenceNo And x.TransactionType = "P").ToList
                If CheckIfPrevious.Count = 0 Then
                    Dim ForDummyPrevious As New CollectionNoticeDetails
                    With ForDummyPrevious
                        .ReferenceNo = tmpReferenceNo
                        .ParticipantID = tmpParticipantId.ToString
                        .ParentId = tmpParticipantId.ToString
                        .INVDMCM = "0"
                        .Total = 0
                    End With
                    PreviousCollectionNoticeDetails.Add(ForDummyPrevious)
                End If
            Next

            Dim BillCalendar = WBillHelper.GetCalendarBP()
            Dim AllWESMbill = WBillHelper.GetWESMBills()

            'For Report
            Dim DateDue = (From x In CurrentCollectionNoticeDetails Select x.DueDate Distinct).FirstOrDefault.ToString
            'Dim ds = WBillHelper.SaveCollectionDataSet(Participants, ForReportCurrentNotice, CurrentCollectionNoticeDetails, PreviousCollectionNoticeDetails, BillCalendar)            
            Dim frmReport As New frmReportViewer
            With frmReport
                '.LoadWESMBillCollectionNotice(ds, CDate(DateDue), _InterestRate(CDate(FormatDateTime(SystemDate, DateFormat.ShortDate))))
                '.ShowDialog()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub txt_SearchNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SearchNo.GotFocus
        Me.txt_SearchNo.Text = ""
        Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Regular)
        Me.txt_SearchNo.ForeColor = Color.Black
    End Sub

    Private Sub txt_SearchNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_SearchNo.LostFocus
        If Me.txt_SearchNo.Text.Trim.Length = 0 Then
            Me.txt_SearchNo.Text = "Enter Collection Notice No."
            Me.txt_SearchNo.Font = New System.Drawing.Font("Helvetica", 8.5, FontStyle.Regular)
            Me.txt_SearchNo.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub btnSearchSOA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSOA.Click
        Try
            If Not IsNumeric(Me.txt_SearchNo.Text) Then
                MsgBox("Invalid Statement of Account number.", MsgBoxStyle.Critical, "Error Encountered")
                Exit Sub
            End If

            Dim _lstSOAHeader = Me.WBillHelper.GetCollectionNotice(CLng(Me.txt_SearchNo.Text))
            Dim _lstSOADetails = Me.WBillHelper.GetCollectionNoticeDetails((CLng(Me.txt_SearchNo.Text)))

            Dim ForViewPrevious = (From x In _lstSOADetails Where x.ReferenceNo = CLng(cbo_RefNo.SelectedItem.ToString) And _
                                   x.TransactionType = "P" Select x Order By x.BillingPeriod Ascending).ToList

            Dim ForViewCurrent = (From x In _lstSOADetails Where x.ReferenceNo = CLng(cbo_RefNo.SelectedItem.ToString) And _
                                  x.TransactionType = "C" Select x).ToList

            DGV_Current.Rows.Clear()
            DGV_Previous.Rows.Clear()

            If ForViewCurrent.Count = 0 And ForViewCurrent.Count = 0 Then
                MsgBox("No Records found", MsgBoxStyle.Critical)
                cmd_genReport.Enabled = False
                Exit Sub
            Else
                cmd_genReport.Enabled = True
            End If

            SearchState = 0
            _ForReport = _lstSOAHeader
            _ForReportDetails = _lstSOADetails

            For Each prevItem In ForViewPrevious
                With prevItem
                    DGV_Previous.Rows.Add(.BillingPeriod, .INVDMCM, .InvoiceDate.ToString("MM/dd/yyyy"), .DueDate.ToString("MM/dd/yyyy"), .Energy, .VATEnergy, (CDec(.MF.ToString) + CDec(.VATMF.ToString)), .Total, .UpdatedBy, .UpdatedDate.ToString("MM/dd/yyyy"))
                End With
            Next

            For Each currItem In ForViewCurrent
                With currItem
                    DGV_Current.Rows.Add(.BillingPeriod, .INVDMCM, .InvoiceDate.ToString("MM/dd/yyyy"), .DueDate.ToString("MM/dd/yyyy"), .Energy, .VATEnergy, (CDec(.MF.ToString) + CDec(.VATMF.ToString)), .Total, .UpdatedBy, .UpdatedDate.ToString("MM/dd/yyyy"))
                End With
            Next
            cmd_genReport.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

End Class