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
'
Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmRPTCollectionNotice
    Dim WBillHelper As WESMBillHelper
    Private Sub frmCollectionNotice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = ConnectionString
        WBillHelper.UserName = "JCLP"


        Try
            Dim Participants = WBillHelper.GetParticipantIDs()
            Dim ParentParticipants = WBillHelper.GetParentChildMapping(1)
            'Dim ParentParticipants 
            Dim ToListParticipant = (From x In Participants Join y In ParentParticipants On x.IDNumber Equals y.IDNumber Where _
                                     y.ParentFlag = 1 And y.Status = 1 Select x.ParticipantID Distinct Order By ParticipantID Ascending).ToList
            CHB_AllParticipants.Checked = True

            'cbo_participant.DataSource = Participants
            For Each item In ToListParticipant
                LB_ParentParticipants.Items.Add(item.ToString)
            Next
            'ListBox1.DataSource = Participants

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub
    Private Sub cmd_genreport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim WESMBillsSummary = WBillHelper.GetWESMBillSummary()
            Dim CurrentBillingPeriod As Integer = CInt((From x In WESMBillsSummary Select x.BillPeriod Order By BillPeriod Descending).FirstOrDefault.ToString)
            Dim _SignatoriesCN = WBillHelper.GetSignatories("CN").First

            'Get Current WESM Bills and All Summaries
            Dim WESMBills = WBillHelper.GetWESMBills(CurrentBillingPeriod)
            Dim BillCalendar = WBillHelper.GetCalendarBP(CurrentBillingPeriod)

            'Get Participants and Participants Mapping
            Dim ParticipantList As New List(Of AMParticipants)
            Dim AllParticipants = WBillHelper.GetParticipantIDs()
            If CHB_AllParticipants.Checked = False Then
                If LB_ParentParticipants.SelectedItems.Count = 1 Then
                    ParticipantList = (From x In AllParticipants Where x.ParticipantID = LB_ParentParticipants.SelectedItem.ToString).ToList
                Else
                    For Each item In LB_ParentParticipants.SelectedItems
                        Dim currSelected = item.ToString
                        ParticipantList.Add((From x In AllParticipants Where x.ParticipantID = currSelected).FirstOrDefault)
                    Next
                End If
            Else
                ParticipantList = WBillHelper.GetParticipantIDs()
            End If
            Dim CurrentParticipants = (From x In WESMBills Select x.IDNumber Distinct)
            Dim CurrentParticipant = (From x In ParticipantList Join y In CurrentParticipants On x.IDNumber Equals y Select x).ToList
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(1)

            'get all child
            Dim _temp = (From x In CurrentParticipant Join y In ParticipantMapping On x.IDNumber Equals y.PCNumber _
                                Where x.Status = 1 And y.Status = 1 And y.BillPeriod = CurrentBillingPeriod _
                 Select New With {.IDNum = y.IDNumber, .ParentNum = y.PCNumber, x.ParticipantID, _
                                  x.FullName, y.ParentFlag})

            If _temp.Count = 0 Then
                MsgBox("The selected participant/s has no Current WESM Bill, a Collection notice cannot be created.", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            Dim PreviousWESMBills = WBillHelper.GetWESMBillSummaryForCNReport()

            'Save Header to list
            Dim CollectionNoticeHeader As New List(Of CollectionNotice)
            For Each item In _temp
                Dim ParticipantID = item.IDNum
                Dim _CollectionNoticeHeader As New CollectionNotice
                If item.ParentFlag = 1 Then
                    With _CollectionNoticeHeader
                        .IDNumber = CInt(ParticipantID)
                        .PreparedBy = _SignatoriesCN.PreparedBy
                        .ApprovedBy = _SignatoriesCN.ApprovedBy
                        .UpdatedBy = _SignatoriesCN.UpdatedBy
                    End With
                    CollectionNoticeHeader.Add(_CollectionNoticeHeader)
                End If
            Next
            'Save Header
            'WBillHelper.SaveCollectionNotice(CollectionNoticeHeader)

            Dim CollectionNotice = WBillHelper.GetCollectionNotice()

            'Get Previous Bills (WESM_BILLS_SUMMARY)
            Dim PreviousBills As New List(Of CollectionNoticeDetails)
            Dim CurrentBills As New List(Of CollectionNoticeDetails)
            For Each item In _temp
                Dim itemParticipant = item.IDNum
                Dim ParentParticipant = item.ParentNum
                'Reference Number
                Dim CurrentReferenceNumber As Integer = CInt((From x In CollectionNotice Where x.IDNumber = itemParticipant Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault.ToString)
                Dim PreviousSummary = (From x In WESMBillsSummary Where x.IDNumber.IDNumber = itemParticipant And x.EndingBalance <> 0 And x.BillPeriod < CurrentBillingPeriod Select x Order By x.BillPeriod).ToList
                If CurrentReferenceNumber = 0 Then
                    CurrentReferenceNumber = CInt((From x In CollectionNotice Where x.IDNumber = ParentParticipant Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault.ToString)
                End If

                Dim DueDatesList = (From x In PreviousWESMBills Where x.IDNumber.IDNumber = itemParticipant Select x.DueDate Distinct).ToList
                Dim BillPeriodList = (From x In PreviousWESMBills Where x.IDNumber.IDNumber = itemParticipant Select x.BillPeriod Distinct).ToList

                For Each pBillPeriod In BillPeriodList
                    Dim _PreviousBills As New CollectionNoticeDetails
                    Dim _BillPeriod = pBillPeriod
                    For Each pDueDates In DueDatesList
                        Dim _DueDates = pDueDates
                        Dim forPreviousReport = (From x In PreviousWESMBills Where x.DueDate = _DueDates And x.BillPeriod = _BillPeriod And x.IDNumber.IDNumber = itemParticipant).ToList
                        For Each pReport In forPreviousReport
                            With _PreviousBills
                                .BillingPeriod = pReport.BillPeriod
                                .DueDate = pReport.DueDate
                                .ReferenceNo = CurrentReferenceNumber
                                .TransactionType = "P"
                                If item.ParentFlag = 1 Then
                                    .ParticipantID = CStr(itemParticipant)
                                    .ParentId = itemParticipant.ToString
                                Else
                                    .ParticipantID = CStr(item.ParentNum)
                                    .ParentId = itemParticipant.ToString
                                End If

                                If pReport.Energy < 0 Then
                                    .Energy += Math.Abs(pReport.Energy)
                                End If

                                If pReport.VATEnergy < 0 Then
                                    .VATEnergy += Math.Abs(pReport.VATEnergy)
                                End If
                                .MF += Math.Abs(pReport.MarketFees)
                                .VATMF += Math.Abs(pReport.VATMarketFees)
                            End With
                        Next
                    Next
                    '_PreviousBills.Total = _PreviousBills.Energy + _PreviousBills.VATEnergy + _PreviousBills.MF + _PreviousBills.VATMF
                    PreviousBills.Add(_PreviousBills)
                Next



                'Save each record found in WESMBills to list as Current
                Dim CurrentWESMBills = (From x In WESMBills Where x.IDNumber = itemParticipant And x.BillingPeriod = CurrentBillingPeriod)
                Dim CurrentInvoices = (From x In CurrentWESMBills Select x.InvoiceNumber Distinct).ToList

                For Each record In CurrentInvoices
                    Dim CurrInvoice As Double = CDbl(record.ToString)
                    Dim BrowseCurrentWESMBills = (From x In CurrentWESMBills Where x.InvoiceNumber = CurrInvoice Select x).ToList
                    Dim _Currentbills As New CollectionNoticeDetails
                    For Each ToJoin In BrowseCurrentWESMBills
                        With _Currentbills
                            If item.ParentFlag = 1 Then
                                .ParticipantID = CStr(itemParticipant)
                                .ParentId = itemParticipant.ToString
                            Else
                                .ParticipantID = CStr(item.ParentNum)
                                .ParentId = itemParticipant.ToString
                            End If
                            .BillingPeriod = ToJoin.BillingPeriod
                            .DueDate = ToJoin.DueDate
                            .InvoiceDate = ToJoin.InvoiceDate
                            .INVDMCM = CStr(ToJoin.InvoiceNumber)
                            .TransactionType = "C"
                            .ReferenceNo = CurrentReferenceNumber

                            If ToJoin.ChargeType = EnumChargeType.E Then
                                If ToJoin.Amount < 0 Then
                                    .Energy += ToJoin.Amount
                                End If
                            Else
                                If ToJoin.ChargeType = EnumChargeType.E Then
                                    .Energy += ToJoin.Amount
                                ElseIf ToJoin.ChargeType = EnumChargeType.EV Then
                                    .VATEnergy += ToJoin.Amount
                                ElseIf ToJoin.ChargeType = EnumChargeType.MF Then
                                    .MF += ToJoin.Amount
                                ElseIf ToJoin.ChargeType = EnumChargeType.MFV Then
                                    .VATMF += ToJoin.Amount
                                End If
                            End If
                        End With

                    Next
                    If CurrentWESMBills.Count > 0 Then
                        CurrentBills.Add(_Currentbills)
                    End If
                Next
            Next
            'Order Bills by Invoice number - Participant ID before saving
            CurrentBills = (From x In CurrentBills Select x Order By x.INVDMCM Ascending, x.ParticipantID Ascending).ToList
            PreviousBills = (From x In PreviousBills Select x Order By x.INVDMCM Ascending, x.ParticipantID Ascending).ToList

            'Save to Database
            ' WBillHelper.SaveCollectionNoticeDetails(PreviousBills)
            ' WBillHelper.SaveCollectionNoticeDetails(CurrentBills)

            'Generate Details for report
            Dim CollectionNoticenumber = (From x In CollectionNotice Select x.CNNumber Order By CNNumber Descending).FirstOrDefault.ToString
            Dim ForReportCurrentHeader = (From x In CollectionNotice Where x.CNNumber = CLng(CollectionNoticenumber)).ToList

            'insert dummy data on List to be sent to Crystal Reports
            ' To show current Transactions even there's no Previous Transactions
            For Each item In _temp
                Dim ParticipantID = item.IDNum
                Dim ParentID = item.ParentNum
                Dim tmpReferenceNumber As Long

                If item.ParentFlag = 1 Then
                    tmpReferenceNumber = CLng((From x In ForReportCurrentHeader Where x.IDNumber = ParticipantID Select x.ReferenceNo).FirstOrDefault.ToString)
                Else
                    tmpReferenceNumber = CLng((From x In ForReportCurrentHeader Where x.IDNumber = ParentID Select x.ReferenceNo).FirstOrDefault.ToString)
                End If

                Dim CheckPrevious = (From x In PreviousBills Where x.ReferenceNo = tmpReferenceNumber And x.TransactionType = "P").ToList

                If CheckPrevious.Count = 0 Then
                    Dim ForDummyPrevious As New CollectionNoticeDetails
                    With ForDummyPrevious
                        .ReferenceNo = tmpReferenceNumber
                        If item.ParentFlag = 1 Then
                            .ParticipantID = CStr(ParticipantID)
                        Else
                            .ParticipantID = CStr(ParentID)
                        End If
                        .Total = 0
                    End With
                    PreviousBills.Add(ForDummyPrevious)
                End If
            Next
            Dim DateDue = CDate((From x In CurrentBills Select x.DueDate Distinct).FirstOrDefault.ToString)
            Dim ds = WBillHelper.SaveCollectionDataSet(AllParticipants, ForReportCurrentHeader, CurrentBills, PreviousBills, BillCalendar)
            frmProgress.Show()
            Dim frmReport As New frmReportViewer
            With frmReport
                .LoadWESMBillCollectionNotice(ds, DateDue)
                .ShowDialog()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub cmd_Generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Get Current Billing Period
            Dim BillPeriod = WBillHelper.GetCalendarBP()
            Dim CurrentBillPeriod As Integer = CInt((From x In BillPeriod Select x.BillingPeriod Order By BillingPeriod Descending).FirstOrDefault.ToString)

            'Get All WESMBill Summary
            Dim WESMBillSummary = WBillHelper.GetWESMBillSummary()

            'Get All WESM Bills in the Current Billing Period
            Dim WESMBills = WBillHelper.GetWESMBills(CurrentBillPeriod)

            'Get Participants and Mapping
            Dim AllParticipants = WBillHelper.GetParticipantIDs()
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(1)

            Dim SaveCollectionNoticeList As New List(Of CollectionNotice)

            'All Participants Not Yet assigned in Participant Mapping will not have Collection Notice 11/30/2011 - vlad
            'Participants That will be listed in dropdown are only Parents                            11/30/2011 - vlad

            ' LINQ Joining BILL_PARTICIPANTS  AND AM_PARENT_CHILD_MAPPING
            'Dim _temp = (From x In AllParticipants Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
            '     Where x.Status = 1 And y.Status = 1 _
            '     Select New With {.IDNum = x.IDNumber, .ParentNum = y.PCNumber, x.ParticipantID, _
            '                      x.Remarks, y.ParentFlag})

            'All Participants with current WESM Bills will have Collection Notice if (-) is present 02/24/2012 - jclp
            Dim _temp = WBillHelper.GetP2CMappingAllBP()

            'For Collection Notice Header
            For Each item In _temp
                Dim ParticipantIDNumber As Integer = item.IDNumber
                Dim SaveCollectionNoticeHeader As New CollectionNotice
                If item.ParentFlag = 1 Then
                    With SaveCollectionNoticeHeader
                        .IDNumber = CInt(item.IDNumber)
                        .PreparedBy = "JCLP - PREPARED"
                        .ApprovedBy = "JCLP - APPROVED"
                        .UpdatedBy = "JCLP - UPDATED"
                    End With
                    SaveCollectionNoticeList.Add(SaveCollectionNoticeHeader)
                End If
            Next
            'DataGridView1.DataSource = SaveCollectionNoticeList

            'Save data to AM_COLLECTION_NOTICE
            '  WBillHelper.SaveCollectionNotice(SaveCollectionNoticeList)

            Dim CollectionNoticeHeader = WBillHelper.GetCollectionNotice()
            Dim CollectionNoticenumber = (From x In CollectionNoticeHeader Select x.CNNumber Order By CNNumber Descending).FirstOrDefault.ToString

            Dim CollectionNoticeDetails As New List(Of CollectionNoticeDetails)

            'Save Previous details to list
            For Each item In _temp
                Dim PresentIDNumber As Integer = CInt(item.IDNumber)
                Dim ParticipantSummary = (From x In WESMBillSummary Where x.IDNumber.IDNumber = PresentIDNumber And x.EndingBalance <> 0 And x.BillPeriod < CInt(CurrentBillPeriod)).ToList
                Dim CurrentRefNo = CDbl((From x In CollectionNoticeHeader Where x.IDNumber = PresentIDNumber Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault.ToString)
                'Assign Records for saving Previous Details
                'MsgBox(ParticipantSummary.Count)
                For Each record In ParticipantSummary
                    Dim SaveCollectionNoticeDetails As New CollectionNoticeDetails
                    With SaveCollectionNoticeDetails
                        .BillingPeriod = record.BillPeriod
                        .DueDate = record.DueDate
                        .Total = record.EndingBalance
                        .TransactionType = "P"
                        .ReferenceNo = CurrentRefNo
                        If item.ParentFlag = 1 Then
                            'if Parent, assign Participant ID as own ID
                            .ParticipantID = CStr(item.IDNumber)
                        ElseIf item.ParentFlag = 0 Then
                            'if Child assign Participant ID as Parent ID
                            .ParticipantID = item.PCNumber.ToString
                        End If
                        If record.ChargeType = EnumChargeType.E Then
                            .Energy = record.EndingBalance
                        ElseIf record.ChargeType = EnumChargeType.EV Then
                            .VATEnergy = record.EndingBalance
                        ElseIf record.ChargeType = EnumChargeType.MF Then
                            .MF = record.EndingBalance
                        ElseIf record.ChargeType = EnumChargeType.MFV Then
                            .VATMF = record.EndingBalance
                        End If

                    End With

                    'Added 11/30/2011
                    If record.ChargeType = EnumChargeType.E Then
                        If record.EndingBalance < 0 Then
                            CollectionNoticeDetails.Add(SaveCollectionNoticeDetails)
                        End If
                    Else
                        CollectionNoticeDetails.Add(SaveCollectionNoticeDetails)
                    End If
                Next
            Next

            'Save Current Details To list
            For Each item In _temp
                Dim PresentIDNumber As Integer = CInt(item.IDNumber)
                Dim ParticipantSummary = (From x In WESMBills Where x.IDNumber = PresentIDNumber And x.BillingPeriod = CInt(CurrentBillPeriod)).ToList
                Dim CurrentRefNo = CDbl((From x In CollectionNoticeHeader Where x.IDNumber = PresentIDNumber Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault.ToString)
                'Assign Records for saving Current Details
                For Each record In ParticipantSummary
                    Dim SaveCollectionNoticeDetails As New CollectionNoticeDetails
                    With SaveCollectionNoticeDetails
                        .BillingPeriod = record.BillingPeriod
                        .DueDate = record.DueDate
                        .InvoiceDate = record.InvoiceDate
                        .INVDMCM = CStr(record.InvoiceNumber)
                        .Total = record.Amount
                        .TransactionType = "C"
                        .ReferenceNo = CurrentRefNo
                        If item.ParentFlag = 1 Then
                            'if Parent, assign Participant ID as own ID
                            .ParticipantID = item.IDNumber.ToString
                        ElseIf item.ParentFlag = 0 Then
                            'if Child assign Participant ID as Parent ID
                            .ParticipantID = item.PCNumber.ToString
                        End If
                        If record.ChargeType = EnumChargeType.E Then
                            .Energy = record.Amount
                        ElseIf record.ChargeType = EnumChargeType.EV Then
                            .VATEnergy = record.Amount
                        ElseIf record.ChargeType = EnumChargeType.MF Then
                            .MF = record.Amount
                        ElseIf record.ChargeType = EnumChargeType.MFV Then
                            .VATMF = record.Amount
                        End If

                    End With
                    'CollectionNoticeDetails.Add(SaveCollectionNoticeDetails)
                    'Added 11/30/2011
                    If record.ChargeType = EnumChargeType.E Then
                        If record.Amount < 0 Then
                            CollectionNoticeDetails.Add(SaveCollectionNoticeDetails)
                        End If
                    Else
                        CollectionNoticeDetails.Add(SaveCollectionNoticeDetails)
                    End If
                Next
            Next
            'Save data to AM_COLLECTION_NOTICE_DETAILS
            '   WBillHelper.SaveCollectionNoticeDetails(CollectionNoticeDetails)


            '*************************************************************************
            ' Prepare Data for Report
            '*************************************************************************
            Dim GetCollectionNoticedetails = WBillHelper.GetCollectionNoticeDetails()
            Dim PreviousCollectionNoticeDetails = (From x In GetCollectionNoticedetails Where x.TransactionType = "P" And x.BillingPeriod < CurrentBillPeriod _
                                                   Order By x.BillingPeriod Ascending, x.INVDMCM Ascending).ToList
            Dim CurrentCollectionNoticeDetails = (From x In GetCollectionNoticedetails Where x.TransactionType = "C" And x.BillingPeriod = CurrentBillPeriod _
                                                   Order By x.BillingPeriod Ascending, x.INVDMCM Ascending).ToList
            Dim ForReportCurrentNotice = (From x In CollectionNoticeHeader Where x.CNNumber = CDbl(CollectionNoticenumber) Select x Order By x.ReferenceNo).ToList
            Dim GetCalendar = WBillHelper.GetCalendarBP()

            CurrentCollectionNoticeDetails = (From x In CurrentCollectionNoticeDetails Join y In GetCalendar On x.BillingPeriod Equals y.BillingPeriod _
                                      Select x).ToList

            'insert dummy data on List to be sent to Crystal Reports

            For Each item In CollectionNoticeHeader
                Dim tmpReferenceNo = item.ReferenceNo
                Dim tmpParticipantId = item.IDNumber
                Dim CheckIfPrevious = (From x In GetCollectionNoticedetails Where x.ReferenceNo = tmpReferenceNo And x.TransactionType = "P").ToList
                If CheckIfPrevious.Count = 0 Then
                    Dim ForDummyPrevious As New CollectionNoticeDetails
                    With ForDummyPrevious
                        .ReferenceNo = tmpReferenceNo
                        .ParticipantID = tmpParticipantId.ToString
                        .Total = 0
                    End With
                    PreviousCollectionNoticeDetails.Add(ForDummyPrevious)
                End If
            Next

            'Save Tables to Dataset for Report
            'Dim ds = WBillHelper.SaveCollectionDataSet(AllParticipants, ForReportCurrentNotice, CurrentCollectionNoticeDetails, PreviousCollectionNoticeDetails)

            'frmProgress.Show()
            'Dim frmReport As New frmReportViewer
            'With frmReport
            '    .LoadWESMBillCollectionNotice(ds)
            '    .ShowDialog()
            'End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'Get WESMBills and WESMBill Summary
    '        Dim PartId As String = ""
    '        Dim AMWESMBill = WBillHelper.GetWESMBills()
    '        Dim AMWESMBillSummary = WBillHelper.GetWESMBillSummary()
    '        Dim CurrentBP = (From x In AMWESMBill Order By x.BillingPeriod Descending Select x.BillingPeriod).FirstOrDefault.ToString
    '        Dim CurrentAMWESMBills = (From x In AMWESMBill Where x.BillingPeriod = CInt(CurrentBP) Select x)
    '        Dim CurrentAMWESMBillSummary = (From x In AMWESMBillSummary Where x.BillPeriod = CInt(CurrentBP) Select x)
    '        Dim Participants = WBillHelper.GetParticipantIDs()
    '        Dim CurrentParticipants = (From x In CurrentAMWESMBills Select x.IDNumber Distinct)
    '        ' Me.DataGridView3.DataSource = Participants

    '        'Get Participants
    '        If cbo_participant.SelectedIndex <> 0 Then

    '            Participants = (From x In Participants Where x.ParticipantID = cbo_participant.SelectedItem.ToString Select x).ToList
    '            PartId = (From x In Participants Select x.IDNumber).FirstOrDefault.ToString
    '            AMWESMBill = (From x In AMWESMBill Where x.IDNumber = CDbl(PartId)).ToList
    '            AMWESMBillSummary = (From x In AMWESMBillSummary Where x.IDNumber.IDNumber = CDbl(PartId)).ToList

    '            If AMWESMBill.Count = 0 Then
    '                MsgBox("No Record found for Participant " & cbo_participant.SelectedItem.ToString & "!", MsgBoxStyle.Critical)
    '                Exit Sub
    '            End If

    '            If AMWESMBillSummary.Count = 0 Then
    '                MsgBox("No Record found for Participant " & cbo_participant.SelectedItem.ToString & "!", MsgBoxStyle.Critical)
    '                Exit Sub
    '            End If

    '        End If

    '        Dim SaveCollectionNoticeList As New List(Of CollectionNotice)
    '        For Each itemParticipants In Participants
    '            'Save Present Run
    '            Dim SaveCollectionNotice As New CollectionNotice
    '            With SaveCollectionNotice
    '                .IDNumber = CInt(itemParticipants.IDNumber)
    '                .PreparedBy = "JCLP - PREPARED"
    '                .ApprovedBy = "JCLP - APPROVED"
    '                .UpdatedBy = "JCLP - UPDATED"
    '            End With
    '            SaveCollectionNoticeList.Add(SaveCollectionNotice)
    '        Next
    '        WBillHelper.SaveCollectionNotice(SaveCollectionNoticeList)

    '        'Get Header Collection Notice
    '        Dim CurrentCollectionNotice = WBillHelper.GetCollectionNotice()

    '        'Get Current Collection Notice for Specific Participant
    '        If cbo_participant.SelectedIndex <> 0 Then
    '            CurrentCollectionNotice = (From x In CurrentCollectionNotice Where x.IDNumber = CDbl(PartId) Select x).ToList
    '        End If

    '        Dim CollectionNoticeNumber = (From x In CurrentCollectionNotice Order By x.CNNumber Descending Select x.CNNumber).FirstOrDefault.ToString
    '        Dim ForReportCurrentNotice = (From x In CurrentCollectionNotice Where x.CNNumber = CDbl(CollectionNoticeNumber) Select x Order By x.ReferenceNo).ToList

    '        'Create Current Details (FROM AM_WESM_BILL)
    '        Dim SaveCurrentWESMList As New List(Of CollectionNoticeDetails)
    '        For Each itemCurrentAMWESMBills In CurrentAMWESMBills
    '            Dim CurrentParticipant = CDbl(itemCurrentAMWESMBills.IDNumber)
    '            'Get Current Ref No for Saving in details for Participant
    '            Dim CurrentRefNo = (From x In CurrentCollectionNotice Where x.IDNumber = CurrentParticipant Order By x.ReferenceNo Descending _
    '                                Select x.ReferenceNo).FirstOrDefault.ToString
    '            Dim SaveCurrentWESMBills As New CollectionNoticeDetails
    '            With SaveCurrentWESMBills
    '                .BillingPeriod = itemCurrentAMWESMBills.BillingPeriod
    '                .DueDate = itemCurrentAMWESMBills.DueDate
    '                .InvoiceDate = itemCurrentAMWESMBills.InvoiceDate
    '                .InvoiceNumber = itemCurrentAMWESMBills.InvoiceNumber
    '                .Total = itemCurrentAMWESMBills.Amount
    '                .TransactionType = "C"
    '                .ReferenceNo = CDbl(CurrentRefNo)
    '                .ParticipantID = CurrentParticipant
    '                If itemCurrentAMWESMBills.ChargeType = EnumChargeType.E Then
    '                    .Energy = itemCurrentAMWESMBills.Amount
    '                ElseIf itemCurrentAMWESMBills.ChargeType = EnumChargeType.EV Then
    '                    .VATEnergy = itemCurrentAMWESMBills.Amount
    '                ElseIf itemCurrentAMWESMBills.ChargeType = EnumChargeType.MF Then
    '                    .MF = itemCurrentAMWESMBills.Amount
    '                ElseIf itemCurrentAMWESMBills.ChargeType = EnumChargeType.MFV Then
    '                    .VATMF = itemCurrentAMWESMBills.Amount
    '                End If
    '            End With
    '            'added 11/24/2011
    '            If itemCurrentAMWESMBills.ChargeType = EnumChargeType.E Then
    '                If itemCurrentAMWESMBills.Amount < 0 Then
    '                    SaveCurrentWESMList.Add(SaveCurrentWESMBills)
    '                End If
    '            Else
    '                SaveCurrentWESMList.Add(SaveCurrentWESMBills)
    '            End If

    '        Next
    '        WBillHelper.SaveCollectionNoticeDetails(SaveCurrentWESMList)

    '        '**************************************************************************************
    '        ' Added November 24/2011 - Previous details should be per Summary and not per invoice
    '        '**************************************************************************************
    '        'Create Previous Details (From AM_WESM_BILL_SUMMARY)
    '        Dim SavePreviousWESMBillSummaryList As New List(Of CollectionNoticeDetails)
    '        Dim WithBalance = (From x In AMWESMBillSummary Where x.BillPeriod < CDbl(CurrentBP) And x.EndingBalance <> 0 Select x)
    '        For Each itemWithBalance In WithBalance
    '            Dim SavePreviousWESMBillsSummary As New CollectionNoticeDetails
    '            Dim CurrentParticipant = CDbl(itemWithBalance.IDNumber.IDNumber.ToString)
    '            'Get Current Ref No for Saving in details for Participant
    '            Dim CurrentRefNo = (From x In CurrentCollectionNotice Where x.IDNumber = CurrentParticipant Order By x.ReferenceNo Descending _
    '                                Select x.ReferenceNo).FirstOrDefault.ToString
    '            With SavePreviousWESMBillsSummary
    '                .BillingPeriod = itemWithBalance.BillPeriod
    '                .DueDate = itemWithBalance.DueDate
    '                .Total = itemWithBalance.EndingBalance
    '                .TransactionType = "P"
    '                .ReferenceNo = CDbl(CurrentRefNo)
    '                .ParticipantID = CurrentParticipant
    '                If itemWithBalance.ChargeType = EnumChargeType.E Then
    '                    .Energy = itemWithBalance.EndingBalance
    '                ElseIf itemWithBalance.ChargeType = EnumChargeType.EV Then
    '                    .VATEnergy = itemWithBalance.EndingBalance
    '                ElseIf itemWithBalance.ChargeType = EnumChargeType.MF Then
    '                    .MF = itemWithBalance.EndingBalance
    '                ElseIf itemWithBalance.ChargeType = EnumChargeType.MFV Then
    '                    .VATMF = itemWithBalance.EndingBalance
    '                End If
    '            End With
    '            'Added 11/24/2011
    '            If itemWithBalance.ChargeType = EnumChargeType.E Then
    '                If itemWithBalance.EndingBalance < 0 Then
    '                    SavePreviousWESMBillSummaryList.Add(SavePreviousWESMBillsSummary)
    '                End If
    '            Else
    '                SavePreviousWESMBillSummaryList.Add(SavePreviousWESMBillsSummary)
    '            End If
    '        Next
    '        WBillHelper.SaveCollectionNoticeDetails(SavePreviousWESMBillSummaryList)


    '        '**************************************************************************************
    '        ' Removed November 24/2011 - Previous details should be per Summary and not per invoice
    '        '**************************************************************************************

    '        'Create Previous Details (FROM AM_WESM_BILL)
    '        ' ''Dim SavePreviousWESMBillSummaryList As New List(Of CollectionNoticeDetails)
    '        ' ''Dim WithBalance = (From x In AMWESMBill Where x.BillingPeriod < CDbl(CurrentBP) And x.Balance <> 0 Select x)
    '        ' ''For Each itemWithBalance In WithBalance
    '        ' ''    Dim SavePreviousWESMBillsSummary As New CollectionNoticeDetails
    '        ' ''    Dim CurrentParticipant = CDbl(itemWithBalance.IDNumber.ToString)
    '        ' ''    'Get Current Ref No for Saving in details for Participant
    '        ' ''    Dim CurrentRefNo = (From x In CurrentCollectionNotice Where x.IDNumber = CurrentParticipant Order By x.ReferenceNo Descending _
    '        ' ''                        Select x.ReferenceNo).FirstOrDefault.ToString
    '        ' ''    With SavePreviousWESMBillsSummary
    '        ' ''        .BillingPeriod = itemWithBalance.BillingPeriod
    '        ' ''        .DueDate = itemWithBalance.DueDate
    '        ' ''        .Total = itemWithBalance.Balance
    '        ' ''        .InvoiceNumber = itemWithBalance.InvoiceNumber
    '        ' ''        .InvoiceDate = itemWithBalance.InvoiceDate
    '        ' ''        .TransactionType = "P"
    '        ' ''        .ReferenceNo = CDbl(CurrentRefNo)
    '        ' ''        .ParticipantID = CurrentParticipant
    '        ' ''        If itemWithBalance.ChargeType = EnumChargeType.E Then
    '        ' ''            .Energy = itemWithBalance.Balance
    '        ' ''        ElseIf itemWithBalance.ChargeType = EnumChargeType.EV Then
    '        ' ''            .VATEnergy = itemWithBalance.Balance
    '        ' ''        ElseIf itemWithBalance.ChargeType = EnumChargeType.MF Then
    '        ' ''            .MF = itemWithBalance.Balance
    '        ' ''        ElseIf itemWithBalance.ChargeType = EnumChargeType.MFV Then
    '        ' ''            .VATMF = itemWithBalance.Balance
    '        ' ''        End If
    '        ' ''    End With
    '        ' ''    SavePreviousWESMBillSummaryList.Add(SavePreviousWESMBillsSummary)
    '        ' ''Next
    '        ' ''WBillHelper.SaveCollectionNoticeDetails(SavePreviousWESMBillSummaryList)

    '        Dim CurrentCollectionDetails = WBillHelper.GetCollectionNoticeDetails()
    '        Dim ForReportCurrentDetails = (From x In CurrentCollectionDetails Where x.BillingPeriod = CDbl(CurrentBP) And x.TransactionType = "C" Order By x.BillingPeriod _
    '                                          Ascending, x.InvoiceNumber Ascending).ToList
    '        Dim ForReportPreviousDetails = (From x In CurrentCollectionDetails Where x.BillingPeriod < CDbl(CurrentBP) And x.TransactionType = "P" Order By x.BillingPeriod _
    '                                        Ascending, x.InvoiceNumber Ascending).ToList

    '        'Place Dummy data on Previous details without records in DB in order to 
    '        'show the participant in the report viewer.
    '        For Each item In CurrentCollectionNotice
    '            Dim tmpReferenceNo = item.ReferenceNo
    '            Dim tmpParticipantId = item.IDNumber
    '            Dim CheckIfPrevious = (From x In CurrentCollectionDetails Where x.ReferenceNo = tmpReferenceNo And x.TransactionType = "P").ToList
    '            If CheckIfPrevious.Count = 0 Then
    '                Dim ForDummyPrevious As New CollectionNoticeDetails
    '                With ForDummyPrevious
    '                    .ReferenceNo = tmpReferenceNo
    '                    .ParticipantID = tmpParticipantId
    '                    .Total = 0
    '                End With
    '                ForReportPreviousDetails.Add(ForDummyPrevious)
    '            End If
    '        Next

    '        'Save Tables to Dataset for Report
    '        Dim ds = WBillHelper.SaveCollectionDataSet(Participants, ForReportCurrentNotice, ForReportCurrentDetails, ForReportPreviousDetails)
    '        frmProgress.Show()
    '        Dim frmReport As New frmReportViewer
    '        With frmReport
    '            .LoadWESMBillCollectionNotice(ds)
    '            ShowDialog()
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
    '    End Try
    'End Sub

    Private Sub CHB_AllParticipants_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CHB_AllParticipants.CheckedChanged
        If CHB_AllParticipants.Checked = True Then
            LB_ParentParticipants.Enabled = False
        Else
            LB_ParentParticipants.Enabled = True
            LB_ParentParticipants.SelectedIndex = 0
        End If
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim CurrentBilling = WBillHelper.GetCalendarBP()
    '    Dim CurrentBillingPeriod As Integer = CInt((From x In CurrentBilling Select x.BillingPeriod Order By BillingPeriod Descending).FirstOrDefault.ToString)

    '    'Get Current WESM Bills and All Summaries
    '    Dim WESMBills = WBillHelper.GetWESMBills(CurrentBillingPeriod)
    '    Dim WESMBillsSummary = WBillHelper.GetWESMBillSummary()

    '    'Get Participants and Participants Mapping
    '    Dim AllParticipants = WBillHelper.GetParticipantIDs()
    '    If CHB_AllParticipants.Checked = False Then
    '        AllParticipants = (From x In AllParticipants Where x.ParticipantID = LB_ParentParticipants.SelectedItem.ToString).ToList
    '    End If
    '    Dim CurrentParticipants = (From x In WESMBills Select x.IDNumber Distinct)
    '    Dim CurrentParticipant = (From x In AllParticipants Join y In CurrentParticipants On x.IDNumber Equals y Select x).ToList
    '    Dim ParticipantMapping = WBillHelper.GetParentChildMapping(1)

    '    Dim CollectionNoticeHeader As New List(Of CollectionNotice)

    '    For Each item In AllParticipants
    '        Dim ParticipantID = item.IDNumber
    '        Dim CheckParent = (From x In ParticipantMapping Where x.IDNumber = ParticipantID And x.Status = 1 And x.ParentFlag = 1)
    '        Dim _CollectionNoticeHeader As New CollectionNotice
    '        If CheckParent.Count = 1 Then
    '            For Each record In CheckParent
    '                With _CollectionNoticeHeader
    '                    .IDNumber = CInt(ParticipantID)
    '                    .PreparedBy = "JCLP - PREPARED"
    '                    .ApprovedBy = "JCLP - APPROVED"
    '                    .UpdatedBy = "JCLP - UPDATED"
    '                End With
    '                CollectionNoticeHeader.Add(_CollectionNoticeHeader)
    '            Next
    '        End If
    '    Next

    '    WBillHelper.SaveCollectionNotice(CollectionNoticeHeader)

    '    Dim CollectionNotice = WBillHelper.GetCollectionNotice()

    '    'Get Previous Summary
    '    Dim NoticeDetails As New List(Of CollectionNoticeDetails)

    '    For Each item In AllParticipants
    '        Dim ParticipantID = item.IDNumber
    '        Dim ParticipantBillSummary = (From x In WESMBillsSummary Where x.IDNumber.IDNumber = ParticipantID And x.EndingBalance <> 0 And x.BillPeriod < CurrentBillingPeriod)
    '        Dim CurrentReferenceNo As Double
    '        Dim GetMappingCount = (From x In ParticipantMapping Where x.IDNumber = ParticipantID And x.Status = 1).Count
    '        If GetMappingCount <> 0 Then
    '            Dim GetMapping = (From x In ParticipantMapping Where x.IDNumber = ParticipantID And x.Status = 1 Select x).FirstOrDefault

    '            If GetMapping.ParentFlag = 1 Then
    '                CurrentReferenceNo = CDbl((From x In CollectionNotice Where x.IDNumber = ParticipantID Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault)
    '            Else
    '                CurrentReferenceNo = CDbl((From x In CollectionNotice Where x.IDNumber = GetMapping.PCNumber Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault)
    '            End If

    '            For Each record In ParticipantBillSummary
    '                Dim _PreviousBills As New CollectionNoticeDetails
    '                With _PreviousBills
    '                    If GetMapping.ParentFlag = 1 Then
    '                        .ParticipantID = ParticipantID
    '                    Else
    '                        .ParticipantID = GetMapping.PCNumber
    '                    End If
    '                    .BillingPeriod = record.BillPeriod
    '                    .DueDate = record.DueDate
    '                    .Total = record.EndingBalance
    '                    .TransactionType = "P"
    '                    .ReferenceNo = CurrentReferenceNo
    '                    If record.ChargeType = EnumChargeType.E Then
    '                        .Energy = record.EndingBalance
    '                    ElseIf record.ChargeType = EnumChargeType.EV Then
    '                        .VATEnergy = record.EndingBalance
    '                    ElseIf record.ChargeType = EnumChargeType.MF Then
    '                        .MF = record.EndingBalance
    '                    ElseIf record.ChargeType = EnumChargeType.MFV Then
    '                        .VATMF = record.EndingBalance
    '                    End If
    '                End With
    '                'Add Previous bills
    '                'Added 11/30/2011
    '                If record.ChargeType = EnumChargeType.E Then
    '                    If record.EndingBalance < 0 Then
    '                        NoticeDetails.Add(_PreviousBills)
    '                    End If
    '                Else
    '                    NoticeDetails.Add(_PreviousBills)
    '                End If
    '            Next

    '            'Current Bills
    '            WESMBills = (From x In WESMBills Where x.IDNumber = ParticipantID).ToList
    '            For Each record In WESMBills
    '                Dim _CurrentBills As New CollectionNoticeDetails
    '                With _CurrentBills
    '                    If GetMapping.ParentFlag = 1 Then
    '                        .ParticipantID = ParticipantID
    '                    Else
    '                        .ParticipantID = GetMapping.PCNumber
    '                    End If
    '                    .BillingPeriod = record.BillingPeriod
    '                    .DueDate = record.DueDate
    '                    .Total = record.Amount
    '                    .TransactionType = "P"
    '                    .ReferenceNo = CurrentReferenceNo
    '                    If record.ChargeType = EnumChargeType.E Then
    '                        .Energy = record.Amount
    '                    ElseIf record.ChargeType = EnumChargeType.EV Then
    '                        .VATEnergy = record.Amount
    '                    ElseIf record.ChargeType = EnumChargeType.MF Then
    '                        .MF = record.Amount
    '                    ElseIf record.ChargeType = EnumChargeType.MFV Then
    '                        .VATMF = record.Amount
    '                    End If
    '                End With
    '                ' ADD CurrentBills
    '                If record.ChargeType = EnumChargeType.E Then
    '                    If record.Amount < 0 Then
    '                        NoticeDetails.Add(_CurrentBills)
    '                    End If
    '                Else
    '                    NoticeDetails.Add(_CurrentBills)
    '                End If
    '            Next
    '        End If
    '    Next
    '    WBillHelper.SaveCollectionNoticeDetails(NoticeDetails)


    '    Dim CollectionNoticeDetails = WBillHelper.GetCollectionNoticeDetails()

    '    Dim CollectionNoticenumber = (From x In CollectionNotice Select x.CNNumber Order By CNNumber Descending).FirstOrDefault.ToString
    '    Dim ForReportCurrentHeader = (From x In CollectionNotice Where x.CNNumber = CLng(CollectionNoticenumber)).ToList

    '    Dim PreviousCollectionNoticeDetails = (From x In CollectionNoticeDetails Where x.TransactionType = "P" And x.BillingPeriod < CurrentBillingPeriod _
    '                                           Order By x.BillingPeriod Ascending, x.InvoiceNumber Ascending).ToList

    '    Dim CurrentCollectionNoticeDetails = (From x In CollectionNoticeDetails Where x.TransactionType = "C" And x.BillingPeriod = CurrentBillingPeriod _
    '                                         Order By x.BillingPeriod Ascending, x.InvoiceNumber Ascending).ToList
    '    Dim _tmpCurrentNoticeList As New List(Of CollectionNoticeDetails)
    '    Dim _tmpPreviousNoticeList As New List(Of CollectionNoticeDetails)

    '    For Each item In ForReportCurrentHeader
    '        Dim ItemRefNo = item.ReferenceNo
    '        CurrentCollectionNoticeDetails = (From x In CollectionNoticeDetails Where x.TransactionType = "C" And x.BillingPeriod = CurrentBillingPeriod _
    '                                          And x.ReferenceNo = ItemRefNo Order By x.BillingPeriod Ascending, x.InvoiceNumber Ascending).ToList
    '        For Each record In CurrentCollectionNoticeDetails
    '            Dim _tmpCurrentNotice As New CollectionNoticeDetails
    '            With _tmpCurrentNotice
    '                .BillingPeriod = record.BillingPeriod
    '                .DueDate = record.DueDate
    '                .Energy = record.Energy
    '                .InvoiceDate = record.InvoiceDate
    '                .InvoiceNumber = record.InvoiceNumber
    '                .MF = record.MF
    '                .ParentId = record.ParentId
    '                .ParticipantID = record.ParticipantID
    '                .ReferenceNo = record.ReferenceNo
    '                .Total = record.Total
    '                .TransactionType = record.TransactionType
    '                .UpdatedBy = record.UpdatedBy
    '                .UpdatedDate = record.UpdatedDate
    '                .VATEnergy = record.VATEnergy
    '                .VATMF = record.VATMF
    '                .ParticipantID = item.IDNumber
    '            End With
    '            _tmpCurrentNoticeList.Add(_tmpCurrentNotice)
    '        Next
    '    Next

    '    For Each item In ForReportCurrentHeader
    '        Dim ItemRefNo = item.ReferenceNo
    '        PreviousCollectionNoticeDetails = (From x In CollectionNoticeDetails Where x.TransactionType = "P" And x.BillingPeriod < CurrentBillingPeriod _
    '                                          Order By x.BillingPeriod Ascending, x.InvoiceNumber Ascending).ToList
    '        For Each record In PreviousCollectionNoticeDetails
    '            Dim _tmpPreviousotice As New CollectionNoticeDetails
    '            With _tmpPreviousotice
    '                .BillingPeriod = record.BillingPeriod
    '                .DueDate = record.DueDate
    '                .Energy = record.Energy
    '                .InvoiceDate = record.InvoiceDate
    '                .InvoiceNumber = record.InvoiceNumber
    '                .MF = record.MF
    '                .ParentId = record.ParentId
    '                .ParticipantID = record.ParticipantID
    '                .ReferenceNo = record.ReferenceNo
    '                .Total = record.Total
    '                .TransactionType = record.TransactionType
    '                .UpdatedBy = record.UpdatedBy
    '                .UpdatedDate = record.UpdatedDate
    '                .VATEnergy = record.VATEnergy
    '                .VATMF = record.VATMF
    '                .ParticipantID = item.IDNumber
    '            End With
    '            _tmpPreviousNoticeList.Add(_tmpPreviousotice)
    '        Next

    '    Next

    '    'insert dummy data on List to be sent to Crystal Reports
    '    For Each item In AllParticipants
    '        Dim ParticipantID = item.IDNumber
    '        Dim ParticipantBillSummary = (From x In WESMBillsSummary Where x.IDNumber.IDNumber = ParticipantID And x.EndingBalance <> 0 And x.BillPeriod < CurrentBillingPeriod)
    '        Dim CurrentReferenceNo As Double
    '        Dim GetMappingcount = (From x In ParticipantMapping Where x.IDNumber = ParticipantID And x.Status = 1).Count
    '        Dim GetParticipantParentChild = (From x In ParticipantMapping Where x.IDNumber = ParticipantID And x.Status = 1).FirstOrDefault
    '        If GetMappingcount <> 0 Then
    '            If GetParticipantParentChild.ParentFlag = 1 Then
    '                CurrentReferenceNo = CDbl((From x In CollectionNotice Where x.IDNumber = ParticipantID Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault)
    '            Else
    '                CurrentReferenceNo = CDbl((From x In CollectionNotice Where x.IDNumber = GetParticipantParentChild.PCNumber Select x.ReferenceNo Order By ReferenceNo Descending).FirstOrDefault)
    '            End If

    '            Dim CheckPrevious = (From x In CollectionNoticeDetails Where x.ReferenceNo = CurrentReferenceNo And x.TransactionType = "P").ToList

    '            If CheckPrevious.Count = 0 Then
    '                Dim ForDummyPrevious As New CollectionNoticeDetails
    '                With ForDummyPrevious
    '                    .ReferenceNo = CurrentReferenceNo
    '                    If GetParticipantParentChild.ParentFlag = 1 Then
    '                        .ParticipantID = ParticipantID
    '                    Else
    '                        .ParticipantID = GetParticipantParentChild.PCNumber
    '                    End If
    '                    .Total = 0
    '                End With
    '                PreviousCollectionNoticeDetails.Add(ForDummyPrevious)
    '            End If
    '        End If
    '    Next

    '    ''Dim ds = WBillHelper.SaveCollectionDataSet(AllParticipants, ForReportCurrentHeader, _tmpCurrentNoticeList, _tmpPreviousNoticeList)
    '    'frmProgress.Show()
    '    'Dim frmReport As New frmReportViewer
    '    'With frmReport
    '    '    .LoadWESMBillCollectionNotice(ds)
    '    '    .ShowDialog()
    '    'End With
    'End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ''For saving to Database 
            '' CNHeader - Collection Notice Header

            ''Dictionary for Participant-ReferenceNo
            'Dim dicReferenceNo As New Dictionary(Of Long, Long)
            'dicReferenceNo.Add(0, 0)
            'Dim RefNo As Integer = 1
            ''Get Document Signatories
            'Dim _SignatoriesCN = WBillHelper.GetSignatories("CN").First
            ''Get DMCM Details
            'Dim DMCMDetails = WBillHelper.GetDebitCreditMemoDetails()
            ''Get All Participants
            'Dim AllParticipants = WBillHelper.GetParticipantIDs()
            ''Get WESMBillsSummary
            'Dim WESMSummary = WBillHelper.GetWESMBillSummary()
            ''Get Offsetting Details
            'Dim Offsetdetails = WBillHelper.GetWESMBillOffsetDetails()
            ''Get Current BP
            'Dim currBillPeriod As Integer = CInt((From x In WESMSummary _
            '                                     Select x.BillPeriod _
            '                                     Order By BillPeriod Descending).FirstOrDefault.ToString)
            ''Get Current Due Date
            'Dim lstDueDate = (From x In WESMSummary _
            '                  Where x.BillPeriod = currBillPeriod _
            '                  Select x.DueDate Distinct).ToList

            ''Get Calendar Billing Period
            'Dim BillCalendar = WBillHelper.GetCalendarBP(currBillPeriod)
            ''Get All WESM Bills
            'Dim AllWESMBills = WBillHelper.GetWESMBills()
            ''Get Current Invoices
            'Dim cWESMBills = (From x In AllWESMBills _
            '                  Where x.BillingPeriod = currBillPeriod).ToList
            ''Get Current WESM Bill Participants
            'Dim currParticipants As New List(Of AMParticipants)
            'currParticipants = (From x In AllWESMBills _
            '                    Join y In AllParticipants _
            '                    On x.IDNumber Equals y.IDNumber _
            '                    Where x.BillingPeriod = currBillPeriod _
            '                    Select y Distinct _
            '                    Order By y.ParticipantID Ascending).ToList

            'Dim SelectedParticipants As New List(Of AMParticipants)
            'Dim cParticipants As New List(Of AMParticipants)
            ''Prepare Participants with Collection Notice
            'If CHB_AllParticipants.Checked = False Then
            '    If LB_ParentParticipants.SelectedItems.Count = 1 Then
            '        'if user chooses only one participant to generate notice
            '        SelectedParticipants = (From x In AllParticipants _
            '                                Where x.ParticipantID = LB_ParentParticipants.SelectedItem.ToString).ToList
            '    Else
            '        'if user chooses more than one participant
            '        For Each item In LB_ParentParticipants.SelectedItems
            '            Dim curSelect = item.ToString
            '            SelectedParticipants.Add((From x In AllParticipants _
            '                                      Where x.ParticipantID = curSelect).FirstOrDefault)
            '        Next
            '    End If
            'Else
            '    SelectedParticipants = currParticipants
            'End If

            ''Get Parent Child Mapping for All Billing Period
            'Dim PCMapping = WBillHelper.GetP2CMappingAllBP()

            ''Get All Parents on the list for the current Billing Period
            'Dim lstParentParticipant = (From x In SelectedParticipants _
            '                            Join y In PCMapping _
            '                            On x.IDNumber Equals y.IDNumber _
            '                            Where _
            '                            y.Status = 1 _
            '                            And y.ParentFlag = 1 _
            '                            And y.BillPeriod = currBillPeriod _
            '                            Select x).ToList

            'If lstParentParticipant.Count = 0 Then
            '    MsgBox("No Mapping was found for the current Billing Period.", MsgBoxStyle.Critical, "Error!")
            '    Exit Sub
            'End If


            ''Generate CN Header
            'Dim CNHeader As New List(Of CollectionNotice)
            'For Each item In lstParentParticipant
            '    Dim cParticipantID = item.IDNumber
            '    Dim _CNHeader As New CollectionNotice
            '    With _CNHeader
            '        .IDNumber = CInt(cParticipantID)
            '        .CNDate = DateTime.Now
            '        .PreparedBy = _SignatoriesCN.PreparedBy
            '        .ApprovedBy = _SignatoriesCN.ApprovedBy
            '        .UpdatedBy = _SignatoriesCN.UpdatedBy
            '        .ReferenceNo = RefNo
            '    End With
            '    dicReferenceNo.Add(CInt(cParticipantID), RefNo)
            '    RefNo += 1
            '    CNHeader.Add(_CNHeader)
            'Next

            ''Loop all Current Parent Participant
            'Dim prevBills As New List(Of CollectionNoticeDetails)
            'Dim currBills As New List(Of CollectionNoticeDetails)
            'For Each item In lstParentParticipant
            '    Dim currParticipant = item.IDNumber

            '    'Get Current Participant's Previous Sumamry
            '    Dim prevSummary = (From x In WESMSummary _
            '                       Where x.BillPeriod < currBillPeriod _
            '                       And x.IDNumber.IDNumber = currParticipant _
            '                       And x.EndingBalance <> 0 _
            '                       Select x Order By x.BillPeriod Ascending, x.ChargeType Ascending).ToList

            '    For Each preSumm In prevSummary
            '        Dim cbillPd = preSumm.BillPeriod
            '        'is participant a Parent of the Previous Summary
            '        Dim isParticipantParent = (From x In AllParticipants _
            '                                   Join y In PCMapping _
            '                                   On x.IDNumber Equals y.IDNumber _
            '                                   Where y.Status = 1 _
            '                                   And x.Status = 1 _
            '                                   And y.PCNumber = 1 _
            '                                   And y.BillPeriod = cbillPd _
            '                                   And x.IDNumber = currParticipant _
            '                                   Select x).FirstOrDefault

            '        Dim preBills As New CollectionNoticeDetails
            '        With preBills
            '            .ParentId = CStr(currParticipant)
            '            .ParticipantID = Me.GetMPIDMPName(CInt(preSumm.IDNumber.IDNumber), AllParticipants) 'preSumm.IDNumber.IDNumber & " " & preSumm.IDNumber.ParticipantID
            '            .BillingPeriod = preSumm.BillPeriod
            '            .TransactionType = "P"
            '            .INVDMCM = CStr(IIf(preSumm.SummaryType = EnumSummaryType.DCM, "DMCM-", "INV-")) & preSumm.INVDMCMNo
            '            .ReferenceNo = dicReferenceNo(currParticipant)
            '            .Total = preSumm.EndingBalance
            '            If preSumm.SummaryType = EnumSummaryType.INV Then
            '                .InvoiceDate = Me.GetINVDate(preSumm.INVDMCMNo, AllWESMBills)
            '                .DueDate = Me.GetINVDueDate(preSumm.INVDMCMNo, AllWESMBills)
            '            ElseIf preSumm.SummaryType = EnumSummaryType.DCM Then
            '                .InvoiceDate = Me.GetDMCMDate(preSumm.INVDMCMNo,dmcmdetails, AllWESMBills)
            '                .DueDate = Me.GetDMCMDueDate(preSumm.INVDMCMNo, DMCMDetails, AllWESMBills)
            '            End If
            '            .SummaryType = preSumm.SummaryType
            '            Select Case preSumm.ChargeType
            '                Case EnumChargeType.E
            '                    .Energy = preSumm.EndingBalance
            '                Case EnumChargeType.EV
            '                    .VATEnergy = preSumm.EndingBalance
            '                Case EnumChargeType.MF Or EnumChargeType.MFV
            '                    .MF += preSumm.EndingBalance
            '            End Select
            '        End With
            '        preBills.Total = preSumm.EndingBalance
            '        prevBills.Add(preBills)
            '    Next

            '    'Loop DueDates if Any
            '    For Each lstDue In lstDueDate
            '        Dim _dueDate = lstDue

            '        'Get Current Participant's Current Billing Period Summary (Invoices first)
            '        Dim pCurINVSummary = (From x In WESMSummary _
            '                           Where x.BillPeriod = currBillPeriod _
            '                           And x.IDNumber.IDNumber = currParticipant _
            '                           And x.DueDate = _dueDate _
            '                           And x.SummaryType = EnumSummaryType.INV _
            '                           And x.EndingBalance <> 0 _
            '                           Select x Order By x.ChargeType Ascending).ToList

            '        For Each wBill In pCurINVSummary
            '            Dim cBill As New CollectionNoticeDetails
            '            Dim curGroupNo = wBill.GroupNo
            '            Dim curInvNum = wBill.INVDMCMNo
            '            Dim cChargeType = wBill.ChargeType
            '            Dim cOffsetting = (From x In Offsetdetails _
            '                               Where x.GroupNo = curGroupNo _
            '                               And x.InvoiceNumber = curInvNum _
            '                               Select x).FirstOrDefault

            '            Dim cDMCMDetails = (From x In Offsetdetails _
            '                                Where x.DMCMNumber = cOffsetting.DMCMNumber _
            '                                And x.DMCMNumber <> 0 _
            '                                And x.InvoiceNumber <> curInvNum _
            '                                Select x).ToList

            '            'Add Summary first
            '            'Get Invoice Details
            '            Dim summInvDetails = (From x In AllWESMBills _
            '                                  Where x.InvoiceNumber = curInvNum _
            '                                  And x.DueDate = _dueDate _
            '                                  And x.ChargeType = cChargeType _
            '                                  Select x).ToList

            '            For Each invDetails In summInvDetails
            '                With cBill
            '                    .BillingPeriod = currBillPeriod
            '                    .ParentId = CStr(currParticipant)
            '                    .ParticipantID = Me.GetMPIDMPName(invDetails.IDNumber, AllParticipants)
            '                    .TransactionType = "C"
            '                    .INVDMCM = CStr(invDetails.InvoiceNumber)
            '                    .DueDate = invDetails.DueDate
            '                    .InvoiceDate = invDetails.InvoiceDate
            '                    .ReferenceNo = dicReferenceNo(currParticipant)
            '                    Select Case invDetails.ChargeType
            '                        Case EnumChargeType.E
            '                            .Energy = invDetails.Amount
            '                        Case EnumChargeType.EV
            '                            .VATEnergy = invDetails.Amount
            '                        Case EnumChargeType.MF Or EnumChargeType.MFV
            '                            .MF += invDetails.Amount
            '                    End Select
            '                    cBill.Total = wBill.EndingBalance
            '                    currBills.Add(cBill)
            '                End With
            '            Next

            '            'If Item has DMCM Details
            '            If cDMCMDetails.Count <> 0 Then
            '                For Each InvDMCM In cDMCMDetails
            '                    Dim detBills As New CollectionNoticeDetails
            '                    Dim cInvNumber = InvDMCM.InvoiceNumber
            '                    'Get Invoice Details
            '                    Dim invDetails = (From x In AllWESMBills _
            '                                      Where x.InvoiceNumber = cInvNumber _
            '                                      And x.DueDate = _dueDate _
            '                                      And x.ChargeType = cChargeType _
            '                                      Select x).ToList
            '                    For Each details In invDetails
            '                        With detBills
            '                            .ParentId = CStr(currParticipant)
            '                            .ParticipantID = Me.GetMPIDMPName(details.IDNumber, AllParticipants)
            '                            .TransactionType = "C"
            '                            .BillingPeriod = currBillPeriod
            '                            .INVDMCM = CStr(details.InvoiceNumber)
            '                            .DueDate = details.DueDate
            '                            .InvoiceDate = details.InvoiceDate
            '                            .ReferenceNo = dicReferenceNo(currParticipant)

            '                            Select Case details.ChargeType
            '                                Case EnumChargeType.E
            '                                    .Energy = details.Amount
            '                                Case EnumChargeType.EV
            '                                    .VATEnergy = details.Amount
            '                                Case EnumChargeType.MF Or EnumChargeType.MFV
            '                                    .MF += details.Amount
            '                            End Select
            '                        End With
            '                        currBills.Add(detBills)
            '                    Next
            '                Next
            '            End If
            '        Next

            '        'Get Current Participant's Current Billing Period Summary (DMCM)
            '        Dim pCurDMCMSummary = (From x In WESMSummary _
            '                           Where x.BillPeriod = currBillPeriod _
            '                           And x.DueDate = _dueDate _
            '                           And x.IDNumber.IDNumber = currParticipant _
            '                           And x.SummaryType = EnumSummaryType.DCM _
            '                           Select x Order By x.ChargeType Ascending).ToList

            '        For Each curDMCM In pCurDMCMSummary
            '            Dim cBill As New CollectionNoticeDetails
            '            Dim _dmcmNumber As Long = curDMCM.INVDMCMNo
            '            Dim cChargeType = curDMCM.ChargeType


            '            'Input Summary to Collection Notice
            '            With cBill
            '                .ParentId = CStr(currParticipant)
            '                Dim chargeType As String = CStr(IIf(curDMCM.ChargeType = EnumChargeType.E, " ENERGY ", _
            '                                                       IIf(curDMCM.ChargeType = EnumChargeType.EV, " VAT ON ENERGY ", _
            '                                                           IIf(curDMCM.ChargeType = EnumChargeType.MF Or curDMCM.ChargeType = EnumChargeType.MFV, _
            '                                                               " MARKET FEES ", " MARKET FEES "))))
            '                .ParticipantID = "DMCM Ref. " & chargeType & ":"
            '                .InvoiceDate = Nothing
            '                .BillingPeriod = curDMCM.BillPeriod
            '                .TransactionType = "C"
            '                .INVDMCM = CStr(curDMCM.INVDMCMNo)
            '                .SummaryType = EnumSummaryType.DCM
            '                .ReferenceNo = dicReferenceNo(currParticipant)
            '                cBill.Total = curDMCM.EndingBalance
            '                currBills.Add(cBill)
            '            End With
            '        Next

            '        'Get Child of Parent
            '        Dim ChildOfParent = (From x In PCMapping _
            '                             Where x.PCNumber = currParticipant _
            '                             And x.ParentFlag <> 1 _
            '                             And x.BillPeriod = currBillPeriod _
            '                             Select x.IDNumber Distinct).ToList

            '        For Each C2P In ChildOfParent
            '            Dim chId As Double = CDbl(C2P.ToString)
            '            Dim lstChldInvoice = (From x In AllWESMBills _
            '                                  Where x.BillingPeriod = currBillPeriod _
            '                                  And x.DueDate = _dueDate _
            '                                  And x.IDNumber = chId _
            '                                  Select x Order By x.ChargeType Ascending).ToList

            '            Dim lstChldInvoiceNo = (From x In lstChldInvoice _
            '                                    Select x.InvoiceNumber Distinct).ToList

            '            For Each invNo In lstChldInvoiceNo
            '                Dim detBills As New CollectionNoticeDetails
            '                Dim cInvoiceNumber = invNo
            '                Dim getInvDetails = (From x In AllWESMBills _
            '                                     Where x.InvoiceNumber = cInvoiceNumber _
            '                                     Select x).ToList
            '                For Each details In getInvDetails
            '                    With detBills
            '                        .ParentId = CStr(currParticipant)
            '                        .ParticipantID = Me.GetMPIDMPName(details.IDNumber, AllParticipants)
            '                        .TransactionType = "C"
            '                        .BillingPeriod = currBillPeriod
            '                        .INVDMCM = CStr(details.InvoiceNumber)
            '                        .DueDate = details.DueDate
            '                        .InvoiceDate = details.InvoiceDate
            '                        .ReferenceNo = dicReferenceNo(currParticipant)
            '                        Select Case details.ChargeType
            '                            Case EnumChargeType.E
            '                                .Energy = details.Amount
            '                            Case EnumChargeType.EV
            '                                .VATEnergy = details.Amount
            '                            Case EnumChargeType.MF Or EnumChargeType.MFV
            '                                .MF += details.Amount
            '                        End Select
            '                    End With
            '                Next
            '                currBills.Add(detBills)
            '            Next
            '        Next
            '    Next
            'Next


            'WBillHelper.SaveCollectionNotice(CNHeader, currBills, prevBills)


            ''For Report Generation
            ''get max collection Notice number
            'Dim cCollectionNotice = WBillHelper.GetCollectionNotice()
            'Dim maxCNNumber = (From x In cCollectionNotice _
            '                            Select x.CNNumber).Max
            'Dim currCollectionNotice = (From x In cCollectionNotice _
            '                            Where x.CNNumber = maxCNNumber _
            '                            Select x)
            'Dim dicNewRefNo As New Dictionary(Of Long, Long)
            'dicNewRefNo.Add(0, 0)



            'For Each item In currCollectionNotice
            '    dicNewRefNo.Add(item.IDNumber, CLng(item.ReferenceNo))
            'Next

            'Dim cRemarks = WBillHelper.GetCollectionNotice()
            ''update Reference Number of all lists
            'For Each item In CNHeader
            '    Dim cParticipant = item.IDNumber
            '    With item
            '        .ReferenceNo = dicNewRefNo(cParticipant)
            '    End With
            'Next

            'For Each item In prevBills
            '    Dim cParticipant = CLng(item.ParentId)
            '    With item
            '        .ReferenceNo = dicNewRefNo(cParticipant)
            '    End With
            'Next

            'For Each item In currBills
            '    Dim cParticipant = CLng(item.ParentId)
            '    With item
            '        .ReferenceNo = dicNewRefNo(cParticipant)
            '    End With
            'Next


            ''Add Blank Data in PreviousBills to show Collection Notice (in order for participant to have a Collection Notice
            'For Each item In currCollectionNotice
            '    Dim cParticipant = item.IDNumber

            '    'check if there's Previous Bills
            '    Dim isTherePrevious = (From x In prevBills _
            '                           Where x.ParentId = CStr(cParticipant) _
            '                           And x.TransactionType = "P" _
            '                           And x.BillingPeriod < currBillPeriod _
            '                           Select x).ToList

            '    If isTherePrevious.Count = 0 Then
            '        Dim dmyPrevious As New CollectionNoticeDetails
            '        With dmyPrevious
            '            .ParentId = CStr(cParticipant)
            '            .ParticipantID = Me.GetMPIDMPName(cParticipant, AllParticipants)
            '            .ReferenceNo = dicNewRefNo(cParticipant)
            '            .INVDMCM = "0"
            '            .TransactionType = "P"
            '            .Energy = 0
            '            .VATEnergy = 0
            '            .MF = 0
            '            .Total = 0
            '        End With
            '        prevBills.Add(dmyPrevious)
            '    End If
            'Next

            'Dim DateDue = CDate((From x In currBills Select x.DueDate Distinct).FirstOrDefault.ToString)
            'Dim ds = WBillHelper.SaveCollectionDataSet(AllParticipants, CNHeader, currBills, prevBills, BillCalendar)



            'For Remarks
            Dim LoadMgt As New frmRPTCollectionNoticeMgt
            With LoadMgt
                '         .LoadParticipants(CNHeader, CLng(maxCNNumber))
                '         .LoadForReportDataset(ds, DateDue)
                .ShowDialog()
            End With


            'frmProgress.Show()
            'Dim frmReport As New frmReportViewer
            'With frmReport
            '    .LoadWESMBillCollectionNotice(ds, DateDue)
            '    .ShowDialog()
            'End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GetMPIDMPName(ByVal IDNumber As Integer, ByVal ParticipantList As List(Of AMParticipants)) As String
        Dim MPIDName As String = ""
        Dim cParticipant = (From x In ParticipantList _
                           Where x.IDNumber = IDNumber).FirstOrDefault
        MPIDName = cParticipant.IDNumber & "   " & cParticipant.ParticipantID
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
                   On x.INVDMCMNo Equals y.InvoiceNumber _
                   Where x.DMCMNumber = DMCMNo _
                   Select y.DueDate Order By DueDate Descending).FirstOrDefault
        Return retDate
    End Function

    Private Function GetINVDate(ByVal InvoiceNo As Long, ByVal AllInvoices As List(Of WESMBill)) As Date
        Dim retDate As Date
        retDate = (From x In AllInvoices _
                       Where x.InvoiceNumber = InvoiceNo _
                       Select x.InvoiceDate).FirstOrDefault
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
End Class