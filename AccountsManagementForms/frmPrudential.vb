'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollection
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     April 02, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the maintenance of Collection
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   April 02, 2012          Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects

Public Class frmPrudential
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Private _ListParticipants As List(Of AMParticipants)
    Private _ListPrudentials As List(Of Prudential)

    Private Sub frmPrudential_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm

        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName
        BFactory = BusinessFactory.GetInstance()

        'Get the prudentials
        Me._ListPrudentials = WBillHelper.GetParticipantsPrudential()

        Me._ListParticipants = WBillHelper.GetAMParticipants()

        'Load prudentials in grid
        Me.LoadPrudentials()

        'Load prudentials history in grid
        Me.LoadPrudentialHistory()

        With Me.ddlType.Items
            .Add("-- All --")
            .Add("GEN")
            .Add("LOAD")
            .Add("DCC")
        End With

        Me.ddlType.SelectedIndex = 0
    End Sub

    Private Sub DGridViewPrudential_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewPrudential.CellClick
        If Me.DGridViewPrudential.Rows.Count = 0 Then
            Exit Sub
        End If

        'Load history
        Me.LoadPrudentialHistory()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DGridViewPrudentialHistory.RowCount = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Exclamation, "No Data")
                Exit Sub
            End If

            Dim dt = New DSReport.PrudentialHistoryDataTable

            For index As Integer = 0 To Me.DGridViewPrudentialHistory.SelectedRows.Count - 1
                With Me.DGridViewPrudentialHistory.SelectedRows(index)
                    Dim transtype = CType(System.Enum.Parse(GetType(EnumPrudentialTransType), _
                                    CStr(.Cells("colTransType").Value)), EnumPrudentialTransType)

                    If transtype = EnumPrudentialTransType.Drawdown Or transtype = EnumPrudentialTransType.Replenishment Then
                        Dim row = dt.NewRow()
                        Dim totalPRudential = CDec(Me.DGridViewPrudential.CurrentRow.Cells("colPrudentialAmount").Value) + _
                                              CDec(Me.DGridViewPrudential.CurrentRow.Cells("colInterestAmount").Value)

                        row("DATE_NOW") = Now.ToString("dd MMMM yyyy")
                        row("ID") = .Cells("colID").Value
                        row("REP_NAME") = Me.DGridViewPrudential.CurrentRow.Cells("colRepName").Value
                        row("REP_POSITION") = Me.DGridViewPrudential.CurrentRow.Cells("colRepPosition").Value
                        row("ADDRESS") = Me.DGridViewPrudential.CurrentRow.Cells("colRepAddress").Value
                        row("REMARKS") = "Please be informed that Security Deposit of " & Me.DGridViewPrudential.CurrentRow.Cells("colParticipantID").Value.ToString() & _
                                         " in the amount of Php " & .Cells("colAmount").Value.ToString() & "  was drawn in payment " & _
                                         "for energy trading amount due on " & CDate(.Cells("colTransDate").Value).ToString("dd MMMM yyyy") & _
                                         ". As such, the balance of your security deposit as of date, including interest, is Php " & _
                                         FormatNumber(totalPRudential, 2) & "."

                        If transtype = EnumPrudentialTransType.Drawdown Then
                            row("SUBJECT") = "Drawdown of Security Deposit"
                        Else
                            row("SUBJECT") = "Replenishment of Security Deposit"
                        End If

                        dt.Rows.Add(row)
                    End If
                End With
            Next

            ProgressThread.Show("Please wait while preparing Prudential Report.")
            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadPrudential(dt)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub btnReplenishment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New frmPrudentialMgt

            frm.ListParticipants = WBillHelper.GetAMParticipants()
            frm.TransType = frmPrudentialMgt.EnumTransType.Replenishment
            If frm.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Dim listPrudential As New List(Of Prudential)
            Dim listPRHistory As New List(Of PrudentialHistory)
            Dim listOR As New List(Of OfficialReceiptMain)
            Dim itemJV As New JournalVoucher
            Dim itemGP As New WESMBillGPPosted

            'Generate Entries
            Me.GeneratePrudentialEntries(listPrudential, listPRHistory, listOR, itemJV, itemGP, _
                                         frm.DGridMain, CDate(FormatDateTime(frm.dtTransDate.Value, DateFormat.ShortDate)))

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to save this record/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            'Save
            'WBillHelper.SavePrudentialReplenishment(listPrudential, listPRHistory, listOR, itemJV, itemGP, New Date)

            MsgBox("Successfully saved!", MsgBoxStyle.Information, "Save")

            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential()

            'Load prudentials in grid
            Me.LoadPrudentials()

            'Load prudentials history in grid
            Me.LoadPrudentialHistory()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnInterest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New frmPrudentialMgt

            frm.ListParticipants = WBillHelper.GetAMParticipants()
            frm.TransType = frmPrudentialMgt.EnumTransType.Interest
            If frm.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If

            Dim listPrudential As New List(Of Prudential)
            Dim listPRHistory As New List(Of PrudentialHistory)
            Dim listDMCM As New List(Of DebitCreditMemo)
            Dim itemJV As New JournalVoucher
            Dim itemGP As New WESMBillGPPosted

            'Generate Entries
            Me.GenerateInterestEntries(listPrudential, listPRHistory, listDMCM, itemJV, itemGP, _
                                       frm.DGridMain, CDate(FormatDateTime(frm.dtTransDate.Value, DateFormat.ShortDate)))

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to save this record/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            'Save
            'WBillHelper.SavePrudentialInterest(listPrudential, listPRHistory, listDMCM, itemJV, itemGP)

            MsgBox("Successfully saved!", MsgBoxStyle.Information, "Save")

            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential()

            'Load prudentials in grid
            Me.LoadPrudentials()

            'Load prudentials history in grid
            Me.LoadPrudentialHistory()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub



    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.DGridViewPrudential.RowCount = 0 Then
            MsgBox("Nothing to transfer!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        'Get the prudential
        Dim listPR = WBillHelper.GetParticipantsPrudential()

        Dim cnt = (From x In listPR _
                   Where x.InterestAmount <> 0 _
                   Select x).Count()

        If cnt = 0 Then
            MsgBox("Nothing to transfer!", MsgBoxStyle.Exclamation, "No Interst Amount")
            Exit Sub
        End If

        Dim frm As New frmPrudentialMgt

        frm.ListParticipants = WBillHelper.GetAMParticipants()
        frm.ListPrudential = listPR
        frm.TransType = frmPrudentialMgt.EnumTransType.TranferInterestIntoPR
        If frm.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Try

            Dim listPrudential As New List(Of Prudential)
            Dim listPRHistory As New List(Of PrudentialHistory)
            Dim itemJV As New JournalVoucher
            Dim itemGP As New WESMBillGPPosted

            'Generate Entries
            Me.GenerateTransferInterestIntoPrudentialEntries(listPrudential, listPRHistory, itemJV, itemGP, _
                                                             frm.DGridMain, CDate(FormatDateTime(frm.dtTransDate.Value, DateFormat.ShortDate)))

            Dim ans As MsgBoxResult
            ans = MsgBox("Do you really want to transfer this record/s?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Transfer")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            'Save
            'WBillHelper.SavePrudentialTransferInterest(listPrudential, listPRHistory, itemJV, itemGP)

            MsgBox("Successfully transfered!", MsgBoxStyle.Information, "Transfered")

            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential()

            'Load prudentials in grid
            Me.LoadPrudentials()

            'Load prudentials history in grid
            Me.LoadPrudentialHistory()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        If Me.ddlType.SelectedIndex = 0 Then
            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential()
        ElseIf Me.ddlType.SelectedIndex = 1 Then
            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential(EnumGenLoad.G)
        ElseIf Me.ddlType.SelectedIndex = 2 Then
            'Get the prudentials
            Me._ListPrudentials = WBillHelper.GetParticipantsPrudential(EnumGenLoad.L)
        End If

        Me._ListParticipants = WBillHelper.GetAMParticipants()

        'Load prudentials in grid
        Me.LoadPrudentials()

        'Load prudentials history in grid
        Me.LoadPrudentialHistory()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.LoadRecord()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.LoadRecord()
        End If
    End Sub

    Private Sub DGridViewPrudentialHistory_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewPrudentialHistory.CellContentClick
        If e.ColumnIndex = 2 Then
            ProgressThread.Show("Please wait while processing.")
            Try
                If Me.DGridViewPrudentialHistory.RowCount = 0 Then
                    MsgBox("Please select first the prudential transaction!", MsgBoxStyle.Exclamation, "Select the data")
                    Exit Sub
                ElseIf CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colORNo").Value) = 0 And _
                       CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colDMCMNo").Value) = 0 And _
                       CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colFTFNo").Value) = 0 Then
                    MsgBox("The selected record has no official receipt/debit credit memo/fund transfer!", MsgBoxStyle.Exclamation, "Select the data")
                    Exit Sub
                End If

                'Get the OR Number
                Dim ORNo = CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colORNo").Value)

                'Get the DMCM No
                Dim DMCMNo = CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colDMCMNo").Value)

                'Get the FTF No
                Dim FTFNo = CLng(Me.DGridViewPrudentialHistory.CurrentRow.Cells("colFTFNo").Value)

                If ORNo <> 0 Then

                    Dim result As New DataTable
                    Dim itemOR = WBillHelper.GetOfficialReceipt(ORNo)

                    Dim itemORReportMain As New OfficialReceiptReportMain
                    With itemORReportMain
                        .ItemOfficialReceipt = WBillHelper.GetOfficialReceipt(ORNo)
                        .ListOfficialReceiptReportRawDetails = WBillHelper.GetOfficialReceiptRawDetails(ORNo)
                        .ItemParticipant = WBillHelper.GetAMParticipants(.ItemOfficialReceipt.IDNumber).First()
                        .DefaultInterestRate = 0 'WBillHelper.GetDailyInterestRate()(itemORReportMain.ItemOfficialReceipt.ORDate)
                        .BIRPermitNumber = AMModule.BIRPermitNumber
                        .TotalPaymentInWords = BFactory.NumberConvert(.TotalPayment)
                    End With

                    Dim ORPEMC_Header As Integer = EnumHeaderType.Yes

                    result = BFactory.GenerateOfficialReceiptReport(ORPEMC_Header, itemORReportMain, itemORReportMain.ItemParticipant, New DSReport.OfficialReceiptMainNewDataTable)

                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadOR(result)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With

                ElseIf DMCMNo <> 0 Then
                    Dim listDMCMMain As New List(Of DebitCreditMemo)
                    Dim itemDMCM As New DebitCreditMemo

                    itemDMCM = WBillHelper.GetDebitCreditMemoMain(DMCMNo).First()


                    Dim listDMCMNo As New List(Of Long)

                    'Get the Accounting Codes
                    Dim listAccountCodes = WBillHelper.GetAccountingCodes()

                    'Get the Participants
                    Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                    'Get the Signatory
                    Dim signatory = WBillHelper.GetSignatories("DMCM").First()

                    'Add the DMCM
                    listDMCMMain.Add(itemDMCM)

                    'Get the WESM Bill Sales and Purchase
                    Dim listWESMBillSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchasedWithDMCM(itemDMCM.DMCMNumber)

                    'Add the DMCM Number
                    listDMCMNo.Add(itemDMCM.DMCMNumber)

                    'Get the datasource for the report
                    Dim dt = BFactory.GenerateDMCMReport1(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCMMain, _
                                                         listAccountCodes, listParticipants, signatory, listWESMBillSalesAndPurchased)

                    'Get the datasource for the report
                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With


                Else
                    'Signatories
                    Dim itemSignatory = WBillHelper.GetSignatories("FTF").First()

                    Dim itemFTF = WBillHelper.GetFundTransferForm(FTFNo).First()

                    Dim dtMain As New DSReport.FTFMainDataTable
                    Dim dtParticipant As New DSReport.FTFParticipantDataTable
                    Dim dtDetails As New DSReport.FTFDetailsDataTable

                    Dim ds = BFactory.GenerateFundTransferFormReport(dtMain, dtParticipant, dtDetails, itemFTF, itemSignatory)

                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadFTF(ds)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With
                End If
            Catch ex As Exception
                ProgressThread.Close()
                MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#Region "Methods/Functions"

    Private Sub LoadPrudentials()
        Me.DGridViewPrudential.Rows.Clear()

        Dim items = From x In Me._ListPrudentials Join y In Me._ListParticipants _
                    On x.IDNumber Equals y.IDNumber _
                    Select x, y.ParticipantID, y.FullName, y.ParticipantAddress, _
                    y.Representative.GetFullName, y.Representative.Position _
                    Order By ParticipantID

        For Each item In items
            With item
                Me.DGridViewPrudential.Rows.Add(.x.IDNumber, .ParticipantID, .FullName, FormatNumber(.x.PrudentialAmount, 2), _
                                                FormatNumber(.x.InterestAmount, 2), .GetFullName, .Position, _
                                                .ParticipantAddress)
            End With
        Next

        Dim totalPrudential = (From x In items Select x.x.PrudentialAmount).Sum()
        Dim totalPrudentialInterest = (From x In items Select x.x.InterestAmount).Sum()

        Me.txtPrudential.Text = FormatNumber(totalPrudential, 2)
        Me.txtInterest.Text = FormatNumber(totalPrudentialInterest, 2)
    End Sub

    Private Sub LoadPrudentialHistory()
        Me.DGridViewPrudentialHistory.Rows.Clear()

        If Me.DGridViewPrudential.Rows.Count = 0 Then
            Exit Sub
        End If

        'Get the id number of the selected participant
        Dim idnumber = CStr(Me.DGridViewPrudential.CurrentRow.Cells("colIDNumber").Value)

        'Get the list of prudential history
        Dim listPrudentialHistory = WBillHelper.GetParticipantsPrudentialHistoryParticipant(idnumber)

        For index As Integer = 0 To listPrudentialHistory.Count - 1
            With listPrudentialHistory(index)
                Dim createdDocument As String = ""

                If .ORNo <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                ElseIf .DMCMNumber <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                ElseIf .FTFNumber <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(.FTFNumber, BIRDocumentsType.FTF)
                Else

                End If

                Me.DGridViewPrudentialHistory.Rows.Add(index + 1, .TransDate.ToString("dd MMM yyyy"), createdDocument, _
                                                       FormatNumber(.Amount, 2), .TransType.ToString(), .ORNo, .DMCMNumber, .FTFNumber)
            End With
        Next
    End Sub

    Private Function ComputeMarginCall(ByVal listWESMBillSummary As List(Of WESMBillSummary)) As DataTable
        Dim dt As New DataTable

        With dt.Columns
            .Add("ID_NUMBER", GetType(Long))
            .Add("PARTICIPANT_ID", GetType(String))
            .Add("NAME", GetType(String))
            .Add("ADDRESS", GetType(String))
            .Add("POSITION", GetType(String))
            .Add("MNE", GetType(Decimal))
            .Add("TRADING_LIMIT", GetType(Decimal))
            .Add("MINIMUM_REQUIREMENT", GetType(Decimal))
        End With
        dt.AcceptChanges()

        Dim listSummaries = From x In listWESMBillSummary Group By x.IDNumber.IDNumber Into sumEnd = Sum(x.EndingBalance) _
                             Select IDNumber, sumEnd

        Dim participants = From x In listSummaries Join y In Me._ListParticipants _
                           On x.IDNumber Equals y.IDNumber _
                           Select y Order By y.ParticipantID

        For Each item In participants
            Dim selectedParticipant = item
            Dim MNE As Decimal = 0

            Dim totalReceivable = (From x In listSummaries _
                                   Where x.IDNumber = selectedParticipant.IDNumber _
                                   Select x.sumEnd).Sum()
            If totalReceivable <> 0 Then
                MNE = Math.Abs(totalReceivable) / (12D / 360D * 63D)
            End If

            Dim PRAmount = (From x In Me._ListPrudentials _
                            Where x.IDNumber = selectedParticipant.IDNumber _
                            Select x.PrudentialAmount).Sum()

            Dim tradingLimit = PRAmount * 0.95D

            If MNE > tradingLimit Then
                Dim row = dt.NewRow()

                row("ID_NUMBER") = selectedParticipant.IDNumber
                row("PARTICIPANT_ID") = selectedParticipant.ParticipantID
                row("NAME") = selectedParticipant.Representative.GetFullName
                row("ADDRESS") = selectedParticipant.ParticipantAddress
                row("POSITION") = selectedParticipant.Representative.Position
                row("MNE") = MNE
                row("TRADING_LIMIT") = tradingLimit
                row("MINIMUM_REQUIREMENT") = MNE - tradingLimit
                dt.Rows.Add(row)
                dt.AcceptChanges()
            End If
        Next

        Return dt
    End Function

    Private Sub GeneratePrudentialEntries(ByRef listPrudential As List(Of Prudential), ByRef listPRHistory As List(Of PrudentialHistory), _
                                          ByRef listOR As List(Of OfficialReceiptMain), ByRef itemJV As JournalVoucher, _
                                          ByVal itemGP As WESMBillGPPosted, ByVal DGrid As DataGridView, ByVal transDate As Date)

        'Get the Signatories
        Dim SignatoriesJV = WBillHelper.GetSignatories("JV").First()

        Dim totalAmount As Decimal = 0

        For index As Integer = 0 To DGrid.RowCount - 1
            Dim IDNumber As String = CStr(DGrid.Rows(index).Cells("colIDNumber").Value)
            Dim Amount As Decimal = CDec(DGrid.Rows(index).Cells("colAmount").Value)

            If Amount <> 0 Then
                totalAmount += Amount

                'For replenishment
                listPrudential.Add(New Prudential(IDNumber, Amount, 0))

                'For PR history
                listPRHistory.Add(New PrudentialHistory(New AMParticipants(IDNumber), Amount, EnumPrudentialTransType.Replenishment, transDate))

                'For OR
                Dim listORDetails As New List(Of OfficialReceiptDetails)
                listORDetails.Add(New OfficialReceiptDetails(0, AMModule.CashinBankPrudentialCode, _
                                                             EnumCollectionMonitoringType.TransferToPRReplenishment.ToString(), _
                                                             Amount, 0))

                listORDetails.Add(New OfficialReceiptDetails(0, AMModule.PRWESMCode, _
                                                             "Prudential requirement - WESM", 0, Amount))

                listOR.Add(New OfficialReceiptMain(0, transDate, IDNumber, Amount, _
                                                   EnumStatus.Active, listORDetails, "", "Prudential Replenishment", EnumORTransactionType.Replenishment))
            End If
        Next

        'For JV
        Dim jvDetails As New List(Of JournalVoucherDetails)
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashinBankPrudentialCode, totalAmount, 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, 0, totalAmount))

        'For Journal Voucher
        With itemJV
            .Status = 1
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.PRR.ToString()
            .JVDetails = jvDetails
        End With

        'For WESM GP Posted
        With itemGP
            .PostType = EnumPostedType.PRR.ToString()
            .DocumentAmount = totalAmount
        End With
    End Sub

    Private Sub GenerateInterestEntries(ByRef listPrudential As List(Of Prudential), ByRef listPRHistory As List(Of PrudentialHistory), _
                                        ByRef listDMCM As List(Of DebitCreditMemo), ByRef itemJV As JournalVoucher, _
                                        ByVal itemGP As WESMBillGPPosted, ByVal DGrid As DataGridView, ByVal transDate As Date)

        'Get the Signatories
        Dim SignatoriesJV = WBillHelper.GetSignatories("JV").First()

        'Get the Signatories
        Dim SignatoriesDMCM = WBillHelper.GetSignatories("DMCM").First()

        Dim totalInterest As Decimal = 0
        Dim DMCMNo As Long = 0

        For index As Integer = 0 To DGrid.RowCount - 1
            Dim IDNumber As String = CStr(DGrid.Rows(index).Cells("colIDNumber").Value)
            Dim Amount As Decimal = CDec(DGrid.Rows(index).Cells("colAmount").Value)

            If Amount <> 0 Then
                'For Interest
                listPrudential.Add(New Prudential(IDNumber, 0, Amount))

                'For PR history
                listPRHistory.Add(New PrudentialHistory(New AMParticipants(IDNumber), Amount, EnumPrudentialTransType.InterestAmount, transDate))

                totalInterest += Amount

                'For DMCM 
                DMCMNo += 1

                Dim listDMCMDetails As New List(Of DebitCreditMemoDetails)
                listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.CashinBankPrudentialCode, Amount, 0, _
                                                               "", EnumSummaryType.INV, New AMParticipants(IDNumber)))
                listDMCMDetails.Add(New DebitCreditMemoDetails(DMCMNo, AMModule.InterestPayablePRCode, 0, Amount, _
                                                               "", EnumSummaryType.INV, New AMParticipants(IDNumber)))

                Dim itemDMCM As New DebitCreditMemo
                With itemDMCM
                    .IDNumber = IDNumber
                    .ChargeType = EnumChargeType.E
                    .Particulars = "Prudential Interest"
                    .TotalAmountDue = Amount
                    .TransType = EnumDMCMTransactionType.PrudentialInterest
                    .VATExempt = Amount
                    .TotalAmountDue = Amount
                    .DMCMDetails = listDMCMDetails
                    .CheckedBy = SignatoriesDMCM.Signatory_1
                    .ApprovedBy = SignatoriesDMCM.Signatory_2
                End With

                listDMCM.Add(itemDMCM)
                listDMCM.TrimExcess()
            End If
        Next

        Dim jvDetails As New List(Of JournalVoucherDetails)

        jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashinBankPrudentialCode, totalInterest, 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, 0, totalInterest))
        jvDetails.TrimExcess()

        'For Journal Voucher
        With itemJV
            .Status = 1
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.PRI.ToString()
            .JVDetails = jvDetails
        End With

        'For WESM GP Posted
        With itemGP
            .PostType = EnumPostedType.PRI.ToString()
            .DocumentAmount = totalInterest
        End With
    End Sub

    Private Sub GenerateTransferInterestIntoPrudentialEntries(ByRef listPrudential As List(Of Prudential), ByRef listPRHistory As List(Of PrudentialHistory), _
                                                              ByRef itemJV As JournalVoucher, ByVal itemGP As WESMBillGPPosted, ByVal DGrid As DataGridView, _
                                                              ByVal transDate As Date)

        'Get the Signatories
        Dim SignatoriesJV = WBillHelper.GetSignatories("JV").First()
        Dim totalInterest As Decimal = 0

        For index As Integer = 0 To DGrid.RowCount - 1
            If CBool(DGrid.Rows(index).Cells("colCheck").Value) = True Then
                Dim IDNumber As String = CStr(DGrid.Rows(index).Cells("colIDNumber").Value)
                Dim ParticipantID As String = CStr(DGrid.Rows(index).Cells("colParticipantID").Value)
                Dim Amount As Decimal = CDec(DGrid.Rows(index).Cells("colAmount").Value)

                If Amount <> 0 Then
                    'For Interest
                    listPrudential.Add(New Prudential(IDNumber, 0, Amount))

                    'For PR history
                    listPRHistory.Add(New PrudentialHistory(New AMParticipants(IDNumber, ParticipantID), Amount, EnumPrudentialTransType.TransferInterestAmount, transDate))

                    totalInterest += Amount
                End If
            End If
        Next

        Dim jvDetails As New List(Of JournalVoucherDetails)

        jvDetails.Add(New JournalVoucherDetails(0, AMModule.InterestPayablePRCode, totalInterest, 0))
        jvDetails.Add(New JournalVoucherDetails(0, AMModule.PRWESMCode, 0, totalInterest))
        jvDetails.TrimExcess()

        'For Journal Voucher
        With itemJV
            .Status = 1
            .CheckedBy = SignatoriesJV.Signatory_1
            .ApprovedBy = SignatoriesJV.Signatory_2
            .PostedType = EnumPostedType.PRTI.ToString()
            .JVDetails = jvDetails
        End With

        'For WESM GP Posted
        With itemGP
            .PostType = EnumPostedType.PRTI.ToString()
            .DocumentAmount = totalInterest
        End With
    End Sub

    Private Sub LoadRecord()
        If Me.DGridViewPrudential.RowCount = 0 Then
            MsgBox("No current records!", MsgBoxStyle.Exclamation, "No data")
            Exit Sub
        End If

        If Me.txtSearch.Text.Trim.Length = 0 Then
            MsgBox("Specify first the participant id!", MsgBoxStyle.Critical, "Invalid")
            Me.txtSearch.Select()
            Exit Sub
        End If

        For Index As Integer = 0 To Me.DGridViewPrudential.RowCount - 1
            If CStr(Me.DGridViewPrudential.Rows(Index).Cells("colParticipantID").Value).ToUpper() = Me.txtSearch.Text.Trim.ToUpper Then
                Me.DGridViewPrudential.Rows(Index).Selected = True
                Me.DGridViewPrudential.CurrentCell = Me.DGridViewPrudential.Rows(Index).Cells(0)
                Me.txtSearch.Text = ""
                Exit Sub
            End If
        Next

        MsgBox(Me.txtSearch.Text & " does not exist in grid!", MsgBoxStyle.Information, "No data")
    End Sub

    Private Function LoadDMCM(ByVal itemDMCM As DebitCreditMemo) As DataTable
        Dim dt As New DSReport.DebitCreditMemoDataTable
        Dim itemDetails = itemDMCM.DMCMDetails

        'Get the participant
        Dim itemParticipant = WBillHelper.GetAMParticipants(itemDMCM.IDNumber)

        'Get the accounting codes
        Dim listAcctgCodes = WBillHelper.GetAccountingCodes()

        Dim items = (From x In itemDetails Join y In listAcctgCodes _
                     On x.AccountCode Equals y.AccountCode _
                     Select x, y).ToList()

        For Each item In items
            With item
                Dim row As DataRow = dt.NewRow()
                Dim dmcm_no As String = "000000000000" & itemDMCM.DMCMNumber.ToString()
                Dim jv_no As String = "000000000000" & itemDMCM.JVNumber.ToString()
                row("ID_NUMBER") = itemDMCM.IDNumber.ToString()
                row("ADDRESS") = itemParticipant.First.ParticipantAddress
                row("DMCM_NO") = dmcm_no.Substring(dmcm_no.Length - 12, 12)
                row("JV_NO") = "JV-" & jv_no.Substring(jv_no.Length - 12, 12)
                row("PARTICULARS") = itemDMCM.Particulars
                row("ACCOUNT_CODE") = item.x.AccountCode

                If item.x.InvDMCMNo <> "" Then
                    row("DESCRIPTION") = "(" & item.x.SummaryType.ToString() & "-" & item.x.InvDMCMNo.ToString() & ") " & item.y.Description
                Else
                    row("DESCRIPTION") = item.y.Description
                End If

                row("PREPARED_BY") = itemDMCM.PreparedBy
                row("CHECKED_BY") = itemDMCM.CheckedBy
                row("APPROVED_BY") = itemDMCM.ApprovedBy
                row("PARTICIPANT_NAME") = itemParticipant.First.FullName
                row("DR_AMOUNT") = item.x.Debit
                row("CR_AMOUNT") = item.x.Credit
                row("PARTICIPANT_ID") = itemParticipant.First.ParticipantID
                row("EWT") = itemDMCM.EWT
                row("EWV") = itemDMCM.EWV
                row("VATABLE") = itemDMCM.Vatable
                row("VAT") = itemDMCM.VAT
                row("VAT_EXEMPT_SALE") = itemDMCM.VATExempt
                row("VAT_ZERO") = itemDMCM.VatZeroRated
                row("TOTAL_AMOUNT_DUE") = itemDMCM.TotalAmountDue
                dt.Rows.Add(row)
            End With
        Next
        dt.AcceptChanges()

        Return dt
    End Function

#End Region


End Class