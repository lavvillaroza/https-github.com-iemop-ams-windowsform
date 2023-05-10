'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionSummary
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     July 25, 2012
'Development Group:      Software Development and Support Division
'Description:            Report generator for collection summary
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   July 25, 2012           Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmCollectionSummary

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private _flag As Integer
    Public Property flag() As Integer
        Get
            Return _flag
        End Get
        Set(ByVal value As Integer)
            _flag = value
        End Set
    End Property


    Public Sub frmCollectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        BFactory = BusinessFactory.GetInstance()

        Select Case Me.flag
            Case 1, 2
                Me.Text = "Collection Summary"
                Me.lblFilter.Text = "Enter allocation date:"

            Case 2
                Me.Text = "Collection Summary"
                Me.lblFilter.Text = "Enter allocation date:"

            Case 3
                Me.Text = "Daily Collection Summary"
                Me.lblFilter.Text = "Enter collection date:"
                Me.DGridMain.Columns("colTransDate").HeaderText = "CollectionDate"

        End Select
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.DGridMain.Rows.Clear()

        If CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate)) > CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate)) Then
            MsgBox("Invalid date range!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Sub
        End If

        Dim listJV As New List(Of JournalVoucher)
        Dim dateFrom As Date = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
        Dim dateTo As Date = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate)).AddHours(23).AddMinutes(59)

        Select Case Me.flag
            Case 1, 2
                listJV = WBillHelper.GetJournalVoucher(dateFrom, dateTo, EnumPostedType.C)

            Case 3
                listJV = WBillHelper.GetDailyJournalVoucher(dateFrom, dateTo)
        End Select

        If listJV.Count = 0 Then
            MsgBox("No records found!", MsgBoxStyle.Information, "No data")
            Exit Sub
        End If

        For Each item In listJV
            With item
                Me.DGridMain.Rows.Add(FormatDateTime(.JVDate, DateFormat.ShortDate), .BatchCode, .JVNumber)
            End With
        Next

    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        If Me.DGridMain.RowCount = 0 Then
            MsgBox("Nothing to generate!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        Try
            ProgressThread.Show("Please wait while preparing Collection Summary.")
            Dim batchCode As String = CStr(Me.DGridMain.CurrentRow.Cells("colBatchCode").Value)
            Dim JVNo As Long = CLng(Me.DGridMain.CurrentRow.Cells("colJVNo").Value)

            Select Case flag
                'For Collection Summary Report
                Case 1
                   
                    Dim TotalCash As Decimal = 0, TotalDrawdown As Decimal = 0

                    'Get the date today
                    Dim DocumentDate As String = SystemDate.ToString("MM/dd/yyyy") 'SystemDate.ToString("MM/dd/yyyy") 

                    'Get the Signatories
                    Dim Signatories = WBillHelper.GetSignatories("CS").First()

                    'Get participants 
                    Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                    'Get collections
                    Dim listCollections = WBillHelper.GetCollections(batchCode)

                    'Get collection monitoring
                    Dim listCollectionMonitoring = WBillHelper.GetCollectionMonitoring(batchCode)

                    'Get the DMCM 
                    Dim listDMCM = WBillHelper.GetDebitCreditMemoMainFromJV(JVNo)

                    Dim listCollectionFinal = From x In listCollections Join y In listParticipants _
                                              On x.IDNumber Equals y.IDNumber _
                                              Select x, y.ParticipantID

                    'Generate collection summary for manual
                    Dim dt = BFactory.GenerateCollectionSummary(batchCode, JVNo, listCollections, _
                                                                 listCollectionMonitoring, listParticipants, _
                                                                 listDMCM, Signatories, TotalCash, TotalDrawdown, _
                                                                 New DSReport.CollectionSummaryDataTable, DocumentDate)
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadCollectionSummaryPerJV(dt, TotalCash, TotalDrawdown)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With


                    'For Journal Voucher
                Case 2
                    Dim itemJV = WBillHelper.GetJournalVoucher(batchCode).First()
                    Dim listAccountingCode = WBillHelper.GetAccountingCodes()

                    'Get the Signatories
                    Dim Signatories = WBillHelper.GetSignatories("CS").First()

                    Dim dtJV As New DSReport.JournalVoucherDataTable
                    Dim rowJV = dtJV.NewRow

                    With itemJV
                        rowJV("JV_NO") = .JVNumber
                        rowJV("JV_DATE") = .JVDate
                        rowJV("BATCHCODE") = .BatchCode.ToString
                        rowJV("STATUS") = .Status
                        rowJV("PREPAREDBY") = AMModule.FullName
                        rowJV("CHECKEDBY") = Signatories.Signatory_1
                        rowJV("APPROVEDBY") = Signatories.Signatory_2
                        rowJV("UPDATEDBY") = .UpdatedBy
                        rowJV("UPDATEDDATE") = .UpdatedDate
                        rowJV("GPREF_NO") = .GPRefNo
                        rowJV("REMARKS") = "Collection summary for batch " & batchCode
                    End With
                    dtJV.Rows.Add(rowJV)
                    dtJV.AcceptChanges()

                    Dim dtJVDetails As New DSReport.JournalVoucherDetailsDataTable
                    For Each item In itemJV.JVDetails
                        Dim rowJVDetails = dtJVDetails.NewRow()

                        With item
                            rowJVDetails("JV_NO") = .JVNumber
                            rowJVDetails("ACCOUNTCODE") = .AccountCode
                            rowJVDetails("CREDIT") = Math.Abs(.Credit)
                            rowJVDetails("DEBIT") = Math.Abs(.Debit)
                            rowJVDetails("UPDATEDBY") = .UpdatedBy
                            rowJVDetails("UPDATEDDATE") = .UpdatedDate
                        End With
                        dtJVDetails.Rows.Add(rowJVDetails)
                    Next
                    dtJVDetails.AcceptChanges()

                    Dim dtAcct As New DSReport.AccountingCodeDataTable
                    For Each item In listAccountingCode
                        Dim rowAcct = dtAcct.NewRow()

                        With item
                            rowAcct("ACCT_CODE") = .AccountCode
                            rowAcct("DESCRIPTION") = .Description
                        End With
                        dtAcct.Rows.Add(rowAcct)
                    Next
                    dtAcct.AcceptChanges()

                    Dim ds As New DataSet
                    With ds.Tables
                        .Add(dtJV)
                        .Add(dtJVDetails)
                        .Add(dtAcct)
                    End With

                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadJournalVoucher(ds)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With

                Case 3
                    'For Daily Collection Summary Report

                    'Get the Signatories
                    Dim Signatories = WBillHelper.GetSignatories("DCS").First()

                    'Get participants 
                    Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                    'Get collections
                    Dim listCollections = WBillHelper.GetCollectionsDaily(batchCode)

                    Dim dt As DataTable = Me.BFactory.GenerateDailyCollectionSummary(batchCode, JVNo, listCollections, _
                                                                                     listParticipants, Signatories, _
                                                                                     New DSReport.DailyCollectionSummaryDataTable)
                    If batchCode.Contains("DCC") Then
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDailyCollectionSummaryCancelled(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    Else
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDailyCollectionSummary(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    End If
                    

            End Select
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Updated By Lance 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.InqRepCollectionSummaryWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInPrinting.ToString, AMModule.UserName)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"

    Public Function GenerateDataTable(ByVal BatchCode As String, ByVal JVNo As Long, _
                                      ByRef TotalCash As Decimal, ByRef TotalDrawdown As Decimal) As DataTable

        'Get the Signatories
        Dim Signatories = WBillHelper.GetSignatories("CS").First()

        'Get participants 
        Dim listParticipants = WBillHelper.GetAMParticipantsAll()

        'Get collections
        Dim listCollections = WBillHelper.GetCollections(BatchCode)

        'Get collection monitoring
        Dim listCollectionMonitoring = WBillHelper.GetCollectionMonitoring(BatchCode)

        'Get the DMCM 
        Dim listDMCM = WBillHelper.GetDebitCreditMemoMainFromJV(JVNo)

        Dim listCollectionFinal = From x In listCollections Join y In listParticipants _
                                  On x.IDNumber Equals y.IDNumber _
                                  Select x, y.ParticipantID

        Dim dt As New DSReport.CollectionSummaryDataTable

        For Each item In listCollectionFinal

            If item.x.CollectionCategory = EnumCollectionCategory.Cash Then
                TotalCash += item.x.CollectedAmount
            Else
                TotalDrawdown += item.x.CollectedAmount
            End If

            For Each itemAlloc In item.x.ListOfCollectionAllocation
                Dim selectedItemAlloc = itemAlloc
                Dim row = dt.NewRow()
                With item
                    row("JVNo") = BFactory.GenerateBIRDocumentNumber(JVNo, BIRDocumentsType.JournalVoucher)
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()

                    If .x.CollectionCategory = EnumCollectionCategory.Cash Then
                        row("RefNo") = BFactory.GenerateBIRDocumentNumber(.x.ORNo, BIRDocumentsType.OfficialReceipt)
                    Else
                        row("RefNo") = BFactory.GenerateBIRDocumentNumber(.x.DMCMNumber, BIRDocumentsType.DMCM)
                    End If

                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = itemAlloc.ReferenceNumber 'itemAlloc.ReferenceType.ToString() & itemAlloc.ReferenceNumber.ToString("0000000")                    

                    If itemAlloc.DMCMNumber <> 0 Then
                        Try
                            row("DocDate") = (From x In listDMCM _
                                              Where x.DMCMNumber = selectedItemAlloc.DMCMNumber _
                                              Select x.UpdatedDate.ToString("MM/dd/yyyy")).First()
                        Catch ex As Exception
                            row("DocDate") = ""
                        End Try
                    End If

                    row("DueDate") = itemAlloc.DueDate.ToString("MM/dd/yyyy")
                    row("AmountApplied") = itemAlloc.Amount
                    row("CollecTypeCode") = itemAlloc.CollectionTypeCode.ToString()
                    row("Signatory1") = AMModule.FullName
                    row("Position1") = AMModule.Position
                    row("Signatory2") = Signatories.Signatory_1
                    row("Position2") = Signatories.Position_1
                    row("Signatory3") = Signatories.Signatory_2
                    row("Position3") = Signatories.Position_2

                    If itemAlloc.DMCMNumber <> 0 Then
                        row("CreatedDocumentNo") = BFactory.GenerateBIRDocumentNumber(.x.DMCMNumber, BIRDocumentsType.DMCM)
                    Else
                        row("CreatedDocumentNo") = ""
                    End If

                    dt.Rows.Add(row)
                End With
            Next


            Dim colNumber = item.x.CollectionNumber

            'For Unapplied Amount
            Dim totalUnapplied = (From x In listCollectionMonitoring _
                                  Where x.CollectionNo = colNumber _
                                  And x.TransType <> EnumCollectionMonitoringType.TransferToPRDrawdown _
                                  And x.TransType <> EnumCollectionMonitoringType.TransferToPRReplenishment _
                                  Select x.Amount).Sum()

            If totalUnapplied <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = "OR-" & .x.ORNo.ToString()
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "Unapplied"
                    row("AmountApplied") = totalUnapplied
                    row("Signatory1") = AMModule.FullName
                    row("Position1") = AMModule.Position
                    row("Signatory2") = Signatories.Signatory_1
                    row("Position2") = Signatories.Position_1
                    row("Signatory3") = Signatories.Signatory_2
                    row("Position3") = Signatories.Position_2

                    dt.Rows.Add(row)
                End With
            End If

            'For Unapplied Amount
            Dim totalReplenishment = (From x In listCollectionMonitoring _
                                      Where x.CollectionNo = colNumber _
                                      And x.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment _
                                      Select x.Amount).Sum()

            If totalReplenishment <> 0 Then
                Dim row = dt.NewRow()

                With item
                    row("JVNo") = JVNo
                    row("BatchCode") = BatchCode
                    row("ParticipantID") = .ParticipantID
                    row("IDNumber") = .x.IDNumber
                    row("Type") = .x.CollectionCategory.ToString()
                    row("RefNo") = "OR-" & .x.ORNo.ToString()
                    row("CollectionDate") = .x.CollectionDate.ToString("MM/dd/yyyy")
                    row("Amount") = .x.CollectedAmount
                    row("AllocationDate") = .x.AllocationDate.ToString("MM/dd/yyyy")
                    row("DocumentNo") = "Replenishment"
                    row("AmountApplied") = totalReplenishment
                    row("Signatory1") = AMModule.FullName
                    row("Position1") = AMModule.Position
                    row("Signatory2") = Signatories.Signatory_1
                    row("Position2") = Signatories.Position_1
                    row("Signatory3") = Signatories.Signatory_2
                    row("Position3") = Signatories.Position_2

                    dt.Rows.Add(row)
                End With
            End If
        Next

        Return dt
    End Function

#End Region

End Class