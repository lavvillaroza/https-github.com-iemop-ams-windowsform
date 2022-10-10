'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmReportCollectionNotice
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 29, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for the Generation of Collection Notice Report
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION_NOTICE, AM_COLLECTION_NOTICE_DETAILS, AM_WESM_BILL, 
'                        BILL_PARTICIPANTS
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   October 03, 2011     Juan Carlo L. Panopio         Completed form GUI and Generation per participant
'	October 03, 2011     Juan Carlo L. Panopio         Completed Generation for all Participants
'	October 07, 2011     Juan Carlo L. Panopio         Adjusted Report generation to not to show previous transactions
'                                                      with Zero (0) Balance in AM_WESM_BILL
'	October 10, 2011     Juan Carlo L. Panopio         Added a method in order to generate report even there's no 
'                                                      Previous Balances of the participant.
'   November 24, 2011    Juan Carlo L. Panopio         Removed old code in populating Previous Collection Notice Details,
'                                                      Changed Previous Collection Notice Details from Per Invoice to Per Summary.
'                                                      Added Getting Participant Lineage in the chosen Billing Period.
'                                                      Added Code to exclude POSITIVE VALUES in Energy in the Collection Notice
'   November 30, 2011   Juan Carlo L. Panopio          Modified Collection Notice Generation according to New Parent-Child Mapping of Participants
'                                                      Participants Listed on Table/Dropdown will be all PARENT only and All Participants
'                                                      not yet assigned will not have any Collection Notice.
'   December 3, 2011    Juan Carlo L. Panopio          Finished Collection Notice Preparation - All Participants, Single Participant Generation
'   March 12, 2012      Juan Carlo L. Panopio          Changed frmCollectionNotice to frmcollectionNoticeMgt for Adding remarks and new form design
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports LDAPControl
Imports LDAPLogin

'For Crystal Reports Export to PDF
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared



Public Class frmRPTCollectionNoticeMgt
    Private _state As String
    Public Property state() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
        End Set
    End Property

    Private _IsSaved As Boolean
    Public Property IsSaved() As Boolean
        Get
            Return _IsSaved
        End Get
        Set(ByVal value As Boolean)
            _IsSaved = value
        End Set
    End Property


    Dim WBillHelper As WESMBillHelper
    Private dicParticipantToMPID As New Dictionary(Of String, String)
    Private dicMPIDtoSOANumber As New Dictionary(Of String, Long)
    Private lstForUpdate As New List(Of CollectionNotice)

    Private lstCNHeader As New List(Of CollectionNotice)
    Private lstCNprevDetails As New List(Of CollectionNoticeDetails)
    Private lstCNcurrDetails As New List(Of CollectionNoticeDetails)
    Private lstWESMSummary As New List(Of WESMBillSummary)

    Private CNDueDate As New Date
    Private CollectionNoticeNumber As New Long
    Private destPDFPath As String

    Private BFactory As New BusinessFactory
    'get Current Interest Rate
    Private _tmpInterestRate As Dictionary(Of Date, Decimal)
    Public _GenerateFlag As EnumCNFlag

#Region "Form Functions"
    'save to database
    Private Sub cmd_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        Dim ans As New MsgBoxResult
        Try

        
            If state = "UPDATED" Then

                If clb_participants.SelectedItems.Count = 0 Then
                    MsgBox("Please select participant/s for updating/saving.", MsgBoxStyle.Critical, "Error!")
                    Exit Sub
                End If

                ans = MsgBox("Do you want to Save the Collection Notices?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Save Collection Notice")
                If ans = MsgBoxResult.Yes Then
                    'Update Remarks in collection notice
                    For Each item In lstCNHeader
                        Dim cParticipant = item.IDNumber
                        Dim _updRemarks = (From x In lstForUpdate _
                                           Where x.IDNumber = cParticipant _
                                           Select x).ToList
                        If _updRemarks.Count <> 0 Then
                            item.Remarks = _updRemarks.First.Remarks
                        Else
                            item.Remarks = txt_remarks.Text
                        End If
                        item.DueDate = CDate(FormatDateTime(CDate(cbo_dueDate.SelectedItem.ToString), DateFormat.ShortDate))
                    Next
                    lstForUpdate.Clear()
                    WBillHelper.SaveCollectionNotice(lstCNHeader, lstCNcurrDetails, lstCNprevDetails)
                    MsgBox("Save Completed!", MsgBoxStyle.Information, "Success!")

                    Dim _CurSelected As Integer = cbo_dueDate.SelectedIndex
                    Me.cmd_refresh_Click(Nothing, Nothing)
                    Me.cbo_dueDate.SelectedIndex = _CurSelected


                    cmd_save.Enabled = False
                End If
            Else
                MsgBox("Search Values have changed, please Search again", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Failed, "Error encountered: " & ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        If lstForUpdate.Count > 0 And _IsSaved = False Then
            Dim ans As New MsgBoxResult
            ans = MsgBox("Do you really want to discard the Collection Notices made?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Discard Changes")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        Me.Close()
    End Sub

    'Toggle Of CheckBox All Participants
    Private Sub chk_allParticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_allParticipants.CheckedChanged
        If Me.chk_allParticipants.Checked Then
            For i As Integer = 0 To clb_participants.Items.Count - 1
                clb_participants.SetItemChecked(i, True)
            Next

            'If checked, disable check list box
            clb_participants.Enabled = False

            'Enable remarks
            txt_remarks.Enabled = True
        Else
            For i = 0 To clb_participants.Items.Count - 1
                clb_participants.SetItemChecked(i, False)
            Next

            'If not checked, enable check list box
            clb_participants.Enabled = True

            'Disable remarks
            txt_remarks.Enabled = False
        End If
    End Sub

    'Load Participant List in Check box List
    Public Sub LoadParticipants(ByRef CNHeader As List(Of CollectionNotice))
        Try
            Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = "JCLP"

            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            Dim joinParticipants = (From x In AllParticipants _
                                    Join y In CNHeader _
                                    On x.IDNumber Equals y.IDNumber _
                                    Select x.ParticipantID).ToList
            clb_participants.Items.Clear()
            For Each item In joinParticipants
                clb_participants.Items.Add(item.ToString)
            Next
            Me.clb_participants.SelectedIndex = 0
            Me.LoadParticipantsToDictionary(AllParticipants)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Failed, "Error Encountered: " & ex.Message)
            Exit Sub
        End Try
    End Sub

    'Assign Participants to Dictionary
    Private Sub LoadParticipantsToDictionary(ByVal lstAllParticipants As List(Of AMParticipants))
        dicParticipantToMPID.Clear()
        For Each item In lstAllParticipants
            dicParticipantToMPID.Add(item.ParticipantID, CStr(item.IDNumber))
        Next
    End Sub

    'On Leave of focus on Remarks Save selected item to list
    Private Sub txt_remarks_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_remarks.LostFocus
        'Continuously add to list and validate upon saving
        If Not chk_allParticipants.Checked Then
            If clb_participants.GetItemCheckState(clb_participants.SelectedIndex) = CheckState.Checked And Trim(txt_remarks.Text) <> "" Then
                'If the selected item is Checked and the Remarks is not blank
                Dim selForUpdate As New CollectionNotice
                selForUpdate.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString)
                selForUpdate.Remarks = txt_remarks.Text

                If lstForUpdate.Exists(Function(a) a.IDNumber = selForUpdate.IDNumber) = False Then
                    lstForUpdate.Add(selForUpdate)
                Else
                    Dim getPrevious = (From x In lstForUpdate Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) _
                                      Select x).ToList

                    If getPrevious.Count <> 0 Then
                        lstForUpdate.Remove(getPrevious.FirstOrDefault)
                        lstForUpdate.Add(selForUpdate)
                    End If
                End If
            ElseIf Trim(txt_remarks.Text) <> "" Then
                'Uncheck if the remarks is blank
                clb_participants.SetItemCheckState(clb_participants.SelectedIndex, CheckState.Unchecked)
            End If
        End If
    End Sub

    Private Sub cmd_browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_browse.Click
        Dim folderSelection As New FolderBrowserDialog
        With folderSelection
            .ShowDialog()
            txt_FilePath.Text = .SelectedPath
            destPDFPath = .SelectedPath
        End With
    End Sub

    Private Sub cmd_PDFExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_PDFExport.Click
        Try


            If Me.txt_FilePath.Text.Length = 0 Then
                MsgBox("Please browse for a destination folder first", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim frmProg As New frmProgress

            frmProg.Show()
            frmProg.Text = "Exporting to PDF"
            frmProg.Label1.Text = "Please Wait"

            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            Dim forPreviewCNPrev As New List(Of CollectionNoticeDetails)
            Dim currBillPeriod As Integer = CInt((From x In WESMSummary _
                                                    Select x.BillPeriod _
                                                    Order By BillPeriod Descending).FirstOrDefault.ToString)

            Dim BillCalendar = WBillHelper.GetCalendarBP()
            Dim forPreviewCurrent As New List(Of CollectionNoticeDetails)
            Dim forFilteringHeader As New List(Of CollectionNotice)
            If clb_participants.CheckedIndices.Count > 0 Then
                If clb_participants.CheckedIndices.Count = clb_participants.Items.Count Then
                    'if all participants are checked
                    forFilteringHeader = lstCNHeader
                Else
                    'If there are checked items
                    For Each rec In clb_participants.CheckedIndices
                        Dim ParticipantId = dicParticipantToMPID(clb_participants.Items(CInt(rec.ToString)).ToString)
                        Dim forFilter = (From x In lstCNHeader _
                                         Where x.IDNumber = ParticipantId _
                                         Select x).FirstOrDefault
                        forFilteringHeader.Add(forFilter)
                    Next
                End If
            ElseIf chk_allParticipants.Checked = False Then
                'if theres no checked items
                forFilteringHeader = (From x In lstCNHeader _
                               Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) _
                               Select x).ToList
            End If

            For Each item In forFilteringHeader
                Dim cParticipant = item.IDNumber
                Dim cRefNo = item.ReferenceNo
                Dim ChangeReferenceNumbers = (From x In lstCNprevDetails _
                                              Where x.ParentId = CStr(cParticipant) _
                                              Select x).ToList

                If ChangeReferenceNumbers.Count <> 0 Then
                    For Each details In ChangeReferenceNumbers
                        details.ReferenceNo = item.ReferenceNo
                        forPreviewCNPrev.Add(details)
                    Next
                Else
                    Dim dmyPrevious As New CollectionNoticeDetails
                    With dmyPrevious
                        .ParentId = CStr(cParticipant)
                        .ParticipantID = Me.GetMPIDMPName(cParticipant, AllParticipants)
                        .ReferenceNo = item.ReferenceNo
                        .INVDMCM = "0"
                        .TransactionType = "P"
                        .Energy = 0
                        .VATEnergy = 0
                        .MF = 0
                        .Total = 0
                    End With
                    forPreviewCNPrev.Add(dmyPrevious)
                End If

                forPreviewCurrent.AddRange((From x In lstCNcurrDetails _
                                         Where x.ReferenceNo = cRefNo _
                                         Select x).ToList)

                'Place remarks
                Dim getRemarks = (From x In lstForUpdate _
                                  Where x.IDNumber = cParticipant _
                                  Select x).ToList

                If getRemarks.Count <> 0 Then
                    item.Remarks = getRemarks.First.Remarks
                Else
                    item.Remarks = txt_remarks.Text
                End If

                Dim GetLst = (From x In forFilteringHeader _
                              Where x.ReferenceNo = cRefNo _
                              And x.IDNumber = cParticipant _
                              Select x).ToList

                'Dim forRPTCollectionNotice = WBillHelper.SaveCollectionDataSet(AllParticipants, GetLst, _
                '                                                   forPreviewCurrent, forPreviewCNPrev, _
                '                                                   BillCalendar)
                CNDueDate = CDate(cbo_dueDate.SelectedItem)
                'Dim expReport As RPTCollectionNotice = New RPTCollectionNotice
                'expReport.SetDataSource(forRPTCollectionNotice)
                'expReport.SetParameterValue("paramDueDate", CNDueDate)
                'expReport.ExportToDisk(ExportFormatType.PortableDocFormat, Me.txt_FilePath.Text & "\STATEMENT_OF_ACCOUNT_" & item.IDNumber & "_" & CNDueDate.ToString("yyyyMdd") & ".pdf")
            Next

            frmProg.Close()
            MsgBox("Successfully exported Collection Notices to PDF", MsgBoxStyle.Information, "Export Collection Notice")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Successful, "Successfully exported Statement of account/s to PDF")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Failed, "Error Encountered: " & ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_GenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_GenerateReport.Click
        Try
            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            Dim forPreviewCNPrev As New List(Of CollectionNoticeDetails)
            Dim currBillPeriod As Integer = CInt((From x In WESMSummary _
                                                    Select x.BillPeriod _
                                                    Order By BillPeriod Descending).FirstOrDefault.ToString)
            Dim BillCalendar = WBillHelper.GetCalendarBP()
            Dim forPreviewCurrent As New List(Of CollectionNoticeDetails)
            Dim forFilteringHeader As New List(Of CollectionNotice)

            If clb_participants.CheckedIndices.Count > 0 Then
                If clb_participants.CheckedIndices.Count = clb_participants.Items.Count Then
                    'if all participants are checked
                    forFilteringHeader = lstCNHeader
                Else
                    'If there are checked items
                    For Each rec In clb_participants.CheckedIndices
                        Dim ParticipantId = dicParticipantToMPID(clb_participants.Items(CInt(rec.ToString)).ToString)
                        Dim forFilter = (From x In lstCNHeader _
                                         Where x.IDNumber = ParticipantId _
                                         Select x).FirstOrDefault
                        forFilteringHeader.Add(forFilter)
                    Next
                End If
            ElseIf chk_allParticipants.Checked = False Then
                'if theres no checked items
                forFilteringHeader = (From x In lstCNHeader _
                                      Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) _
                                      Select x).ToList
            End If

            For Each item In forFilteringHeader
                Dim cParticipant = item.IDNumber
                Dim ChangeReferenceNumbers = (From x In lstCNprevDetails _
                                              Where x.ParentId = CStr(cParticipant) _
                                              Select x).ToList

                If ChangeReferenceNumbers.Count <> 0 Then
                    For Each details In ChangeReferenceNumbers
                        details.ReferenceNo = item.ReferenceNo
                        forPreviewCNPrev.Add(details)
                    Next
                Else
                    Dim dmyPrevious As New CollectionNoticeDetails
                    With dmyPrevious
                        .ParentId = CStr(cParticipant)
                        .ParticipantID = Me.GetMPIDMPName(cParticipant, AllParticipants)
                        .ReferenceNo = item.ReferenceNo
                        .INVDMCM = "0"
                        .TransactionType = "P"
                        .Energy = 0
                        .VATEnergy = 0
                        .MF = 0
                        .Total = 0
                    End With
                    forPreviewCNPrev.Add(dmyPrevious)
                End If

                forPreviewCurrent.AddRange((From x In lstCNcurrDetails _
                                         Where x.ParentId = CStr(cParticipant) _
                                         Select x).ToList)

                'Place remarks
                Dim getRemarks = (From x In lstForUpdate _
                                  Where x.IDNumber = cParticipant _
                                  Select x).ToList

                If getRemarks.Count <> 0 Then
                    item.Remarks = getRemarks.First.Remarks
                Else
                    item.Remarks = txt_remarks.Text
                End If
            Next

            Dim forRPTCollectionNotice = WBillHelper.SaveCollectionDataSet(AllParticipants, forFilteringHeader, _
                                                                           forPreviewCurrent, forPreviewCNPrev)

            CNDueDate = CDate(cbo_dueDate.SelectedItem)
            'Generate Collection Notices for each participant
            Dim viewCNReport As New frmReportViewer
            frmProgress.Show()
            With viewCNReport
                .LoadWESMBillCollectionNotice(forRPTCollectionNotice, CNDueDate, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Failed, "Error Encountered: " & ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub frmRPTCollectionNoticeMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = "JCLP"

            Dim AllWESMBillSummary = WBillHelper.GetWESMBillSummary()
            BFactory = BusinessFactory.GetInstance()
            Me._GenerateFlag = EnumCNFlag.BeforeCollection
            _tmpInterestRate = WBillHelper.GetDailyInterestRate
            Dim getDueDates = (From x In AllWESMBillSummary _
                               Select x.DueDate Distinct Order By DueDate Descending).ToList

            Dim lstDates As New List(Of Date)
            Me.CollectionNoticeNumber = 1

            For Each itmGetDueDates In getDueDates
                Dim _itmDueDate = itmGetDueDates
                Dim _chkOutstanding = (From x In AllWESMBillSummary _
                                       Where x.DueDate = _itmDueDate _
                                       And x.EndingBalance < 0 _
                                       Select x).Count

                If _chkOutstanding > 0 Then
                    lstDates.Add(_itmDueDate)
                End If
            Next

            cbo_dueDate.DataSource = lstDates
            If cbo_dueDate.Items.Count = 0 Then
                MsgBox("No WESM Bill Summaries found", MsgBoxStyle.Critical, "Error!")
                cmd_save.Enabled = False
                cmd_GenerateReport.Enabled = False
                cmd_PDFExport.Enabled = False
                Exit Sub
            End If
            Me.GenerateCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_StatementOfAccountWindow.ToString, eLogsStatus.Failed, "Error Encountered: " & ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub clb_ParticipantsCheck(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clb_participants.ItemCheck
        If clb_participants.GetItemCheckState(clb_participants.SelectedIndex) = CheckState.Checked Then
            txt_remarks.Enabled = False
            Dim getDetails = (From x In lstForUpdate Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) Select x).ToList
            If getDetails.Count <> 0 Then
                lstForUpdate.Remove(getDetails.FirstOrDefault)
            End If
            txt_remarks.Text = ""
        Else
            If lstForUpdate.Count <> 0 Then
                Dim GetRemarks = (From x In lstForUpdate Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) Select x.Remarks).ToList
                If GetRemarks.Count <> 0 Then
                    txt_remarks.Text = GetRemarks.FirstOrDefault.ToString
                Else
                    txt_remarks.Text = ""
                End If

            End If
            txt_remarks.Enabled = True
        End If
    End Sub

    Private Sub clb_participants_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clb_participants.SelectedIndexChanged
        If clb_participants.GetItemCheckState(clb_participants.SelectedIndex) = CheckState.Checked Then
            If lstForUpdate.Count <> 0 Then
                Dim GetRemarks = (From x In lstForUpdate Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) Select x.Remarks).ToList
                If GetRemarks.Count <> 0 Then
                    txt_remarks.Text = GetRemarks.FirstOrDefault.ToString
                Else
                    txt_remarks.Text = ""
                End If
            End If
            txt_remarks.Enabled = True
        Else
            txt_remarks.Enabled = False
            lstForUpdate.Remove((From x In lstForUpdate Where x.IDNumber = dicParticipantToMPID(clb_participants.SelectedItem.ToString) Select x).FirstOrDefault)
            txt_remarks.Text = ""
        End If

        If IsSaved = True Then
            txt_remarks.Enabled = False
        Else
            txt_remarks.Enabled = True
        End If

    End Sub

    Private Sub cmd_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_refresh.Click
        Try
            Dim AllWESMBillSummary = WBillHelper.GetWESMBillSummary()

            If AllWESMBillSummary.Count = 0 Then
                MsgBox("No WESM Bill Summaries found", MsgBoxStyle.Critical, "Collection Notice")
                cmd_save.Enabled = False
                cmd_GenerateReport.Enabled = False
                cmd_PDFExport.Enabled = False
                Exit Sub
            Else
                cmd_save.Enabled = True
                cmd_GenerateReport.Enabled = True
                cmd_PDFExport.Enabled = True
            End If

            Dim getDueDates = (From x In AllWESMBillSummary _
                               Select x.DueDate Distinct Order By DueDate Descending).ToList
            Dim lstDates As New List(Of Date)

            For Each itmGetDueDates In getDueDates
                Dim _itmDueDate = itmGetDueDates
                Dim _chkOutstanding = (From x In AllWESMBillSummary _
                                       Where x.DueDate = _itmDueDate _
                                       And x.EndingBalance < 0 _
                                       Select x).Count

                If _chkOutstanding > 0 Then
                    lstDates.Add(_itmDueDate)
                End If
            Next

            cbo_dueDate.DataSource = lstDates

            lstCNHeader.Clear()
            lstCNprevDetails.Clear()
            lstCNcurrDetails.Clear()
            lstWESMSummary.Clear()

            Me.GenerateCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))
            state = "UPDATED"
            chk_allParticipants.Checked = False
            txt_remarks.Enabled = False
            clb_participants.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub cbo_dueDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_dueDate.SelectedIndexChanged
        Try
            chk_allParticipants.Checked = False
            txt_remarks.Enabled = False
            clb_participants.Enabled = True

            lstCNcurrDetails.Clear()
            lstCNHeader.Clear()
            lstCNprevDetails.Clear()
            lstForUpdate.Clear()

            Me.clb_participants.Items.Clear()

            Dim chkDueDate As Boolean = False

            chkDueDate = Me.CheckDueDate(CDate(cbo_dueDate.SelectedItem.ToString))

            If chkDueDate = True Then
                Me.cmd_save.Enabled = False
                Me.chk_allParticipants.Enabled = False
                Me.txt_remarks.ReadOnly = True
                _IsSaved = True
                Me.GenerateCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))
            Else
                Me.cmd_save.Enabled = True
                Me.chk_allParticipants.Enabled = True
                Me.txt_remarks.ReadOnly = False
                _IsSaved = False
                Me.GenerateCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))
            End If

            Me.state = "UPDATED"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Collection Notice Functions"
    Private Function GenerateSOA() As List(Of CollectionNotice)
        Dim retSOA As New List(Of CollectionNotice)

        'Get WESM Bill Summaries
        Dim AllSummary As New List(Of WESMBillSummary)
        AllSummary = Me.WBillHelper.GetWESMBillSummary()

        'Get All Participants
        Dim SummaryParticipants As New List(Of String)
        SummaryParticipants = (From x In AllSummary _
                               Select x.IDNumber.IDNumber).ToList

        'Get Sum of Participants (if negative include in list for generation of SOA
        For Each itmParicipant In SummaryParticipants

            Me.CollectionNoticeNumber += 1
        Next

        Return retSOA
    End Function

    Private Function GenerateSOADetails() As List(Of CollectionNoticeDetails)
        Dim retSOADetails As New List(Of CollectionNoticeDetails)

        Return retSOADetails
    End Function
#End Region

#Region "COLLECTION NOTICE FUNCTIONS"

    Private Sub GenerateCollectionNotice(ByVal DateDue As Date)
        Try
            'For saving to Database 
            ' CNHeader - Collection Notice Header
            lstForUpdate.Clear()
            'Dictionary for Participant-ReferenceNo
            Dim dicReferenceNo As New Dictionary(Of String, Long)
            dicReferenceNo.Add("0", 0)
            Dim RefNo As Integer = 1
            'Get Document Signatories
            Dim _SignatoriesCN = WBillHelper.GetSignatories("CN").First
            'Get DMCM Details
            Dim DMCMDetails = WBillHelper.GetDebitCreditMemoDetails()
            'Get All Participants
            Dim AllParticipants = WBillHelper.GetAMParticipantsAll()
            'Get WESMBillsSummary
            Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            lstWESMSummary = WESMSummary
            'Get Offsetting Details
            Dim Offsetdetails = WBillHelper.GetWESMBillOffsetDetails()
            'Get Current BP
            Dim currBillPeriod As Integer = CInt((From x In WESMSummary _
                                                  Where x.DueDate = DateDue _
                                                 Select x.BillPeriod _
                                                 Order By BillPeriod Descending).FirstOrDefault.ToString)

            If _IsSaved = True Then
                lstCNprevDetails.Clear()
                lstCNcurrDetails.Clear()
                lstCNHeader.Clear()

                Me.lstCNHeader = WBillHelper.GetCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))

                Dim lstRefNo As New List(Of String)

                For Each itmHeader In lstCNHeader
                    lstRefNo.Add(itmHeader.ReferenceNo.ToString)
                Next

                Dim _lstCNDetails = WBillHelper.GetCollectionNoticeDetails(lstRefNo)

                Me.lstCNprevDetails = _lstCNDetails.Where(Function(x) x.TransactionType = "P").ToList
                Me.lstCNcurrDetails = _lstCNDetails.Where(Function(x) x.TransactionType = "C").ToList

                Me.LoadParticipants(lstCNHeader)
                lstForUpdate = Me.lstCNHeader

                Exit Sub
            End If

            Dim currDueDate = DateDue
            'Get Calendar Billing Period
            Dim BillCalendar = WBillHelper.GetCalendarBP(currBillPeriod)
            'Get All WESM Bills
            Dim AllWESMBills = WBillHelper.GetWESMBills()
            'Get Current Invoices
            Dim cWESMBills = (From x In AllWESMBills _
                              Where x.DueDate = currDueDate).ToList
            'Get Current WESM Bill Participants
            Dim currParticipants As New List(Of AMParticipants)
            currParticipants = (From x In AllWESMBills _
                                Join y In AllParticipants _
                                On CStr(x.IDNumber) Equals y.IDNumber _
                                Where x.DueDate = currDueDate _
                                Select y Distinct _
                                Order By y.ParticipantID Ascending).ToList

            Dim SelectedParticipants As New List(Of AMParticipants)
            Dim cParticipants As New List(Of AMParticipants)

            'Prepare Participants with Collection Notice
            SelectedParticipants = currParticipants

            'Generate CN Header
            Dim CNHeader As New List(Of CollectionNotice)
            CNHeader.AddRange(Me.GetCollectionNoticeHeader(currDueDate, WESMSummary, _SignatoriesCN))

            'Add Reference Numbers to Dictionary
            For Each item In CNHeader
                dicReferenceNo.Add(item.IDNumber, CLng(item.ReferenceNo))
            Next

            Dim prevBills As New List(Of CollectionNoticeDetails)
            Dim currBills As New List(Of CollectionNoticeDetails)

            'Loop all Headers
            For Each item In CNHeader
                'Get Previous Summary of Participant
                Dim cParticipant = item.IDNumber
                Dim preSummary = (From x In WESMSummary _
                                 Where x.DueDate < currDueDate _
                                 And x.IDNumber.IDNumber = cParticipant _
                                 And x.EndingBalance < 0 _
                                 And x.EndingBalance <> 0 _
                                 Select x Order By x.BillPeriod Ascending, x.ChargeType Ascending).ToList

                'Check sum of Energy
                Dim chkEnergySum = (From x In preSummary _
                                    Where x.ChargeType = EnumChargeType.E _
                                    Select x.EndingBalance).Sum

                'Check sum of VAT on Energy
                Dim chkVATSum = (From x In preSummary _
                                 Where x.ChargeType = EnumChargeType.EV _
                                 Select x.EndingBalance).Sum

                'Check sum of MF and VAT on MF
                Dim chkMFSum = (From x In preSummary _
                                Where x.ChargeType = EnumChargeType.MF _
                                Or x.ChargeType = EnumChargeType.MFV _
                                Select x.EndingBalance).Sum

                If chkEnergySum < 0 Then
                    Dim EnergySummary = (From x In preSummary _
                                         Where x.ChargeType = EnumChargeType.E _
                                         Select x).ToList
                    prevBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                                             EnergySummary, dicReferenceNo, DMCMDetails, "P", , AllWESMBills))
                End If

                If chkVATSum < 0 Then
                    Dim VATSummary = (From x In preSummary _
                                         Where x.ChargeType = EnumChargeType.EV _
                                         Select x).ToList
                    prevBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                                             VATSummary, dicReferenceNo, DMCMDetails, "P", , AllWESMBills))
                End If

                If chkMFSum < 0 Then
                    Dim MFSummary = (From x In preSummary _
                                         Where x.ChargeType = EnumChargeType.MF _
                                         Or x.ChargeType = EnumChargeType.MFV _
                                         Select x).ToList
                    prevBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                                             MFSummary, dicReferenceNo, DMCMDetails, "P", , AllWESMBills))
                End If

                'Get Current Bills
                'Get Invoices First
                Dim curSummary = (From x In WESMSummary _
                                  Where x.DueDate = currDueDate _
                                  And x.IDNumber.IDNumber = cParticipant _
                                  And x.SummaryType = EnumSummaryType.INV _
                                  Select x Order By x.ChargeType Ascending).ToList

                Dim curSumMF = (From x In curSummary _
                                Where x.ChargeType = EnumChargeType.MF _
                                Or x.ChargeType = EnumChargeType.MFV _
                                And x.SummaryType = EnumSummaryType.INV _
                                Select x.EndingBalance).Sum

                If curSumMF < 0 Then
                    Dim chkMarketFees = (From x In curSummary _
                                     Where x.ChargeType = EnumChargeType.MF _
                                     Or x.ChargeType = EnumChargeType.MFV _
                                     And x.SummaryType = EnumSummaryType.INV _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkMarketFees, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If


                Dim chkSumEnergy = (From x In curSummary _
                                    Where x.ChargeType = EnumChargeType.E _
                                    And x.SummaryType = EnumSummaryType.INV _
                                    Select x.EndingBalance).Sum

                If chkSumEnergy < 0 Then
                    Dim chkEnergy = (From x In curSummary _
                                     Where x.ChargeType = EnumChargeType.E _
                                     And x.SummaryType = EnumSummaryType.INV _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkEnergy, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If

                Dim chkSumVATEnergy = (From x In curSummary _
                                    Where x.ChargeType = EnumChargeType.EV _
                                    And x.SummaryType = EnumSummaryType.INV _
                                    Select x.EndingBalance).Sum

                If chkSumVATEnergy < 0 Then
                    Dim chkVATEnergy = (From x In curSummary _
                                     Where x.ChargeType = EnumChargeType.EV _
                                     And x.SummaryType = EnumSummaryType.INV _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkVATEnergy, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If



                Dim curSummaryDMCM = (From x In WESMSummary _
                                      Where x.DueDate = currDueDate _
                                      And x.IDNumber.IDNumber = cParticipant _
                                      And x.SummaryType = EnumSummaryType.DMCM _
                                      Select x Order By x.ChargeType Ascending).ToList


                Dim curSumMFDMCM = (From x In curSummaryDMCM _
                                Where (x.ChargeType = EnumChargeType.MF _
                                Or x.ChargeType = EnumChargeType.MFV) _
                                And x.SummaryType = EnumSummaryType.DMCM _
                                Select x.EndingBalance).Sum

                If curSumMFDMCM < 0 Then
                    Dim chkMarketFeesDMCM = (From x In curSummaryDMCM _
                                     Where (x.ChargeType = EnumChargeType.MF _
                                     Or x.ChargeType = EnumChargeType.MFV) _
                                     And x.SummaryType = EnumSummaryType.DMCM _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkMarketFeesDMCM, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If


                Dim chkSumEnergyDMCM = (From x In curSummaryDMCM _
                                    Where x.ChargeType = EnumChargeType.E _
                                    And x.SummaryType = EnumSummaryType.DMCM _
                                    Select x.EndingBalance).Sum

                If chkSumEnergyDMCM < 0 Then
                    Dim chkEnergyDMCM = (From x In curSummaryDMCM _
                                     Where x.ChargeType = EnumChargeType.E _
                                     And x.SummaryType = EnumSummaryType.DMCM _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkEnergyDMCM, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If

                Dim chkSumVATEnergyDMCM = (From x In curSummaryDMCM _
                                    Where x.ChargeType = EnumChargeType.EV _
                                    And x.SummaryType = EnumSummaryType.DMCM _
                                    Select x.EndingBalance).Sum

                If chkSumVATEnergyDMCM < 0 Then
                    Dim chkVATEnergyDMCM = (From x In curSummaryDMCM _
                                     Where x.ChargeType = EnumChargeType.EV _
                                     And x.SummaryType = EnumSummaryType.DMCM _
                                     Select x).ToList
                    currBills.AddRange(Me.GetCollectionNoticeDetailsFromSummary(AllParticipants, CStr(cParticipant), currDueDate, _
                                        chkVATEnergyDMCM, dicReferenceNo, DMCMDetails, "C", , AllWESMBills, Offsetdetails))
                End If
            Next

            lstCNprevDetails.Clear()
            lstCNcurrDetails.Clear()
            lstCNHeader.Clear()
            lstCNprevDetails.AddRange(prevBills)
            lstCNcurrDetails.AddRange(currBills)
            lstCNHeader.AddRange(CNHeader)

            Me.LoadParticipants(CNHeader)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GetMPIDMPName(ByVal IDNumber As String, ByVal ParticipantList As List(Of AMParticipants)) As String
        Dim MPIDName As String = ""
        Dim cParticipant = (From x In ParticipantList _
                           Where x.IDNumber = IDNumber).FirstOrDefault
        MPIDName = cParticipant.ParticipantID 'cParticipant.IDNumber & " " &
        Return MPIDName
    End Function

    Private Function GetINVDueDate(ByVal InvoiceNo As Long, ByVal AllInvoices As List(Of WESMBill)) As Date
        Dim retDate As Date
        retDate = (From x In AllInvoices _
                       Where x.InvoiceNumber = InvoiceNo _
                       Select x.DueDate).FirstOrDefault
        Return retDate
    End Function

    Private Function GetDMCMDueDate(ByVal DMCMNo As Long, ByVal DMCMDetails As List(Of DebitCreditMemoDetails), ByVal AllInvoices As List(Of WESMBill)) As Date
        Dim retDate As Date
        retDate = (From x In DMCMDetails _
                   Join y In AllInvoices _
                   On x.InvDMCMNo Equals y.InvoiceNumber _
                   Where x.DMCMNumber = DMCMNo _
                   Select y.DueDate Order By DueDate Descending).FirstOrDefault
        Return retDate
    End Function

    Private Function GetINVDate(ByVal InvoiceNo As Long, ByVal AllInvoices As List(Of WESMBill)) As Date
        Dim retDate As Date
        Dim _chk = (From x In AllInvoices _
                       Where x.InvoiceNumber = InvoiceNo _
                      Select x.InvoiceDate).Count

        Return retDate
    End Function

    Private Function GetDMCMDate(ByVal DMCMNo As Long, ByVal DMCMDetails As List(Of DebitCreditMemoDetails), ByVal AllInvoices As List(Of WESMBill)) As Date
        Dim retDate As Date
        retDate = (From x In DMCMDetails _
                   Join y In AllInvoices _
                   On x.InvDMCMNo Equals y.InvoiceNumber _
                   Where x.DMCMNumber = DMCMNo _
                   Select x.UpdatedDate Order By UpdatedDate Descending).FirstOrDefault
        Return retDate
    End Function
#End Region

#Region "Collection Notice Get Header/Summary/Summary Details"

    Private Function GetCollectionNoticeHeader(ByVal CurrentDueDate As Date, ByVal SummaryOfBills As List(Of WESMBillSummary), _
                                               ByVal CNSignatories As DocSignatories) As List(Of CollectionNotice)
        Dim retLstHeader As New List(Of CollectionNotice)
        Dim CurrentListOfSummary = (From x In SummaryOfBills _
                                    Where x.EndingBalance <> 0 _
                                    Select x.IDNumber.IDNumber Distinct).ToList 'Where x.DueDate = CurrentDueDate _
        Dim cnRefNoCtr As Long = 1

        If _IsSaved = True Then
            retLstHeader = WBillHelper.GetCollectionNotice(CDate(cbo_dueDate.SelectedItem.ToString))
        End If

        For Each item In CurrentListOfSummary
            Dim retHeader As New CollectionNotice
            Dim _itm = item

            Dim chkSummary = (From x In SummaryOfBills _
                               Where x.IDNumber.IDNumber = _itm _
                               And x.EndingBalance <> 0 _
                               Select x.EndingBalance).Sum

            Dim chkPrevSummary = (From x In SummaryOfBills _
                                  Where x.DueDate < CurrentDueDate _
                                  And x.IDNumber.IDNumber = _itm _
                                  And x.EndingBalance <> 0 _
                                  Select x.EndingBalance).Sum

            Dim chkARPrevSummary = (From x In SummaryOfBills _
                                Where x.IDNumber.IDNumber = _itm _
                                And x.DueDate < CurrentDueDate _
                                And x.EndingBalance < 0 _
                                Select x).ToList

            Dim chkARSummary = (From x In SummaryOfBills _
                                Where x.IDNumber.IDNumber = _itm _
                                And x.DueDate = CurrentDueDate _
                                And x.EndingBalance < 0 _
                                Select x).ToList

            If chkARSummary.Count = 0 And chkARPrevSummary.Count = 0 Then
                Continue For
            End If

            If chkSummary > 0 Then
                Continue For
            End If

            With retHeader
                .IDNumber = CStr(item)
                .CNNumber = 1
                .ReferenceNo = cnRefNoCtr
                .UpdatedBy = AMModule.UserName
                .ApprovedBy = CNSignatories.Signatory_2
                .PreparedBy = AMModule.UserName
                .CNDate = Date.Now
                .GenerateFlag = _GenerateFlag
            End With
            cnRefNoCtr += 1
            retLstHeader.Add(retHeader)
        Next
        Return retLstHeader
    End Function

    'get transactions from summary
    Private Function GetCollectionNoticeDetailsFromSummary(ByVal AllParticipants As List(Of AMParticipants), ByVal currentParticipant As String, ByVal currDueDate As Date, ByVal FilterSummary As List(Of WESMBillSummary), _
                                             ByVal dicReferenceNo As Dictionary(Of String, Long), _
                                             ByVal DMCMDetails As List(Of DebitCreditMemoDetails), Optional ByVal PrevCurrent As String = "", _
                                             Optional ByVal ParentParticipant As String = "", Optional ByVal AllWESMBills As List(Of WESMBill) = Nothing, _
                                             Optional ByVal OffsettingDetails As List(Of OffsetP2PC2CDetails) = Nothing) As List(Of CollectionNoticeDetails)
        Dim retCNDetails As New List(Of CollectionNoticeDetails)

        For Each item In FilterSummary
            Dim CNDetails As New CollectionNoticeDetails
            Dim citmSummary = item
            With CNDetails

                If item.BeginningBalance = 0 And item.EndingBalance = 0 Then
                    Continue For
                End If

                'Assign Parent Participant
                If ParentParticipant = "" Then
                    ParentParticipant = currentParticipant
                End If

                .ParentId = ParentParticipant
                If item.SummaryType = EnumSummaryType.DMCM Then
                    .ParticipantID = "DMCM FOR "
                    If item.ChargeType = EnumChargeType.E Then
                        .ParticipantID &= "ENERGY."
                    ElseIf item.ChargeType = EnumChargeType.EV Then
                        .ParticipantID &= "VAT ON ENERGY."
                    ElseIf item.ChargeType = EnumChargeType.MF Or item.ChargeType = EnumChargeType.MFV Then
                        .ParticipantID &= "MARKET FEES."
                    End If
                Else
                    .ParticipantID = Me.GetMPIDMPName(CStr(item.IDNumber.IDNumber), AllParticipants)
                End If

                .BillingPeriod = item.BillPeriod
                .TransactionType = PrevCurrent
                .ReferenceNo = dicReferenceNo(CStr(ParentParticipant))
                .SummaryType = item.SummaryType
                .INVDMCM = CStr(IIf(item.SummaryType = EnumSummaryType.DMCM, "DMCM-", "INV-")) & item.INVDMCMNo

                If item.SummaryType = EnumSummaryType.INV Then
                    .InvoiceDate = Me.GetINVDate(item.INVDMCMNo, AllWESMBills)
                    .DueDate = citmSummary.DueDate
                ElseIf item.SummaryType = EnumSummaryType.DMCM Then
                    .InvoiceDate = Me.GetDMCMDate(item.INVDMCMNo, DMCMDetails, AllWESMBills)
                    .DueDate = citmSummary.DueDate
                End If

                Dim cInvoiceDMCMNo = item.INVDMCMNo
                Dim cChargeType = item.ChargeType
                Dim DefaultInt As Decimal = 0
                If item.SummaryType = EnumSummaryType.INV Then

                    Dim GetAmountOfInvoice = (From x In AllWESMBills _
                                              Where x.InvoiceNumber = cInvoiceDMCMNo _
                                              And x.ChargeType = cChargeType _
                                              Select x).FirstOrDefault

                    If PrevCurrent = "C" Then
                        Select Case item.ChargeType
                            Case EnumChargeType.E
                                .Energy = item.EndingBalance ' GetAmountOfInvoice.Amount
                                If .Energy < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                                                   Date.Now, .Energy, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If

                            Case EnumChargeType.EV
                                .VATEnergy = item.EndingBalance 'GetAmountOfInvoice.Amount
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                .MF = (From x In FilterSummary _
                                       Where x.EndingBalance <> 0 _
                                       And (x.ChargeType = EnumChargeType.MF _
                                       Or x.ChargeType = EnumChargeType.MFV) _
                                       Select x.EndingBalance).Sum
                                Dim _tmpSum = (From x In FilterSummary _
                                               Select x.EndingBalance).Sum
                                If .MF < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                                                   Date.Now, _tmpSum, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If

                                .Total = _tmpSum + DefaultInt
                        End Select
                    Else
                        Select Case item.ChargeType
                            Case EnumChargeType.E
                                .Energy = item.EndingBalance
                                If .Energy < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                   Date.Now, .Energy, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If
                            Case EnumChargeType.EV
                                .VATEnergy = item.EndingBalance
                            Case EnumChargeType.MF, EnumChargeType.MFV
                                .MF = (From x In FilterSummary _
                                       Where x.DueDate = citmSummary.DueDate _
                                       And (x.ChargeType = EnumChargeType.MF _
                                       Or x.ChargeType = EnumChargeType.MFV) _
                                       And x.SummaryType = citmSummary.SummaryType _
                                       Select x.EndingBalance).Sum

                                If .MF < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                   Date.Now, .MF, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If

                                .Total = .MF + DefaultInt

                                '.Total = (From x In FilterSummary _
                                '          Where x.EndingBalance < 0 _
                                '          And x.DueDate = citmSummary.DueDate _
                                '          Select x.EndingBalance).Sum + DefaultInt
                        End Select
                    End If

                Else
                    Select Case item.ChargeType
                        Case EnumChargeType.E
                            If PrevCurrent = "P" Then
                                .Energy = item.EndingBalance
                                DefaultInt = 0
                                If .Energy < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                   Date.Now, .Energy, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If
                                .Total = .Energy + DefaultInt
                            Else
                                .Total = item.BeginningBalance
                            End If

                        Case EnumChargeType.EV
                            If PrevCurrent = "P" Then
                                .VATEnergy = item.EndingBalance
                                .Total = .VATEnergy
                            Else
                                .Total = item.BeginningBalance
                            End If
                        Case EnumChargeType.MF, EnumChargeType.MFV
                            Dim chkCredit = (From x In DMCMDetails _
                                   Where x.DMCMNumber = cInvoiceDMCMNo _
                                   And x.InvDMCMNo = 0 _
                                   Select x.Credit).FirstOrDefault

                            Dim chkDebit = (From x In DMCMDetails _
                                   Where x.DMCMNumber = cInvoiceDMCMNo _
                                   And x.InvDMCMNo = 0 _
                                   Select x.Debit).FirstOrDefault

                            Dim getMForMFV = (From x In lstWESMSummary _
                                              Where x.DueDate = citmSummary.DueDate _
                                              And x.IDNumber.IDNumber = citmSummary.IDNumber.IDNumber _
                                              And x.EndingBalance < 0 _
                                              And x.SummaryType = citmSummary.SummaryType _
                                              And (x.ChargeType = EnumChargeType.MF _
                                                   Or x.ChargeType = EnumChargeType.MFV) _
                                              Select x).ToList

                            If PrevCurrent = "P" Then
                                .MF = getMForMFV.Sum(Function(x As WESMBillSummary) x.EndingBalance)
                                DefaultInt = 0
                                If .MF < 0 Then
                                    DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                   Date.Now, .MF, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                End If
                                .Total = .MF + DefaultInt
                            End If
                            .Total = getMForMFV.Sum(Function(x As WESMBillSummary) x.EndingBalance) + DefaultInt

                            If PrevCurrent = "C" Then
                                DefaultInt = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                                                             Date.Now, .Total, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                .Total += DefaultInt
                            End If

                    End Select
                End If



                If item.ChargeType <> EnumChargeType.MF And item.ChargeType <> EnumChargeType.MFV Then
                    If item.SummaryType = EnumSummaryType.DMCM Then

                    Else
                        If item.ChargeType = EnumChargeType.E Then
                            .Total = .Energy + DefaultInt
                        Else
                            .Total = .VATEnergy
                        End If
                    End If
                End If

                If DefaultInt <> 0 Then
                    .DefaultInterest = DefaultInt
                End If

                If item.ChargeType <> EnumChargeType.MFV Then
                    retCNDetails.Add(CNDetails)
                End If

                If PrevCurrent = "C" Then
                    Dim chkDetails = Me.GetSummaryDetails(item.GroupNo, item.INVDMCMNo, AllWESMBills, _
                                                               OffsettingDetails, DMCMDetails, item.SummaryType, _
                                                               item.DueDate, item.ChargeType, ParentParticipant, AllParticipants, dicReferenceNo(CStr(ParentParticipant)))

                    If chkDetails.Count <> 0 Then
                        If citmSummary.SummaryType = EnumSummaryType.INV Then
                            'Get Current Invoice from chk details
                            Dim SummaryType = CStr(IIf(citmSummary.SummaryType = EnumSummaryType.DMCM, "DMCM", "INV"))
                            Dim curInvoice = (From x In chkDetails _
                                              Where x.INVDMCM = CStr(SummaryType & "-" & citmSummary.INVDMCMNo) _
                                              Select x).FirstOrDefault

                            Select Case citmSummary.ChargeType
                                Case EnumChargeType.E
                                    .Energy = curInvoice.Energy
                                    '.DefaultInterest = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                    '               Date.Now, .Energy, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                                Case EnumChargeType.EV
                                    .VATEnergy = curInvoice.VATEnergy
                                Case EnumChargeType.MF, EnumChargeType.MFV
                                    .MF = curInvoice.MF
                                    '.DefaultInterest = BFactory.ComputeDefaultInterest(item.DueDate, item.NewDueDate, _
                                    '               Date.Now, .MF, _tmpInterestRate(CDate(FormatDateTime(Date.Now, DateFormat.ShortDate))))
                            End Select

                            chkDetails.Remove(curInvoice)
                        End If
                    End If

                    retCNDetails.AddRange(chkDetails)
                End If
            End With

        Next
        Return retCNDetails
    End Function

    'Get the details of The Summary
    Private Function GetSummaryDetails(ByVal curGroupNo As Long, ByVal chkINVDMCMNo As Long, ByVal AllWESMBills As List(Of WESMBill), _
                                       ByVal OffsetDetails As List(Of OffsetP2PC2CDetails), ByVal DMCMDetails As List(Of DebitCreditMemoDetails), _
                                       ByVal SummaryType As EnumSummaryType, ByVal DateDue As Date, ByVal cChargeType As EnumChargeType, _
                                       ByVal ParentID As String, ByVal AllParticipants As List(Of AMParticipants), ByVal ReferenceNo As Long) As List(Of CollectionNoticeDetails)
        Dim retSummaryDetails As New List(Of CollectionNoticeDetails)

        Dim getDMCMDetails As New List(Of DebitCreditMemoDetails)

        Dim tSelected As Decimal = 0
        If SummaryType = EnumSummaryType.INV Then
            Dim getDMCMNo = (From x In OffsetDetails _
                             Where x.WESMBillSummaryNo = curGroupNo _
                             And x.InvoiceNumber = chkINVDMCMNo _
                             Select x).FirstOrDefault
            'getDMCMDetails = (From x In DMCMDetails _
            '                  Where x.DMCMNumber = getDMCMNo.DMCMNumber _
            '                  Select x).ToList 'And x.InvDMCMNo <> chkINVDMCMNo _
        ElseIf SummaryType = EnumSummaryType.DMCM Then

            Dim getDMCMNo = (From x In OffsetDetails _
                             Where x.WESMBillSummaryNo = curGroupNo _
                             Select x.DMCMNumber Distinct).ToList 'And x.DMCMNumber <> chkINVDMCMNo _

            For Each itmDMCMNo In getDMCMNo
                Dim _itmDMCMNo = itmDMCMNo
                Dim _DetailsList = (From x In DMCMDetails _
                                    Where x.DMCMNumber = _itmDMCMNo _
                                    Select x).ToList()
                getDMCMDetails.AddRange(_DetailsList)

            Next

            getDMCMDetails = (From x In DMCMDetails _
                           Where x.DMCMNumber = chkINVDMCMNo _
                           And x.InvDMCMNo <> 0 _
                           Select x).ToList()
        End If

        For Each item In getDMCMDetails

            Dim CNDetails As New CollectionNoticeDetails
            Dim curInvoice = item.InvDMCMNo
            Dim invDetails = (From x In AllWESMBills _
                                          Where x.InvoiceNumber = curInvoice _
                                          And x.ChargeType = cChargeType).FirstOrDefault
            Dim amtToCollectionNotice As Decimal

            If item.AccountCode = DebitCode Then
                If item.Debit = 0 Then
                    amtToCollectionNotice = item.Credit * -1D
                Else
                    amtToCollectionNotice = item.Debit * -1D
                End If
            ElseIf item.AccountCode = CreditCode Then
                If item.Debit = 0 Then
                    amtToCollectionNotice = item.Credit * -1D
                Else
                    amtToCollectionNotice = item.Debit * -1D
                End If
            End If

            With CNDetails
                .BillingPeriod = invDetails.BillingPeriod
                .DueDate = invDetails.DueDate
                .INVDMCM = CStr(IIf(item.SummaryType = EnumSummaryType.DMCM, "DMCM-", "INV-")) & item.InvDMCMNo
                .ParentId = ParentID
                .ParticipantID = "     " & Me.GetMPIDMPName(CStr(item.IDNumber.IDNumber), AllParticipants)
                .ReferenceNo = ReferenceNo
                .SummaryType = item.SummaryType
                .TransactionType = "C"
                .InvoiceDate = invDetails.InvoiceDate
                .DueDate = invDetails.DueDate



                If SummaryType = EnumSummaryType.DMCM Then
                    Select Case invDetails.ChargeType
                        Case EnumChargeType.E
                            .Energy = amtToCollectionNotice 'invDetails.Amount
                        Case EnumChargeType.EV
                            .VATEnergy = amtToCollectionNotice 'invDetails.Amount
                        Case EnumChargeType.MF, EnumChargeType.MFV
                            'Get Current bill's details
                            Dim GetCurrentBill = (From x In lstWESMSummary _
                                                  Where x.INVDMCMNo = chkINVDMCMNo _
                                                  Select x).FirstOrDefault

                            'Get MarketFees/MarketFees VAT Bills of Current Bill
                            Dim getMFMFV = (From x In AllWESMBills _
                                            Where x.InvoiceNumber = curInvoice _
                                            And (x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV) _
                                            Select x).ToList

                            For Each itmMFMFV In getMFMFV
                                .MF += itmMFMFV.Amount
                            Next
                    End Select
                Else
                    Select Case invDetails.ChargeType
                        Case EnumChargeType.E
                            .Energy = invDetails.Amount
                        Case EnumChargeType.EV
                            .VATEnergy = invDetails.Amount
                        Case EnumChargeType.MF, EnumChargeType.MFV
                            .MF += (From x In AllWESMBills _
                                   Where x.InvoiceNumber = curInvoice _
                                   And (x.ChargeType = EnumChargeType.MF _
                                   Or x.ChargeType = EnumChargeType.MFV) _
                                        Select x.Amount).Sum
                    End Select
                End If

            End With

            If cChargeType <> EnumChargeType.MFV Then
                retSummaryDetails.Add(CNDetails)
            End If
        Next
        Return retSummaryDetails
    End Function

    Private Function CheckDueDate(ByVal DueDate As Date) As Boolean
        Dim retCheck As Boolean = False

        Dim _CollectionNotices = WBillHelper.GetCollectionNotice

        Dim chkDueDate = (From x In _CollectionNotices _
                          Where x.DueDate = DueDate _
                          Select x).ToList

        If chkDueDate.Count > 0 Then
            retCheck = True

            lstCNHeader = chkDueDate

            Dim GetCNDetails = WBillHelper.GetCollectionNoticeDetails()

            GetCNDetails = (From x In GetCNDetails _
                            Join y In chkDueDate _
                            On x.ReferenceNo Equals y.ReferenceNo _
                            Select x).ToList

            lstCNcurrDetails = (From x In GetCNDetails _
                                Where x.TransactionType = "C" _
                                Select x).ToList

            lstCNprevDetails = (From x In GetCNDetails _
                                Where x.TransactionType = "P" _
                                Select x).ToList

            Dim GetAllParticipants = WBillHelper.GetAMParticipantsAll()

            Dim lstAllParticipants = (From x In GetCNDetails _
                                      Join y In GetAllParticipants _
                                      On x.ParentId Equals CStr(y.IDNumber) _
                                      Select y Distinct Order By y.ParticipantID Ascending).ToList

            For Each item In lstAllParticipants
                clb_participants.Items.Add(item.ParticipantID)
            Next

        End If

        Return retCheck
    End Function

#End Region



End Class