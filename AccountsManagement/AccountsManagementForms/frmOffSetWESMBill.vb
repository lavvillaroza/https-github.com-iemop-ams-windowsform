'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Class Name:             frmImportWESMBillDatabase
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     September 28, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI for processing the parent to parent/child to child offsetting. It also include the generation of Debit/Credit Memo.
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                Description
'   September 28, 2011      Vladimir E. Espiritu            GUI initialization
'   September 29, 2011      Vladimir E. Espiritu            Parent to Parent Offsetting
'   September 29, 2011      Vladimir E. Espiritu            Generation of Debit/Credit Memo
'   November 24, 2011       Vladimir E. Espiritu            Parent to Child Offsetting
'   December 12, 2011       Vladimir E. Espiritu            Show the WESM Bills to be offset
'


Option Explicit On
Option Strict On

Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmOffSetWESMBill
    Private WBillHelper As WESMBillHelper
    Private _finalWESMBills As List(Of WESMBill)
    Private _finalP2PC2CMapping As List(Of ParticipantParentChildMapping)
    Private _listGPPosted As List(Of WESMBillGPPosted)
    Private _listParticipants As List(Of AMParticipants)
    Private _billingPeriods As List(Of CalendarBillingPeriod)
    Private _SignatoriesDMCM As DocSignatories
    Private _SignatoriesJV As DocSignatories
    Private _chargeEnergy As Boolean
    Private _chargeMF As Boolean
    Private _billingPeriod As String
    Private _dueDate As String
    Private _Signatories As List(Of DocSignatories)
    Private _ListCalendarBP As List(Of CalendarBillingPeriod)
    Private _ParentChildExemption As List(Of ParentChildExemption)

    Const DocTypeDMCM As String = "DMCM"
    Const DocTypeJV As String = "JV"

    Private Enum EnumFilterType
        Energy
        MarketFees
        BillingPeriod
        DueDate
    End Enum

    Private Enum EnumStatus
        Load
        Search
        Save
    End Enum

    Private Sub frmOffSetWESMBill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MdiParent = MainForm
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName

            Me._Signatories = WBillHelper.GetSignatories()

            'Load the Signatories for DMCM
            Me._SignatoriesDMCM = WBillHelper.GetSignatories(DocTypeDMCM).First()
            Me._SignatoriesJV = WBillHelper.GetSignatories(DocTypeJV).First()
            Me._ListCalendarBP = WBillHelper.GetCalendarBP()

            'Load the primary inputs
            Me.LoadInputs()

            AddHandler Me.rbEnergy.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChanged
            AddHandler Me.rbMF.CheckedChanged, AddressOf Me.rbChargeTypeCheckedChanged
            AddHandler Me.ddlBillingPeriod.SelectedIndexChanged, AddressOf Me.ddlBillingPeriod_SelectedIndexChanged

            Me.rbEnergy.Select()

            Me.btnGenerate.Enabled = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub rbChargeTypeCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.rbEnergy.Checked Then
                Me.LoadFilters(EnumFilterType.Energy)
            Else
                Me.LoadFilters(EnumFilterType.MarketFees)
            End If
            Me.IsChanged()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ddlBillingPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.LoadFilters(EnumFilterType.BillingPeriod)
            Me.IsChanged()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")        
        End Try
    End Sub

    Private Sub ddlDueDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlDueDate.SelectedIndexChanged        
        Try
            Me.LoadFilters(EnumFilterType.DueDate)
            Me.IsChanged()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")        
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Dim billingPeriod As Integer = 0
        Dim dueDate As Date = Nothing
        Dim chargeTypeValue As EnumChargeType = Nothing
        Dim stlRun As String = ""
        Try
            If Me.ddlBillingPeriod.SelectedIndex = -1 Then
                MsgBox("Please select first the billing period!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            ElseIf Me.ddlDueDate.SelectedIndex = -1 Then
                MsgBox("Please select first the due date!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub

            ElseIf Me.ddlSettlementRun.SelectedIndex = -1 Then
                MsgBox("Please select first the settlement run!", MsgBoxStyle.Critical, "Specify the inputs")
                Exit Sub
            End If

            'Get the selected billing period and due date
            billingPeriod = CInt(Me.ddlBillingPeriod.Text)
            dueDate = CDate(Me.ddlDueDate.Text)
            stlRun = CStr(Me.ddlSettlementRun.Text)

            'Check if all data for the specified billing period and due date are already posted
            If Me.rbEnergy.Checked Then
                chargeTypeValue = EnumChargeType.E

                If Not WBillHelper.IsWESMBillsAllPosted(billingPeriod, dueDate, EnumChargeType.E) Then
                    MsgBox("Please post first Final Statement for Energy with billing period of " & billingPeriod.ToString() & _
                           " and due date of " & dueDate.ToString("MM/dd/yyyy"), MsgBoxStyle.Critical, "Denied")
                    Exit Sub
                End If
            Else
                chargeTypeValue = EnumChargeType.MF

                If Not WBillHelper.IsWESMBillsAllPosted(billingPeriod, dueDate, EnumChargeType.MF) Then
                    MsgBox("Please post first Final Statement for Market Fees with billing period of " & billingPeriod.ToString() & _
                           " and due date of " & dueDate.ToString("MM/dd/yyyy"), MsgBoxStyle.Critical, "Denied")
                    Exit Sub
                End If
            End If

            'Get all WESM Bills that are not offset
            Dim listWESMBills = WBillHelper.GetWESMBillForOffsetting(billingPeriod, dueDate, stlRun)

            'Get the Parent to child mapping
            Me._finalP2PC2CMapping = WBillHelper.GetParentChildMappingPerBillingPeriod(billingPeriod)

            'Get the bill Participants
            Me._listParticipants = WBillHelper.GetAMParticipantsAll()


            'Get 1st Offsetting Exemption on VAT
            Me._ParentChildExemption = WBillHelper.GetParentChildOffsettingExemp()

            'Get the list of charge type
            If Me.rbEnergy.Checked Then
                Me._finalWESMBills = (From x In listWESMBills _
                                      Where x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.EV _
                                      Select x).ToList()
            Else
                Me._finalWESMBills = (From x In listWESMBills _
                                      Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV _
                                      Select x).ToList()
            End If

            'Check for in-active participants
            Dim listInActivePartipants = (From x In Me._finalWESMBills Join y In Me._listParticipants _
                                          On CStr(x.IDNumber) Equals y.IDNumber _
                                          Where y.Status = AccountsManagementObjects.EnumStatus.InActive _
                                          Select y.ParticipantID Distinct Order By ParticipantID).ToList()

            If listInActivePartipants.Count > 0 Then
                MsgBox("There are participant/s which have WESM Bills that are already in-active. " & vbCrLf & _
                       "Please edit first the participants to be shown in the next window!", MsgBoxStyle.Critical, "In-active Participants")
                With frmViewDetails
                    .ShowInActiveParticipants(listInActivePartipants)
                    .Show()
                End With
                Exit Sub
            End If

            Dim WESMBillsForOffsetting = Me.GetDataForOffsetting(Me._finalWESMBills, Me._finalP2PC2CMapping, Me._ParentChildExemption)

            'Convert the WESM Bills into datatable
            Dim dt = Me.ConvertToDatatable(WESMBillsForOffsetting, Me._listParticipants)

            'Clear first the DataGridView
            Me.DGridView.Rows.Clear()

            For index As Integer = 0 To dt.Rows.Count - 1
                Dim row = dt.Rows(index)
                Me.DGridView.Rows.Add(row("ParticipantID"), row("ForAccountOf"), row("InvoiceNumber"), row("InvoiceDate"), _
                                      row("ChargeType"), row("Amount"), row("SettlementRun"))
            Next

            Me.btnGenerate.Enabled = False

            If Me.DGridView.RowCount = 0 Then
                MsgBox("No WESM Bills to be offset. Please click Offset button to generate the oustanding balance of the participants. ", MsgBoxStyle.Information, "Offset")
                Me.btnLoad.Select()
            End If

            Me.GetControlStatus()
            Me.IsChanged()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click        
        Dim billingPeriod As Integer = 0
        Dim dueDate As Date = Nothing
        Dim stlrun As String = ""
        Dim chargeTypeValue As EnumChargeType = Nothing
        Dim OffsetFromDate As Date
        Dim OffsetToDate As Date

        Try
            'Validation
            If Me.btnLoad.Text.Contains("*") Then
                MsgBox("Filter values were changed, please reload the data!", MsgBoxStyle.Critical, "Search")
                Exit Sub
            End If

            'Added by Vloody 05/20/2018
            'To select the new duedate

            Dim frm As New frmCollectionSearch
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 343
                valSize.Height = 130

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.SelectNewDueDate
                .dtAllocationDate.Value = CDate(Me.ddlDueDate.Text)

                If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
            End With

            Dim newDueDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))

            'Get the selected billing period and due date
            billingPeriod = CInt(Me.ddlBillingPeriod.Text)
            dueDate = CDate(Me.ddlDueDate.Text)
            stlrun = CStr(Me.ddlSettlementRun.Text)

            'Due Date must be in range based on the config dates
            OffsetFromDate = dueDate.AddDays(CInt(WESMBillOffsetFromDate))
            OffsetToDate = dueDate.AddDays(CInt(WESMBillOffsetToDate))

            If newDueDate < OffsetFromDate Or newDueDate > OffsetToDate Then
                MsgBox("Specified New DueDate must be in " & FormatDateTime(OffsetFromDate, DateFormat.ShortDate) & " to " & _
                       FormatDateTime(OffsetToDate, DateFormat.ShortDate) & " range!", MsgBoxStyle.Critical, "Invalid Due Date")
                Exit Sub
            End If

            ProgressThread.Show("Please wait while loading.")

            'Get WESMBill Sales and Purchases
            Dim listWESMBillSalesAndPurchases = WBillHelper.GetWESMBillSalesAndPurchasedForEWT(dueDate, stlrun, billingPeriod)

            'Get all WESM Bills that are not offset
            Dim listWESMBills = WBillHelper.GetWESMBillForOffsetting(billingPeriod, dueDate, stlrun)

            'Get the Parent to child mapping
            Me._finalP2PC2CMapping = WBillHelper.GetParentChildMappingPerBillingPeriod(billingPeriod)

            'Get the bill Participants
            Me._listParticipants = WBillHelper.GetAMParticipantsAll()

            'Get 1st Offsetting Exemption on VAT
            Me._ParentChildExemption = WBillHelper.GetParentChildOffsettingExemp()

            'Get the list of charge type
            Dim listWTDSumary As New List(Of WESMTransDetailsSummary)
            Dim listWTACSummary As New List(Of WESMBillAllocCoverSummary)
            If Me.rbEnergy.Checked Then
                chargeTypeValue = EnumChargeType.E
                Me._finalWESMBills = (From x In listWESMBills
                                      Where x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.EV
                                      Select x).ToList()

                'Get all WESM Transaction Allocation Summary Including details
                listWTACSummary = WBillHelper.GetListWESMTransCoverSummary(billingPeriod, stlrun, dueDate)
                For Each item In listWTACSummary.Where(Function(x) x.NetPurchase <> 0).ToList
                    For Each dtlItem In item.ListWBAllocDisDetails
                        Dim getWTACSummaryItemAP = listWTACSummary.Where(Function(x) x.BillingID = dtlItem.BillingID And x.NetSellerBuyerTag.ToUpper Like "*SELLER").FirstOrDefault
                        Using wtdSummary As New WESMTransDetailsSummary
                            With wtdSummary
                                .BuyerTransNo = item.TransactionNo
                                .BuyerBillingID = item.BillingID
                                .SellerTransNo = getWTACSummaryItemAP.TransactionNo
                                .SellerBillingID = getWTACSummaryItemAP.BillingID
                                .DueDate = item.DueDate
                                .NewDueDate = item.DueDate
                                .OrigBalanceInEnergy = item.ListWBAllocDisDetails.
                                                         Where(Function(x) x.BillingID = getWTACSummaryItemAP.BillingID).
                                                         Select(Function(y) y.NetPurchase).FirstOrDefault
                                .OrigBalanceInVAT = item.ListWBAllocDisDetails.
                                                         Where(Function(x) x.BillingID = getWTACSummaryItemAP.BillingID).
                                                         Select(Function(y) y.VatOnPurchases).FirstOrDefault
                                .OrigBalanceInEWT = item.ListWBAllocDisDetails.
                                                         Where(Function(x) x.BillingID = getWTACSummaryItemAP.BillingID).
                                                         Select(Function(y) y.EWT).FirstOrDefault * -1
                                .OutstandingBalanceInEnergy = .OrigBalanceInEnergy
                                .OutstandingBalanceInVAT = .OrigBalanceInVAT
                                .OutstandingBalanceInEWT = .OrigBalanceInEWT
                            End With
                            listWTDSumary.Add(wtdSummary)
                        End Using
                    Next
                Next
                listWTDSumary.TrimExcess()

            Else
                chargeTypeValue = EnumChargeType.MF
                Me._finalWESMBills = (From x In listWESMBills _
                                      Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV _
                                      Select x).ToList()
            End If

            'Check for in-active participants
            Dim listInActivePartipants = (From x In Me._finalWESMBills Join y In Me._listParticipants _
                                          On CStr(x.IDNumber) Equals y.IDNumber _
                                          Where y.Status = AccountsManagementObjects.EnumStatus.InActive _
                                          Select y.ParticipantID Distinct Order By ParticipantID).ToList()

            If listInActivePartipants.Count > 0 Then
                ProgressThread.Close()
                MsgBox("There are participant/s which have Final Statements which are already in-active. " & vbCrLf &
                       "Please edit first the participants to be shown in the next window!", MsgBoxStyle.Critical, "In-active Participants")

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPWESMBillOffsetingWindow.ToString, "There are participants that are inactive in the selected billing period " & billingPeriod.ToString() & " and due date " & dueDate.ToString() & ".", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)

                With frmViewDetails
                    .ShowInActiveParticipants(listInActivePartipants)
                    .Show()
                End With
                Exit Sub
            End If

            Dim ans As MsgBoxResult
            If Me.DGridView.RowCount <> 0 Then
                ProgressThread.Close()
                ans = MsgBox("Do you really want to generate Final Statements Offsetting?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Offset")
            Else
                ProgressThread.Close()
                ans = MsgBox("Do you really want to generate the outstanding balance of the participants?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Generate")
            End If

            If ans <> MsgBoxResult.Yes Then
                Exit Sub
            End If

            'Open the progress bar
            ProgressThread.Show("Please wait while processing.")

            Dim listFinalOffset As New List(Of OffsetP2PC2CDetails)
            Dim listFinalWESMBillSummary As New List(Of WESMBillSummary)
            Dim listFinalJournalVoucher As New List(Of JournalVoucher)
            Dim listFinalDMCM As New List(Of DebitCreditMemo)
            Dim listFinalGPPosted As New List(Of WESMBillGPPosted)
            Dim itemOffsettingResult As New FuncOffsettingResult

            'Get the list of charge type excluded 
            Dim listCharge = (From x In _finalWESMBills _
                              Select x.ChargeType Distinct).ToList()

            Dim selectedBP = (From x In Me._ListCalendarBP _
                              Where x.BillingPeriod = billingPeriod _
                              Select x).First()

            Dim listParticipants = WBillHelper.GetAMParticipants()

            'Loop each charge
            For Each item In listCharge
                Dim itemCharge = item
                Dim itemOffsetting As New FuncOffsetting

                'Get the WESM Bills of the selected charge
                Dim tempListWESMBills = (From x In Me._finalWESMBills _
                                         Where x.ChargeType = itemCharge _
                                         Select x).ToList()
                Dim getItemsListInSTLRun As String = ""
                If stlrun = "-ALL-" Then
                    Dim getSTLRun = (From x In Me.ddlSettlementRun.Items.OfType(Of String)() Select x Where Not x = "-ALL-" Order By x).ToList
                    Dim counter As Integer = 0
                    For Each oitem In getSTLRun
                        counter += 1
                        If counter = 1 Then
                            getItemsListInSTLRun = oitem
                        Else
                            getItemsListInSTLRun &= ";" & oitem
                        End If
                    Next
                Else
                    getItemsListInSTLRun = stlrun
                End If

                'Initialize the FunctionOffsetting class
                With itemOffsetting
                    .WESMBillSummaryNo = itemOffsettingResult.WESMBillSummaryNo
                    .DMCMNo = itemOffsettingResult.DMCMNo
                    .JVNo = itemOffsettingResult.JVNo
                    .OffsetNo = itemOffsettingResult.OffsetNo
                    .StlRun = getItemsListInSTLRun
                    .ChargeType = itemCharge
                    .DueDate = dueDate
                    .CalendarBP = selectedBP
                    .ListOfAllParticipants = listParticipants
                    .ListOfWESMBill = tempListWESMBills
                    .ListOfWESMBillSalesAndPurchases = listWESMBillSalesAndPurchases
                    .ListOfP2CMapping = Me._finalP2PC2CMapping
                    .ListDocSignatories = Me._Signatories
                    .ListOfWESMBillAllocCoverySummary = listWTACSummary
                End With

                'Generate the Offsetting
                itemOffsettingResult = itemOffsetting.GenerateOffsetting(_ParentChildExemption, chargeTypeValue)
                With itemOffsettingResult
                    listFinalOffset.AddRange(.ListOfOffsetting)
                    listFinalWESMBillSummary.AddRange(.ListOfWESMBillSummary)
                    listFinalJournalVoucher.AddRange(.ListOfJournalVoucher)
                    listFinalDMCM.AddRange(.ListOfDebitCreditMemo)
                    listFinalGPPosted.AddRange(.ListOfGPPosted)
                End With
            Next
            listFinalOffset.TrimExcess()
            listFinalWESMBillSummary.TrimExcess()
            listFinalJournalVoucher.TrimExcess()
            listFinalDMCM.TrimExcess()
            listFinalGPPosted.TrimExcess()

            For Each item In listCharge
                If item = EnumChargeType.E Or item = EnumChargeType.EV Then
                    'Save the generated offsetting
                    WBillHelper.SaveP2PC2COffseting(billingPeriod, listFinalOffset, listFinalWESMBillSummary, listWTDSumary, listFinalJournalVoucher,
                                                    listFinalDMCM, listFinalGPPosted, itemOffsettingResult.DMCMNo, itemOffsettingResult.JVNo,
                                                    itemOffsettingResult.OffsetNo, itemOffsettingResult.WESMBillSummaryNo, dueDate, newDueDate, stlrun, EnumChargeType.E)
                    Exit For
                ElseIf item = EnumChargeType.MF Or item = EnumChargeType.MFV Then
                    'Save the generated offsetting
                    WBillHelper.SaveP2PC2COffseting(billingPeriod, listFinalOffset, listFinalWESMBillSummary, listWTDSumary, listFinalJournalVoucher,
                                                    listFinalDMCM, listFinalGPPosted, itemOffsettingResult.DMCMNo, itemOffsettingResult.JVNo,
                                                    itemOffsettingResult.OffsetNo, itemOffsettingResult.WESMBillSummaryNo, dueDate, newDueDate, stlrun, EnumChargeType.MF)
                    Exit For
                End If
            Next

            If Me.DGridView.RowCount <> 0 Then
                ProgressThread.Close()
                MsgBox("Successfully Offset!", MsgBoxStyle.Information, "Done")
            Else
                ProgressThread.Close()
                MsgBox("Successfully generated!", MsgBoxStyle.Information, "Done")
            End If

            'Updated By Lance 08/17/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.SAPWESMBillOffsetingWindow.ToString, chargeTypeValue.ToString() & "-" & billingPeriod.ToString() & "-" & dueDate.ToString(), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

            Me.LoadInputs()
            Me.rbEnergy.Checked = False
            Me.rbMF.Checked = False
            Me.ddlBillingPeriod.DataSource = Nothing
            Me.ddlDueDate.DataSource = Nothing
            Me.ddlSettlementRun.DataSource = Nothing
            Me.rbEnergy.Select()
            Me.DGridView.Rows.Clear()
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.LoadInputs()
            Me.rbEnergy.Checked = False
            Me.rbMF.Checked = False
            Me.DGridView.Rows.Clear()
            Me.ddlBillingPeriod.DataSource = Nothing
            Me.ddlDueDate.DataSource = Nothing
            Me.rbEnergy.Select()
            Me.btnLoad.Text = "  Search *"
            Me.btnGenerate.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

#Region "Functions/Method"

    Private Function ConvertToDatatable(ByVal listWESMBills As List(Of WESMBill), ByVal listAMParticipants As List(Of AMParticipants)) As DataTable
        Dim result As New DataTable

        Try
            Dim dicChargeType As New Dictionary(Of EnumChargeType, String)
            With dicChargeType
                .Add(EnumChargeType.E, "Energy")
                .Add(EnumChargeType.EV, "VAT on Energy")
                .Add(EnumChargeType.MF, "Market Fees")
                .Add(EnumChargeType.MFV, "VAT on Market Fees")
            End With


            'Join WESM Bills and Billing participants
            Dim items = (From x In listWESMBills Join y In listAMParticipants On x.IDNumber Equals CStr(y.IDNumber) _
                         Select y.ParticipantID, x.ForTheAccountOf, x.InvoiceNumber, x.InvoiceDate, x.ChargeType, x.Amount, x.SettlementRun _
                         Order By ParticipantID, ChargeType, ForTheAccountOf, SettlementRun).ToList()

            With result.Columns
                .Add("ParticipantID", GetType(String))
                .Add("ForAccountOf", GetType(String))
                .Add("InvoiceNumber", GetType(String))
                .Add("InvoiceDate", GetType(String))
                .Add("ChargeType", GetType(String))
                .Add("Amount", GetType(Decimal))
                .Add("SettlementRun", GetType(String))
            End With
            result.AcceptChanges()

            For Each item In items
                Dim row As DataRow = result.NewRow()
                With item
                    row("ParticipantID") = .ParticipantID
                    row("ForAccountOf") = .ForTheAccountOf
                    row("InvoiceNumber") = .InvoiceNumber
                    row("InvoiceDate") = .InvoiceDate.ToString("MM/dd/yyyy")
                    row("ChargeType") = dicChargeType(.ChargeType)
                    row("Amount") = .Amount
                    row("SettlementRun") = .SettlementRun
                End With
                result.Rows.Add(row)
            Next
            result.AcceptChanges()

        Catch ex As Exception
            Throw New ApplicationException("Event ConvertToDatatable - " & ex.Message)
        End Try

        Return result
    End Function

    Private Sub IsChanged()
        Dim changed As Boolean = False

        If Me._chargeEnergy <> Me.rbEnergy.Checked Then
            changed = True
        ElseIf Me._chargeMF <> Me.rbMF.Checked Then
            changed = True
        ElseIf Me._billingPeriod <> Me.ddlBillingPeriod.Text Then
            changed = True
        ElseIf Me._dueDate <> Me.ddlDueDate.Text Then
            changed = True
        End If

        If changed = False Then
            Me.btnLoad.Text = "  Search"
            Me.btnGenerate.Enabled = True
        Else
            Me.btnLoad.Text = "  Search *"
            Me.btnGenerate.Enabled = False
        End If
    End Sub

    Private Sub GetControlStatus()
        Me._chargeEnergy = Me.rbEnergy.Checked
        Me._chargeMF = Me.rbMF.Checked
        Me._billingPeriod = Me.ddlBillingPeriod.Text
        Me._dueDate = Me.ddlDueDate.Text
    End Sub

    Private Function GetDataForOffsetting(ByVal listWESMBills As List(Of WESMBill), _
                                          ByVal listP2CMapping As List(Of ParticipantParentChildMapping), _
                                          ByVal listofOffsettingExemption As List(Of ParentChildExemption)) As List(Of WESMBill)
        Dim result As New List(Of WESMBill)
        Dim dicWESMBill As New Dictionary(Of String, WESMBill)
        Dim listCharge As List(Of EnumChargeType)

        Try
            'Get the list of charge type
            If Me.rbEnergy.Checked Then
                listCharge = (From x In listWESMBills _
                              Where x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.EV _
                              Select x.ChargeType Distinct).ToList()
            Else
                listCharge = (From x In listWESMBills _
                              Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV _
                              Select x.ChargeType Distinct).ToList()
            End If


            'Get the list of parent
            Dim listParent = (From x In listP2CMapping Select x.PCNumber Distinct).ToList()

            '***************************************************************************
            'Generation of Parent to parent/child to child offsetting
            Dim OffsettingExempDic As New Dictionary(Of String, ParentChildExemption)
            For Each itemCharge In listCharge
                Dim selectedItem = itemCharge

                Dim listFinalWESMBill As New List(Of WESMBill)

                Dim itemList1 = From x In listWESMBills Join y In listP2CMapping _
                                On x.IDNumber Equals y.IDNumber _
                                Where y.ParentFlag = 0 And x.ChargeType = selectedItem _
                                Select New WESMBill _
                                With {.BatchCode = x.BatchCode, .AMCode = x.AMCode, .BillingPeriod = x.BillingPeriod, _
                                      .SettlementRun = x.SettlementRun, .IDNumber = CStr(y.PCNumber), .InvoiceNumber = x.InvoiceNumber, _
                                      .RegistrationID = x.RegistrationID, .ForTheAccountOf = x.ForTheAccountOf, .InvoiceDate = x.InvoiceDate, _
                                      .Amount = x.Amount, .ChargeType = x.ChargeType, .DueDate = x.DueDate, .MarketFeesRate = x.MarketFeesRate, _
                                      .Remarks = x.Remarks}

                listFinalWESMBill.AddRange(itemList1.ToList())
                listFinalWESMBill.TrimExcess()

                'For Each ditem In listofOffsettingExemption
                '    Dim dKey As String = ditem.ParentParticipantID.IDNumber & "|" & ditem.ChildParticipantID.IDNumber & "|" & ditem.ChargeType.ToString
                '    If Not OffsettingExempDic.ContainsKey(dKey) Then
                '        OffsettingExempDic.Add(dKey, ditem)
                '    End If
                'Next

                If OffsettingExempDic.Count > 0 Then
                    Dim itemList2 = From x In listWESMBills _
                                Where x.ChargeType = selectedItem _
                                And Not (From y In listP2CMapping Where y.ParentFlag = 0 Select y.IDNumber).Contains(x.IDNumber) _
                                Select x

                    For Each item In itemList2
                        Dim dKey As String = item.IDNumber & "|" & item.RegistrationID & "|" & item.ChargeType.ToString
                        If Not OffsettingExempDic.ContainsKey(dKey) Then
                            'listFinalWESMBill.Add(item) 'Disabled Offsetting for BIR Ruling as of Feb 15, 2022
                        End If
                    Next
                    listFinalWESMBill.TrimExcess()
                Else
                    Dim itemList2 = From x In listWESMBills _
                                Where x.ChargeType = selectedItem _
                                And Not (From y In listP2CMapping Where y.ParentFlag = 0 Select y.IDNumber).Contains(x.IDNumber) _
                                Select x

                    'listFinalWESMBill.AddRange(itemList2.ToList()) 'Disabled Offsetting for BIR Ruling as of Feb 15, 2022
                    listFinalWESMBill.TrimExcess()
                End If
                

                'Get the distinct participants
                Dim listParticipants = (From x In listFinalWESMBill Select x.IDNumber Distinct Order By IDNumber).ToList()

                'Loop each participants
                For Each itemParticipant In listParticipants
                    Dim selectedParticipant = itemParticipant

                    'Get the WESM Bills of the selected participants
                    Dim listWESMBillParticipant = From x In listFinalWESMBill _
                                                  Where x.IDNumber = selectedParticipant _
                                                  Select x

                    '******** Generate the DM/CM
                    Dim AP = (From x In listWESMBillParticipant Where x.Amount > 0 Select x.Amount).Sum()

                    Dim AR = (From x In listWESMBillParticipant Where x.Amount < 0 Select Math.Abs(x.Amount)).Sum()

                    'Check if there is offsetting
                    If AP <> 0 And AR <> 0 Then
                        result.AddRange(listWESMBillParticipant)
                        result.TrimExcess()
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New ApplicationException("Event GetDatatForOffsetting - " & ex.Message)

        End Try

        Return result
    End Function

    Private Sub LoadFilters(ByVal filterValue As EnumFilterType)

        Try
            Select Case filterValue
                Case EnumFilterType.Energy
                    Dim BPs = (From x In Me._listGPPosted _
                               Where x.Charge = EnumChargeType.E Or x.Charge = EnumChargeType.EV _
                               Select x.BillingPeriod Distinct Order By BillingPeriod).ToList()
                    With Me.ddlBillingPeriod
                        .DataSource = BPs
                        If .Items.Count > 1 Then
                            .SelectedIndex = -1
                            Me.ddlDueDate.DataSource = Nothing
                            Me.ddlSettlementRun.DataSource = Nothing
                        ElseIf .Items.Count = 1 Then
                            .SelectedIndex = 0
                            If Me.ddlDueDate.Items.Count > 1 Then
                                Me.ddlDueDate.SelectedIndex = -1
                            ElseIf .Items.Count = 1 Then
                                Me.ddlDueDate.SelectedIndex = 0
                            Else
                                Me.ddlDueDate.SelectedIndex = -1
                            End If
                        Else
                            Me.ddlDueDate.DataSource = Nothing
                        End If
                    End With

                    If BPs.Count = 0 Then
                        Me.btnLoad.Enabled = False
                    Else
                        Me.btnLoad.Enabled = True
                    End If

                Case EnumFilterType.MarketFees
                    Dim BPs = (From x In Me._listGPPosted _
                               Where x.Charge = EnumChargeType.MF Or x.Charge = EnumChargeType.MFV _
                               Select x.BillingPeriod Distinct Order By BillingPeriod).ToList()
                    With Me.ddlBillingPeriod
                        .DataSource = BPs
                        If .Items.Count > 1 Then
                            .SelectedIndex = -1
                            Me.ddlDueDate.DataSource = Nothing
                        ElseIf .Items.Count = 1 Then
                            .SelectedIndex = 0
                            If Me.ddlDueDate.Items.Count > 1 Then
                                Me.ddlDueDate.SelectedIndex = -1
                            ElseIf .Items.Count = 1 Then
                                Me.ddlDueDate.SelectedIndex = 0
                            Else
                                Me.ddlDueDate.SelectedIndex = -1
                            End If
                        Else
                            Me.ddlDueDate.DataSource = Nothing
                        End If
                    End With

                    If BPs.Count = 0 Then
                        Me.btnLoad.Enabled = False
                    Else
                        Me.btnLoad.Enabled = True
                    End If

                Case EnumFilterType.BillingPeriod
                    If Me.ddlBillingPeriod.Text.Trim.Length = 0 Then
                        Exit Sub
                    End If

                    Dim billingPeriod As Integer = CInt(Me.ddlBillingPeriod.Text)
                    Dim DueDates As List(Of Date)
                    'Dim StlRunList As List(Of String)

                    If Me.rbEnergy.Checked Then
                        DueDates = (From x In Me._listGPPosted _
                                   Where (x.Charge = EnumChargeType.E Or x.Charge = EnumChargeType.EV) And x.BillingPeriod = billingPeriod _
                                   Select x.DueDate Distinct Order By DueDate).ToList()

                        'StlRunList = (From x In Me._listGPPosted _
                        '              Where (x.Charge = EnumChargeType.E Or x.Charge = EnumChargeType.EV) And x.BillingPeriod = billingPeriod _
                        '              Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    Else
                        DueDates = (From x In Me._listGPPosted _
                                   Where (x.Charge = EnumChargeType.MF Or x.Charge = EnumChargeType.MFV) And x.BillingPeriod = billingPeriod _
                                   Select x.DueDate Distinct Order By DueDate).ToList()

                        'StlRunList = (From x In Me._listGPPosted _
                        '           Where (x.Charge = EnumChargeType.MF Or x.Charge = EnumChargeType.MFV) And x.BillingPeriod = billingPeriod _
                        '           Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    End If

                    With Me.ddlDueDate
                        .DataSource = DueDates
                        If .Items.Count > 1 Then
                            .SelectedIndex = -1
                        ElseIf .Items.Count = 1 Then
                            .SelectedIndex = 0                        
                        End If
                    End With

                    'With Me.ddlSettlementRun
                    '    .DataSource = StlRunList
                    '    If .Items.Count > 1 Then
                    '        .SelectedIndex = -1
                    '    ElseIf .Items.Count = 1 Then
                    '        .SelectedIndex = 0
                    '    End If
                    'End With
                Case EnumFilterType.DueDate
                    If Me.ddlDueDate.Text.Trim.Length = 0 Then
                        Exit Sub
                    End If

                    Dim billingPeriod As Integer = CInt(Me.ddlBillingPeriod.Text)
                    Dim DueDates As Date = CDate(CDate(Me.ddlDueDate.Text).ToShortDateString)
                    Dim StlRunList As List(Of String)
                    If Me.rbEnergy.Checked Then                        
                        StlRunList = (From x In Me._listGPPosted _
                                      Where (x.Charge = EnumChargeType.E Or x.Charge = EnumChargeType.EV) And x.BillingPeriod = billingPeriod _
                                      Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    Else                        
                        StlRunList = (From x In Me._listGPPosted _
                                   Where (x.Charge = EnumChargeType.MF Or x.Charge = EnumChargeType.MFV) And x.BillingPeriod = billingPeriod _
                                   Select x.SettlementRun Distinct Order By SettlementRun).ToList()
                    End If

                    With Me.ddlSettlementRun
                        .Items.Clear()
                        If StlRunList.Count > 1 Then
                            .Items.Add("-ALL-")
                        End If
                        For Each item In StlRunList
                            .Items.Add(item)
                        Next
                        If .Items.Count > 1 Then
                            .SelectedIndex = -1
                        ElseIf .Items.Count = 1 Then
                            .SelectedIndex = -1
                        End If
                    End With
                Case Else

            End Select
        Catch ex As Exception
            Throw New ApplicationException("Event LoadFilters - " & ex.Message)
        End Try

    End Sub

    Private Sub LoadInputs()
        Try
            'Get billing period
            Me._billingPeriods = WBillHelper.GetCalendarBP()

            'Get the bill Participants
            Me._listParticipants = WBillHelper.GetAMParticipantsAll()

            'Get GP Posted for offsetting
            Me._listGPPosted = WBillHelper.GetGPPostedNotOffset()
        Catch ex As Exception
            Throw New ApplicationException("Event LoadInputs-" & ex.Message)
        End Try

    End Sub

    Private Function CheckForUnmappedIDNumber(ByVal listWESMBill As List(Of WESMBill), _
                                              ByVal listP2CMapping As List(Of ParticipantParentChildMapping)) As List(Of String)
        Dim result As New List(Of String)

        'Get the participants
        Dim participantsMapped = (From x In listP2CMapping Select x.IDNumber Distinct).AsEnumerable()

        'Get the participants in WESM Bill
        Dim participants = (From x In listWESMBill Join y In Me._listParticipants _
                            On CStr(x.IDNumber) Equals y.IDNumber _
                             Select x.IDNumber, y.ParticipantID Distinct Order By ParticipantID).ToList()

        For Each item In participants
            If Not participantsMapped.Contains(CStr(item.IDNumber)) Then
                result.Add(item.ParticipantID)
            End If
        Next
        result.TrimExcess()

        Return result
    End Function

    Private Function GetDataInDataGrid() As DataTable
        Dim dt As New DataTable

        Try
            With dt.Columns
                .Add("InvoiceNumber", GetType(Integer))
                .Add("IDNumber", GetType(Integer))
                .Add("ParticipantID", GetType(String))
                .Add("InvoiceDate", GetType(String))
                .Add("ChargeType", GetType(String))
                .Add("Amount", GetType(Decimal))
                .Add("SettlementRun", GetType(String))
            End With
            dt.AcceptChanges()

            For index As Integer = 0 To Me.DGridView.RowCount - 1
                With Me.DGridView.Rows(index)
                    Dim row = dt.NewRow()
                    row("InvoiceNumber") = .Cells("colInvoiceNo").Value
                    row("IDNumber") = .Cells("colIDNumber").Value
                    row("ParticipantID") = .Cells("colParticipantID").Value
                    row("InvoiceDate") = .Cells("colInvoiceDate").Value
                    row("ChargeType") = .Cells("colChargeType").Value
                    row("Amount") = .Cells("colAmount").Value
                    row("SettlementRun") = .Cells("colStlRun").Value
                    dt.Rows.Add(row)
                End With
            Next
        Catch ex As Exception
            Throw New ApplicationException("Event GetDataInDatagrid - " & ex.Message)
        End Try

        Return dt
    End Function

#End Region


End Class