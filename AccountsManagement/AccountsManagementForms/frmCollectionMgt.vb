'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionMgt
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     December 27, 2011
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
'   December 27, 2011       Vladimir E. Espiritu                 GUI design and basic functionalities   
'   March 19, 2012          Vladimir E. Espiritu                 Added function that will get the list of prudential
'   March 26, 2012          Vladimir E. Espiritu                 Added automatic computation of drawdown
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin
Imports System.Runtime.Serialization.Formatters.Binary
Imports WESMLib.Auth.Lib

Public Class frmCollectionMgt

#Region "Properties"
    Public Enum CollectionLoadType
        Add
        Edit
        View
        Save
    End Enum

    Public _dicInterestRate As Dictionary(Of Date, Decimal)

    Private _LoadType As CollectionLoadType
    Public Property LoadType() As CollectionLoadType
        Get
            Return _LoadType
        End Get
        Set(ByVal value As CollectionLoadType)
            _LoadType = value
        End Set
    End Property


    Private _ItemCollection As Collection
    Public Property Itemcollection() As Collection
        Get
            Return _ItemCollection
        End Get
        Set(ByVal value As Collection)
            _ItemCollection = value
        End Set
    End Property

    Private _ListWESMBillSummaries As List(Of WESMBillSummary)
    Public Property ListWESMBillSummaries() As List(Of WESMBillSummary)
        Get
            Return _ListWESMBillSummaries
        End Get
        Set(ByVal value As List(Of WESMBillSummary))
            _ListWESMBillSummaries = value
        End Set
    End Property

    Private _SelectedIDNumber As AMParticipants
    Public Property SelectedIDNumber() As AMParticipants
        Get
            Return _SelectedIDNumber
        End Get
        Set(ByVal value As AMParticipants)
            _SelectedIDNumber = value
        End Set
    End Property




#End Region

    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory
    Private Participants As List(Of AMParticipants)
    Private ListOfPrudential As List(Of Prudential)
    Private ListAccountingCodes As List(Of AccountingCode)
    Private ListHeldCollection As List(Of CollectionMonitoring)
    Private itemCollectionMonitoring As CollectionMonitoring
    Private panelHeight As Integer
    Private checkAllBox As Boolean = False
    Private Enum EnumORRemarks
        Energy
        VATonEnergy
        MarketFees
        VATonMarketFees
        ExcessCollection
        PrudentialReplenishment
        HeldCollection
    End Enum

    Private Sub frmCollectionMgt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            'Set the SelectedIDNumber
            Me.SelectedIDNumber = New AMParticipants()

            'Reset WESM Bill Summaries
            Me._ListWESMBillSummaries = New List(Of WESMBillSummary)

            'Rest list of Collection Monitoring
            Me.ListHeldCollection = New List(Of CollectionMonitoring)

            'Get the list of Participants
            Me.Participants = WBillHelper.GetAMParticipants()

            'Get the list of Prudential
            Me.ListOfPrudential = WBillHelper.GetParticipantsPrudential()

            'Get the Accounting Code
            Me.ListAccountingCodes = WBillHelper.GetAccountingCodes()

            'Get the daily interest rates
            Me._dicInterestRate = WBillHelper.GetDailyInterestRate()

            With Me.ddlParticipantID
                .DisplayMember = "ParticipantID"
                .ValueMember = "IDNumber"
                .DataSource = CType(BFactory.CloneObject(Me.Participants), List(Of AMParticipants))
                .SelectedIndex = -1
            End With

            With Me.chckParticipantList
                .DataSource = CType(BFactory.CloneObject(Me.Participants), List(Of AMParticipants))
                .DisplayMember = "ParticipantID"
                .ValueMember = "IDNumber"
            End With

            Me.PanelParticipants.Visible = False
            Me.ClearInputs()
            Me.itemCollectionMonitoring = New CollectionMonitoring()

            Select Case Me.LoadType
                Case CollectionLoadType.Add
                    AddHandler Me.ddlParticipantID.SelectedIndexChanged, AddressOf Me.ddlParticipantID_SelectedIndexChanged

                    Me.rbAutomatic.Select()
                    Me.rbAutomatic.Checked = True

                    Me.gpManual.Visible = False


                Case CollectionLoadType.Edit
                    AddHandler Me.ddlParticipantID.SelectedIndexChanged, AddressOf Me.ddlParticipantID_SelectedIndexChanged

                    With Me.Itemcollection
                        Me.DTCollection.Value = .CollectionDate
                        Me.ddlParticipantID.SelectedValue = .IDNumber
                        Me.txtAmountCollected.Text = FormatNumber(.CollectedAmount, 2)
                        Me.rbAutomatic.Checked = True
                        Me.DTCollection.Enabled = True
                        Me.ddlParticipantID.Enabled = True

                        If Me.Itemcollection.DailyBatchCode.Length > 0 Then
                            Me.txtAmountCollected.ReadOnly = False
                            Me.txtAmountCollected.BackColor = Color.White
                        End If

                        'Compute the remaining cash and applied amount
                        Me.ComputeRemainingCash()
                        Me.ComputeTotalAppliedAmount()
                    End With

                Case CollectionLoadType.View
                    With Me.Itemcollection
                        Me.DTCollection.Value = .CollectionDate
                        Me.ddlParticipantID.SelectedValue = .IDNumber
                        Me.txtAmountCollected.Text = FormatNumber(.CollectedAmount, 2)
                        If .AllocationType = EnumAllocationType.Automatic Then
                            Me.rbAutomatic.Checked = True
                        Else
                            Me.rbManual.Checked = True
                        End If

                        Me.btnSave.Visible = False
                        Me.btnCancel.Enabled = True
                        Me.btnCancel.Text = "Close"
                    End With
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.FormValidation = False Then
            Exit Sub
        End If

        Try
            Dim listCollectionMonitoring As New List(Of CollectionMonitoring)
            Dim ans As MsgBoxResult
            Dim IDNumber As String

            If Me.rbAutomatic.Checked Then

                If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "No access to save new collection", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
                    Exit Sub
                End If

            End If


            If Not Me._dicInterestRate.ContainsKey(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))) Then
                MsgBox("No Interest Rate for " & FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate), _
                       MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            IDNumber = CStr(Me.ddlParticipantID.SelectedValue)
            Dim items = WBillHelper.GetCollections(Me.Itemcollection.CollectionNumber, IDNumber, CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), _
                                                   CDec(Me.txtAmountCollected.Text))


            If items.Count > 0 Then
                MsgBox("Duplicate Entry!!!", MsgBoxStyle.Exclamation, "Warning")
            End If

            'Check if the collection is already posted for daily collection
            If Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) <> 0 Then
                'Collection which to be saved as directly manual is forbidden
                If Me.Itemcollection.ORNo = 0 Then
                    MsgBox("Please save first the collection as Automatic!", MsgBoxStyle.Critical, "Denied")
                    Exit Sub
                End If

                'Check if the collection is already posted in daily collection
                If Me.Itemcollection.DailyBatchCode.Length = 0 Then
                    MsgBox("OR No:" & BFactory.GenerateBIRDocumentNumber(Me.Itemcollection.ORNo, BIRDocumentsType.OfficialReceipt) & " is not yet posted for daily collection.", MsgBoxStyle.Critical, "Not yet posted")
                    Exit Sub
                End If

                'Check if the collection change the collection date
                If Me.Itemcollection.CollectionDate <> CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                    MsgBox("The selected collection has changed it's collection date!", MsgBoxStyle.Critical, "Collection Date has changed")
                    Me.DTCollection.Value = Me.Itemcollection.CollectionDate
                    Exit Sub
                End If
            End If

            ans = MsgBox("Do you really want to save this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim listCollections As New List(Of Collection)
            Dim item As New Collection
            With item
                .ORNo = Me.Itemcollection.ORNo
                .CollectionNumber = Me.Itemcollection.CollectionNumber
                .CollectionDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                .IDNumber = CStr(Me.ddlParticipantID.SelectedValue)
                .CollectedAmount = CDec(Me.txtAmountCollected.Text)

                If rbManual.Checked Then
                    .CollectedPrudential = CDec(Me.txtPrudentialReplenishment.Text)
                Else
                    .CollectedPrudential = 0
                End If

                .AllocationType = If(Me.rbAutomatic.Checked, EnumAllocationType.Automatic, EnumAllocationType.Manual)
                .CollectionCategory = EnumCollectionCategory.Cash
                .Status = EnumCollectionStatus.NotAllocated
                .DailyBatchCode = Me.Itemcollection.DailyBatchCode

                If Me.rbAutomatic.Checked Then
                    .CollectedHeld = 0
                    .AmountForAllocation = .CollectedAmount
                Else
                    .CollectedHeld = CDec(Me.txtHeldPayment.Text)
                    .AmountForAllocation = .CollectedAmount + CDec(Me.txtHeldPayment.Text) - .CollectedPrudential
                End If
            End With

            'Add the collection
            listCollections.Add(item)

            If rbAutomatic.Checked Then
                If item.DailyBatchCode.Length = 0 Then
                    Me.WBillHelper.SaveCollection(item)
                Else
                    Me.EditCollection(item)
                End If
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Saved new collection", "OR No. " & item.ORNo.ToString(), "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
            Else

                'Selection of allocation date
                Dim frm As New frmCollectionSearch
                With frm
                    Dim valSize As New System.Drawing.Size
                    valSize.Width = 343
                    valSize.Height = 130

                    .Size = valSize
                    .LoadType = frmCollectionSearch.EnumFunctionType.SelectAllocationDate
                    .dtAllocationDate.Value = Me.DTCollection.Value

                    If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                        Exit Sub
                    End If
                End With

                Dim AllocatioDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))

                If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
                    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "No credential to allocate collection manually", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
                    Exit Sub
                End If

                If WBillHelper.GetCountAMPayment(AllocatioDate) <> 0 Then
                    MsgBox(AllocatioDate.ToString("MM/dd/yyyy") & " was already use in Payment Allocation. " & _
                           "Please select another allocaton date!", MsgBoxStyle.Critical, "Invalid Date")
                    Exit Sub
                ElseIf AllocatioDate < CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                    MsgBox("Allocation date must be greater than or equal to collection date!", MsgBoxStyle.Critical, "Specify the inputs")
                    Exit Sub
                Else
                    For index As Integer = 0 To Me.DGridView.RowCount - 1
                        With Me.DGridView.Rows(index)
                            If AllocatioDate < CDate(FormatDateTime(CDate(.Cells("colNewDueDate").Value), DateFormat.ShortDate)) _
                               And (CDec(.Cells("colCash").Value) <> 0 Or CDec(.Cells("colDrawdown").Value) <> 0) Then

                                MsgBox("Allocation date must be greater than or equal to due date/s!", MsgBoxStyle.Critical, "Specify the inputs")
                                Exit Sub
                            End If
                        End With
                    Next
                End If

                Dim result = Me.SaveManualCollection(IDNumber, listCollections, AllocatioDate)
                If result = False Then
                    Exit Sub
                End If

                'Save the default interest if edited
                For index As Integer = 0 To Me.DGridView.RowCount - 1
                    With Me.DGridView.Rows(index)
                        If CDec(.Cells("colDefaultInterest").Value) <> CDec(.Cells("colDefaultInterestOrig").Value) Then
                            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Change Default Interest of WESM Bill No/DMCM No. " & _
                            CStr(.Cells("colInvDMCMNo").Value) & " from " & CStr(.Cells("colDefaultInterestOrig").Value) & " into " & _
                            CStr(.Cells("colDefaultInterest").Value), "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                        End If
                    End With
                Next
            End If

            If Me.Itemcollection.CollectionNumber = 0 Then
                Dim selectParticipantID = (From x In Me.Participants _
                                           Where x.IDNumber = item.IDNumber _
                                           Select x.ParticipantID).FirstOrDefault()

                For Each itemCol In listCollections
                    With itemCol

                        Dim createdDocument As String = ""

                        If itemCol.DMCMNumber <> 0 Then
                            createdDocument = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                        End If

                        If itemCol.ORNo <> 0 Then
                            createdDocument = BFactory.GenerateBIRDocumentNumber(.ORNo, BIRDocumentsType.OfficialReceipt)
                        End If

                        frmCollection.DGridViewCollection.Rows.Insert(0, .CollectionNumber, .ORNo, .DMCMNumber, createdDocument, _
                                                                      FormatDateTime(.CollectionDate, DateFormat.ShortDate), _
                                                                     .IDNumber, selectParticipantID, .CollectedAmount, _
                                                                      .CollectedPrudential, _
                                                                      .CollectedHeld, _
                                                                      .AmountForAllocation, _
                                                                      .CollectionCategory, .AllocationType, .Status, False, .IsPosted, _
                                                                      If(.AllocationDate <> New Date(), .AllocationDate.ToString("MM/dd/yyyy"), ""), _
                                                                      .DailyBatchCode)
                    End With

                    If itemCol.Status = EnumCollectionStatus.Allocated Then
                        frmCollection._ListOfCollections.Add(itemCol)
                        frmCollection._ListOfCollections.TrimExcess()
                    End If

                    'Updated By Lance 08/18/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Successfully allocated collection", "Collection no. " & itemCol.CollectionNumber.ToString(), "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

                Next

                ans = MsgBox("Do you want to add another?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Add new")
                If ans = vbNo Then
                    Me.Close()
                Else
                    Me.ClearInputs()

                    'Get the list of Participants
                    Me.Participants = WBillHelper.GetAMParticipants()

                    'Get the list of Prudential
                    Me.ListOfPrudential = WBillHelper.GetParticipantsPrudential()
                End If
            Else
                '******* Comment to disable comment
                MsgBox("Successfully Edited!", MsgBoxStyle.Information, "Updated")

                For Each itemCol In listCollections
                    Dim selectedCollection = itemCol

                    If itemCol.CollectionCategory = EnumCollectionCategory.Cash Then
                        Dim index As Integer = frmCollection.DGridViewCollection.CurrentRow.Index

                        With frmCollection.DGridViewCollection.Rows(index)
                            .Cells("colIDNumber").Value = item.IDNumber
                            .Cells("colParticipantID").Value = Me.ddlParticipantID.Text
                            .Cells("colCollectionDate").Value = FormatDateTime(item.CollectionDate, DateFormat.ShortDate)
                            .Cells("colCollected").Value = item.CollectedAmount
                            .Cells("colPrudentialReplenishment").Value = item.CollectedPrudential
                            .Cells("colHeld").Value = CDec(Me.txtHeldPayment.Text)
                            .Cells("colAmountForAllocation").Value = item.AmountForAllocation
                            .Cells("colType").Value = item.CollectionCategory
                            .Cells("colAllocationType").Value = item.AllocationType
                            .Cells("colStatus").Value = item.Status
                            .Cells("colIsPosted").Value = item.IsPosted
                            .Cells("colDateAllocated").Value = If(item.AllocationDate <> New Date(), item.AllocationDate.ToString("MM/dd/yyyy"), "")
                            .Cells("colDailyBatchCode").Value = item.DailyBatchCode
                        End With
                    Else
                        Dim selectParticipantID = (From x In Me.Participants
                                                   Where x.IDNumber = selectedCollection.IDNumber
                                                   Select x.ParticipantID).FirstOrDefault()

                        With itemCol
                            frmCollection.DGridViewCollection.Rows.Insert(0, .CollectionNumber, .ORNo, .DMCMNumber, BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM),
                                                                          FormatDateTime(.CollectionDate, DateFormat.ShortDate),
                                                                          .IDNumber, selectParticipantID, .CollectedAmount,
                                                                          .CollectedPrudential,
                                                                          .CollectedHeld,
                                                                          .AmountForAllocation,
                                                                          .CollectionCategory, .AllocationType, .Status, False, .IsPosted,
                                                                          If(.AllocationDate <> New Date(), .AllocationDate.ToString("MM/dd/yyyy"), ""),
                                                                          .DailyBatchCode)
                        End With
                    End If

                    If itemCol.Status = EnumCollectionStatus.Allocated Then
                        frmCollection._ListOfCollections.Add(itemCol)
                        frmCollection._ListOfCollections.TrimExcess()
                    End If
                    'Updated By Lance 08/18/2014
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Successfully allocated collection", "Collection no. " & itemCol.CollectionNumber.ToString(), "", CType(EnumColorCode.Red, ColorCode), EnumLogType.SuccessfullyAllocated.ToString, AMModule.UserName)
                Next
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub DTCollection_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTCollection.CloseUp, DTCollection.LostFocus
        Me.DGridView.Rows.Clear()
        Me.ListWESMBillSummaries.Clear()

        For index As Integer = 0 To Me.chckParticipantList.CheckedItems.Count - 1
            Dim PaidTo As String = CType(Me.chckParticipantList.CheckedItems(index), AMParticipants).IDNumber

            'Get the WESM Bill Summary for collection
            Dim items = WBillHelper.GetWESMBillSummaryForCollectionAllocation(PaidTo)
            Dim getListofInvoiceForBIRRuling As List(Of String) = items.Where(Function(c) c.INVDMCMNo.StartsWith("TS-W") And Not c.INVDMCMNo.ToUpper Like "*-ADJ*").OrderBy(Function(z) z.INVDMCMNo).
                                                                  Select(Function(x) x.INVDMCMNo).Distinct.ToList

            If getListofInvoiceForBIRRuling.Count <> 0 Then
                Dim transItems = WBillHelper.GetListWESMTransCoverSummary2(getListofInvoiceForBIRRuling)
                Me.LoadWESMBillSummary(items, transItems)
            Else
                Dim transItems As List(Of WESMBillAllocCoverSummary) = New List(Of WESMBillAllocCoverSummary)
                Me.LoadWESMBillSummary(items, transItems)
            End If
            Me.ListWESMBillSummaries.AddRange(items)
        Next

        'Compute for the totals
        Me.ComputeTotals()
    End Sub

    Private Sub DTCollection_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DTCollection.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.ddlParticipantID.Select()
        End If
    End Sub

    Private Sub ddlParticipantID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddlParticipantID.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.txtAmountCollected.Select()
        End If
    End Sub

    Private Sub txtAmountCollected_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmountCollected.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.btnSave.Select()
        End If
    End Sub

    Private Sub DGridView_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DGridView.CellBeginEdit
        Try
            Select Case e.ColumnIndex
                Case 18 'For column Cash

                    'Compute the amount allocated
                    Dim subTotal As Decimal = 0
                    For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                        If i <> e.RowIndex Then
                            subTotal += CDec(Me.DGridView.Rows(i).Cells("colCash").Value)
                        End If
                    Next

                    'Get the remaning cash
                    Dim remainingAmount As Decimal = CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) -
                                                     CDec(Me.txtPrudentialReplenishment.Text) - subTotal

                    Dim DefaultInterestAmount = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) +
                                                CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithhold").Value)



                    'It will disable cash column if the remaining cash is not already enough to pay the default interest for Market Fees
                    If CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.MF.ToString() And
                       CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                       (remainingAmount = 0 Or Math.Abs(DefaultInterestAmount) > remainingAmount) Then

                        e.Cancel = True

                        'It will disable cash column if the remaining cash and prudential are not 
                        'already enough to pay the default interest for energy
                    ElseIf CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() And
                           CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                           (remainingAmount = 0 Or Math.Abs(DefaultInterestAmount) >
                            remainingAmount + CDec(Me.txtRemainingPrudential.Text)) Then

                        e.Cancel = True
                    End If

                Case 19 'This will not allowed the user to check the FullyPaid column if there is no more money to be allocated
                    If Me.LoadType = CollectionLoadType.Add And CDec(Me.txtPrudentialAmount.Text) > 0 Then
                        'Compute the amount allocated
                        Dim subTotal As Decimal = 0
                        For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                            If i <> e.RowIndex Then
                                subTotal += CDec(Me.DGridView.Rows(i).Cells("colDrawdown").Value)
                            End If
                        Next
                        'Get the remaning cash
                        Dim remainingAmount As Decimal = CDec(Me.txtPrudentialAmount.Text) - subTotal

                        Dim DefaultInterestAmount = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) +
                                                    CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithhold").Value)

                        If CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                               CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() And
                               (remainingAmount = 0 Or Math.Abs(DefaultInterestAmount) > remainingAmount) Then
                            e.Cancel = True
                        ElseIf CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = True And
                                CBool(Me.cb_CheckAll.Checked) = True Then
                            Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value = False
                            Me.ResetDatagridRow(e.RowIndex)
                            'Compute the remaining cash, prudential and applied amount                            
                            Me.ComputeRemainingPrudential()
                            Me.ComputeTotalAppliedAmount()
                        ElseIf CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                                CBool(Me.cb_CheckAll.Checked) = False Then
                            Dim ChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType),
                                                   CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value)), EnumChargeType)
                            If ChargeType = EnumChargeType.E Then
                                Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value = True
                                Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                                With Me.DGridView.Rows(e.RowIndex)
                                    'Enable/Disable AmoutToPay column
                                    If CBool(.Cells("colChckPay").Value) = False Then
                                        .Cells("colDrawdown").ReadOnly = False
                                        'Reset the datagrid row
                                        Me.ResetDatagridRow(Me.DGridView.Rows(e.RowIndex).Index)
                                    Else
                                        'Reset the datagrid row
                                        Me.ResetDatagridRow(Me.DGridView.Rows(e.RowIndex).Index)
                                        Dim AmountToAllocate As Decimal = 0
                                        If remainingAmount >= CDec(.Cells("colTotalPayable").Value) Then
                                            AmountToAllocate = Math.Abs(CDec(.Cells("colTotalPayable").Value))
                                        Else
                                            AmountToAllocate = remainingAmount
                                        End If
                                        .Cells("colDrawdown").ReadOnly = True
                                        .Cells("colDrawdown").Value = AmountToAllocate
                                        Me.PREnergyApplication(e.RowIndex)
                                    End If
                                End With
                            End If
                            Me.ComputeRemainingPrudential()
                        End If
                    ElseIf Me.LoadType = CollectionLoadType.Edit Then
                        'Compute the amount allocated
                        Dim subTotal As Decimal = 0
                        For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                            If i <> e.RowIndex Then
                                subTotal += CDec(Me.DGridView.Rows(i).Cells("colCash").Value)
                            End If
                        Next

                        'Get the remaning cash
                        Dim remainingAmount As Decimal = CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) -
                                                         CDec(Me.txtPrudentialReplenishment.Text) - subTotal

                        Dim DefaultInterestAmount = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) +
                                                    CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithhold").Value)

                        If CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                           CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.MF.ToString() And
                           (remainingAmount = 0 Or Math.Abs(DefaultInterestAmount) > remainingAmount) Then

                            e.Cancel = True

                            'It will disable fully paid column if the remaining cash and prudential are not 
                            'already enough to pay the default interest for energy
                        ElseIf CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                               CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() And
                               (remainingAmount = 0 Or Math.Abs(DefaultInterestAmount) >
                                remainingAmount + CDec(Me.txtRemainingPrudential.Text)) Then

                            e.Cancel = True

                        ElseIf CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = False And
                               CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.EV.ToString() And
                               remainingAmount = 0 Then

                            e.Cancel = True
                        ElseIf CBool(Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value) = True And
                                CBool(Me.cb_CheckAll.Checked) = True Then
                            Me.DGridView.Rows(e.RowIndex).Cells("colChckPay").Value = False
                            Me.ResetDatagridRow(e.RowIndex)
                            'Compute the remaining cash, prudential and applied amount
                            Me.ComputeRemainingCash()
                            Me.ComputeRemainingPrudential()
                            Me.ComputeTotalAppliedAmount()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'Added this try catch exception to trap the error when fully paid check box was checked and replenishment value is not numeric
        End Try
    End Sub

    Private Sub DGridView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellEndEdit
        Try
            If Me.DGridView.Rows.Count = 0 Then
                Exit Sub
            End If

            Select Case e.ColumnIndex 'For Default Interest
                Case 13
                    Dim defaultInterestOrig = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterestOrig").Value)
                    Dim defaultWithholdOrig = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithholdOrig").Value)
                    Dim invNo As String = CStr(Me.DGridView.Rows(e.RowIndex).Cells("colInvDMCMNoText").Value)

                    'Check if its numeric
                    If Not IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) Then
                        MsgBox("Default interest must be numeric!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value = defaultInterestOrig
                        Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithhold").Value = defaultWithholdOrig
                        Me.ResetDatagridRow(e.RowIndex)

                        With Me.DGridView.Rows(e.RowIndex)
                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End With
                        Exit Sub

                        'Check if its zero or equal to original default interest
                        '*** 10/10/2017
                        '*** removed this portion of code due to request by Annsburg for allowing the default interest to edit even it is less than the original amount of default interest 
                        'And defaultInterestOrig <> CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value)
                    ElseIf CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) <> 0 And _
                        CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value) < 0 Then
                        MsgBox("Default interest for " & invNo & " must be zero or " & defaultInterestOrig.ToString(), MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").Value = defaultInterestOrig
                        Me.DGridView.Rows(e.RowIndex).Cells("colDefaultWithhold").Value = defaultWithholdOrig
                        Me.ResetDatagridRow(e.RowIndex)

                        With Me.DGridView.Rows(e.RowIndex)
                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End With
                        Exit Sub
                    End If

                Case 14

                    'Check if its numeric
                    If Not IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colTaxOrig").Value) Then
                        MsgBox("Withholding Tax  must be numeric!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colTaxOrig").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)

                        With Me.DGridView.Rows(e.RowIndex)
                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End With
                        Exit Sub
                    End If

                Case 15

                    'Check if its numeric
                    If Not IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colVatOrig").Value) Then
                        MsgBox("Withholding VAT  must be numeric!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colVatOrig").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)

                        With Me.DGridView.Rows(e.RowIndex)
                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End With
                        Exit Sub
                    End If

                Case 18 'For Cash

                    'Check if its numeric but it will not execute because its already considered in CurrentCellDirtyChangedEvent
                    If Not IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) Then
                        MsgBox("Cash must be numeric!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)
                        Exit Sub

                        'Check if its negative
                    ElseIf CDec(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) < 0 Then
                        MsgBox("Cash must not be negative!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)
                        Exit Sub

                        'Check if the iputted cash is greater than total payable
                    ElseIf CDec(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) > _
                           Math.Abs(CDec(Me.DGridView.Rows(e.RowIndex).Cells("colTotalPayable").Value)) Then
                        MsgBox("The inputted amount is greater than total payable!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)
                        Exit Sub

                    End If

                    'Get the cash allocated in other invoices
                    Dim subTotal As Decimal = 0
                    For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                        If i <> e.RowIndex Then
                            If CBool(Me.DGridView.Rows(i).Cells("colCash").Value) = True Then
                                subTotal += CDec(Me.DGridView.Rows(i).Cells("colCash").Value)
                            End If
                        End If
                    Next

                    'Check if the inputted cash is more than the remaining cash 
                    Dim remainingCash As Decimal = CDec(Me.txtHeldPayment.Text) + CDec(Me.txtAmountCollected.Text) - CDec(Me.txtPrudentialReplenishment.Text) - subTotal
                    If CDec(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) > remainingCash Then
                        MsgBox("The inputted amount is greater than the remaining cash!", MsgBoxStyle.Critical, "Error")
                        Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value = 0
                        Me.ResetDatagridRow(e.RowIndex)
                        Exit Sub
                    End If

                Case 20 'For Drawdown

                    'Check if its numeric
                    If Not IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value) Then
                        MsgBox("Drawdown must be numeric!", MsgBoxStyle.Critical, "Error")
                        With Me.DGridView.Rows(e.RowIndex)
                            .Cells("colDrawdown").Value = 0
                            .Cells("colDrawdownEnergy").Value = 0
                            .Cells("colDrawdownDefaultEnergy").Value = 0
                            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - CDec(.Cells("colCash").Value)
                            Exit Sub
                        End With

                        'Check if its negative
                    ElseIf CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value) < 0 Then
                        MsgBox("Drawdown must not be negative!", MsgBoxStyle.Critical, "Error")
                        With Me.DGridView.Rows(e.RowIndex)
                            .Cells("colDrawdown").Value = 0
                            .Cells("colDrawdownEnergy").Value = 0
                            .Cells("colDrawdownDefaultEnergy").Value = 0
                            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - CDec(.Cells("colCash").Value)
                            Exit Sub
                        End With

                        'Check if the iputted cash is greater than total payable
                    ElseIf CDec(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) + _
                           CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value) > _
                           Math.Abs(CDec(Me.DGridView.Rows(e.RowIndex).Cells("colTotalPayable").Value)) Then
                        MsgBox("The inputted amount is greater than total payable!", MsgBoxStyle.Critical, "Error")
                        With Me.DGridView.Rows(e.RowIndex)
                            .Cells("colDrawdown").Value = 0
                            .Cells("colDrawdownEnergy").Value = 0
                            .Cells("colDrawdownDefaultEnergy").Value = 0
                            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - CDec(.Cells("colCash").Value)
                            Exit Sub
                        End With

                    End If

                    'Get the drawdown allocated in other invoices
                    Dim subTotal As Decimal = 0
                    For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                        If i <> e.RowIndex Then
                            If CBool(Me.DGridView.Rows(i).Cells("colDrawdown").Value) = True Then
                                subTotal += CDec(Me.DGridView.Rows(i).Cells("colDrawdown").Value)
                            End If
                        End If
                    Next

                    'Check if the inputted amount is more than the remaining prudential 
                    If CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value) > CDec(Me.txtPrudentialAmount.Text) + subTotal Then
                        MsgBox("The inputted amount is greater than the remaining prudential!", MsgBoxStyle.Critical, "Error")
                        With Me.DGridView.Rows(e.RowIndex)
                            .Cells("colDrawdown").Value = 0
                            .Cells("colDrawdownEnergy").Value = 0
                            .Cells("colDrawdownDefaultEnergy").Value = 0
                            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - CDec(.Cells("colCash").Value)
                            Exit Sub
                        End With
                    End If


            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_CollectionAllocationWindow.ToString(), ex.Message, "Error in DGridView_CellEndEdit event", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInRetrieving.ToString(), 'LDAPModule.LDAP.Username)
        End Try
    End Sub

    Private Sub rbAutomatic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAutomatic.CheckedChanged
        If Me.rbAutomatic.Checked Then
            Dim valSize As New System.Drawing.Size
            valSize.Width = 800
            valSize.Height = 290
            Me.Size = valSize

            Me.gpManual.Visible = False
            Me.txtForTheAcountOf.Visible = False
            Me.lblAcountFor.Visible = False
            Me.PanelParticipants.Visible = False

            Me.lblCollectionDate.Location = New Drawing.Point(427, 19)
            Me.lblPrudentialAmount.Location = New Drawing.Point(452, 44)

            Me.DTCollection.Location = New Drawing.Point(525, 16)
            Me.txtPrudentialAmount.Location = New Drawing.Point(525, 41)

            Me.lblHeldPayment.Location = New Drawing.Point(428, 66)
            Me.lblCollectedAmount.Location = New Drawing.Point(415, 91)
            Me.lblPrudentialReplenishment.Location = New Drawing.Point(377, 115)

            Me.txtHeldPayment.Location = New Drawing.Point(525, 64)
            Me.txtAmountCollected.Location = New Drawing.Point(525, 88)
            Me.txtPrudentialReplenishment.Location = New Drawing.Point(525, 112)

            'Me.lblPrudentialReplenishment.Visible = False
            'Me.txtPrudentialReplenishment.Visible = False

            Me.txtAmountCollected.ReadOnly = False
        End If

    End Sub

    Private Sub rbManual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbManual.CheckedChanged
        If Me.rbManual.Checked Then
            Dim valSize As New System.Drawing.Size
            valSize.Width = 1250
            valSize.Height = 624
            Me.Size = valSize

            Me.gpManual.Visible = True
            Me.lblAcountFor.Visible = True
            Me.txtForTheAcountOf.Visible = True

            Me.lblCollectionDate.Location = New Drawing.Point(669, 19)
            Me.lblPrudentialAmount.Location = New Drawing.Point(694, 44)

            Me.DTCollection.Location = New Drawing.Point(766, 16)
            Me.txtPrudentialAmount.Location = New Drawing.Point(766, 41)

            Me.lblHeldPayment.Location = New Drawing.Point(670, 66)
            Me.lblCollectedAmount.Location = New Drawing.Point(657, 91)
            Me.lblPrudentialReplenishment.Location = New Drawing.Point(613, 115)

            Me.txtHeldPayment.Location = New Drawing.Point(766, 64)
            Me.txtAmountCollected.Location = New Drawing.Point(766, 88)
            Me.txtPrudentialReplenishment.Location = New Drawing.Point(766, 112)

            'Me.lblPrudentialReplenishment.Visible = True
            'Me.txtPrudentialReplenishment.Visible = True

            Me.txtAmountCollected.ReadOnly = True
        End If
    End Sub

    Private Sub txtAmountCollected_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmountCollected.LostFocus
        If Me.txtAmountCollected.Text.Trim.Length = 0 Then
            Me.txtAmountCollected.Text = "0.00"
            Exit Sub
        ElseIf Not IsNumeric(Me.txtPrudentialReplenishment.Text) Then
            Exit Sub
        ElseIf Not IsNumeric(Me.txtAmountCollected.Text) Then
            MsgBox("Collected amount must be numeric!", MsgBoxStyle.Critical, "Warning")
            Me.txtAmountCollected.Text = "0.00"
            Exit Sub
        ElseIf CDec(Me.txtAmountCollected.Text) < 0 Then
            MsgBox("Collected amount must not be less than zero!", MsgBoxStyle.Critical, "Warning")
            Me.txtAmountCollected.Text = "0.00"
            Exit Sub
        End If

        Me.txtAmountCollected.Text = FormatNumber(CDec(Me.txtAmountCollected.Text), 2)
    End Sub

    Private Sub txtAmountCollected_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                                        Handles txtAmountCollected.TextChanged
        Try
            Me.ComputeRemainingCash()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub DGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridView.CellFormatting
        Try
            Select Case e.ColumnIndex
                Case 13 'For Default Interest
                    Me.DGridView.Rows(e.RowIndex).Cells("colDefaultInterest").ReadOnly = True                                       
                Case 14, 15
                    If CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() _
                      Or CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.EV.ToString() Then
                        Me.DGridView.Rows(e.RowIndex).Cells("colTaxOrig").ReadOnly = True                        
                    End If
                Case 15
                    If CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() _
                      Or CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.EV.ToString() Then
                        Me.DGridView.Rows(e.RowIndex).Cells("colVatOrig").ReadOnly = True
                    End If
                Case 18 'For Cash Column

                    'Format into decimal
                    If IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value) Then
                        Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colCash").Value)
                    End If

                Case 20 'For Drawdown
                    If CStr(Me.DGridView.Rows(e.RowIndex).Cells("colChargeType").Value) = EnumChargeType.E.ToString() _
                       And CDec(Me.txtPrudentialAmount.Text) > 0 Then
                        Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").ReadOnly = False
                    Else
                        Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").ReadOnly = True
                    End If

                    'Format into decimal
                    If IsNumeric(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value) Then
                        Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value = CDec(Me.DGridView.Rows(e.RowIndex).Cells("colDrawdown").Value)
                    End If

            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_CollectionAllocationWindow.ToString(), ex.Message, "DGridView_CellFormatting event", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInRetrieving.ToString(), 'LDAPModule.LDAP.Username)

        End Try
    End Sub

    Private Sub DGridView_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGridView.CurrentCellDirtyStateChanged
        Try
            If Not Me.DGridView.IsCurrentCellDirty Then
                Exit Sub
            End If

            Dim ChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType), _
                                               CStr(Me.DGridView.CurrentRow.Cells("colChargeType").Value)), EnumChargeType)

            Select Case Me.DGridView.CurrentCell.ColumnIndex

                Case 13 'For Default Interest
                    Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                    'Reset the datagrid row
                    Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)

                    With Me.DGridView.CurrentRow
                        If IsNumeric(.Cells("colDefaultInterest").Value) Then
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            'When editing the default interest, the value must be zero
                            If defaultInterest = 0 Then
                                'Set also the default interest in withholding into zero
                                .Cells("colDefaultWithhold").Value = 0
                            Else
                                'Set the original default interest in withholding
                                .Cells("colDefaultWithhold").Value = .Cells("colDefaultWithholdOrig").Value
                            End If

                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End If
                    End With

                Case 14 'For Withholding Tax
                    Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                    'Reset the datagrid row
                    Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)

                    With Me.DGridView.CurrentRow
                        If IsNumeric(.Cells("colTaxOrig").Value) Then
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)

                            'When editing the default interest, the value must be zero
                            If withholdTax > 0 Then
                                'Set also the default interest in withholding into zero
                                .Cells("colDefaultWithhold").Value = 0
                            End If

                            Dim cellCheckBox As DataGridViewCheckBoxCell = CType(.Cells("colChckPay"), DataGridViewCheckBoxCell)
                            cellCheckBox.Value = False

                            If ChargeType = EnumChargeType.MF Or ChargeType = EnumChargeType.MFV Then
                                Dim PaidTo As String = CStr(.Cells("colIDNumber").Value)
                                Dim PaidInvoice As String = CStr(.Cells("colInvDMCMNoText").Value)

                                'Get the WESM Bill Summary for collection
                                Dim items = WBillHelper.GetWESMBillSummaryForCollectionAllocation(PaidTo, PaidInvoice)

                                Dim getMFItems = (From x In items Where x.ChargeType = EnumChargeType.MF).ToList() 'check if no Market Fees base amount given in the list.
                                If getMFItems.Count = 0 Then
                                    MessageBox.Show("No base amount of Market Fees in selected Invoice! You can't alter the amount of withholding tax.", "Warning Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    .Cells("colTaxOrig").Value = 0

                                    'select next column to remove the focus of cell selected
                                    Me.DGridView.CurrentCell = Me.DGridView.Rows(Me.DGridView.CurrentRow.Index).Cells(Me.DGridView.CurrentCell.ColumnIndex + 1)
                                    Me.DGridView.CurrentCell = Me.DGridView.Rows(Me.DGridView.CurrentRow.Index).Cells(Me.DGridView.CurrentCell.ColumnIndex)

                                    Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                                    withholdTax = CDec(.Cells("colTaxOrig").Value)
                                    Dim withholdVat = 0
                                    Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                                    Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                                    Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                                  defaultWithhold + defaultInterest
                                    .Cells("colTotalPayable").Value = totalPayable
                                    .Cells("colRemainingBalance").Value = totalPayable
                                Else
                                    Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                                    withholdTax = CDec(.Cells("colTaxOrig").Value)
                                    Dim withholdVat = CDec(.Cells("colVatOrig").Value)
                                    Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                                    Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                                    Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                                  defaultWithhold + defaultInterest

                                    .Cells("colTotalPayable").Value = totalPayable
                                    .Cells("colRemainingBalance").Value = totalPayable
                                End If
                            End If

                            'Compute the remaining cash, prudential and applied amount
                            Me.ComputeRemainingCash()
                            Me.ComputeRemainingPrudential()
                            Me.ComputeTotalAppliedAmount()
                        End If
                    End With

                Case 15 'For Withholding VAT
                    Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                    'Reset the datagrid row
                    Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)

                    With Me.DGridView.CurrentRow
                        If IsNumeric(.Cells("colVatOrig").Value) Then
                            Dim withholdVat = CDec(.Cells("colVatOrig").Value)

                            'When editing the default interest, the value must be zero
                            If withholdVat > 0 Then
                                'Set also the default interest in withholding into zero
                                .Cells("colVatOrig").Value = 0
                            End If

                            Dim endingBalance = CDec(.Cells("colEndingBalance").Value)
                            Dim withholdTax = CDec(.Cells("colTaxOrig").Value)
                            withholdVat = CDec(.Cells("colVatOrig").Value)
                            Dim defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                            Dim defaultInterest = CDec(.Cells("colDefaultInterest").Value)

                            Dim totalPayable As Decimal = endingBalance + withholdTax + withholdVat + _
                                                          defaultWithhold + defaultInterest

                            .Cells("colTotalPayable").Value = totalPayable
                            .Cells("colRemainingBalance").Value = totalPayable
                        End If
                    End With

                Case 18 'For Cash Column

                    Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                    With Me.DGridView.CurrentRow
                        If IsNumeric(.Cells("colCash").Value) Then
                            If CDec(.Cells("colCash").Value) > 0 Then

                                Select Case ChargeType
                                    Case EnumChargeType.MF
                                        Me.MarketFeesApplication()

                                    Case EnumChargeType.E, EnumChargeType.EV
                                        Me.CashEnergyAndVatOnEnergyApplication()

                                End Select
                            Else
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                            End If
                        Else
                            Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                        End If
                    End With

                    'Compute the remaining cash, prudential and applied amount
                    Me.ComputeRemainingCash()
                    Me.ComputeRemainingPrudential()
                    Me.ComputeTotalAppliedAmount()

                    Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                Case 19 'For Check Fully Paid
                    If Me.LoadType = CollectionLoadType.Add And Me.rbManual.Checked = True And ChargeType = EnumChargeType.E Then
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                        With Me.DGridView.CurrentRow

                            'Enable/Disable AmoutToPay column
                            If CBool(.Cells("colChckPay").Value) = False Then
                                .Cells("colDrawdown").ReadOnly = False

                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                            Else
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)

                                'Compute the amount allocated
                                Dim subTotal As Decimal = 0
                                For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                                    If i <> .Index Then
                                        subTotal += CDec(Me.DGridView.Rows(i).Cells("colDrawdown").Value)
                                    End If
                                Next

                                Dim AmountToAllocate As Decimal = CDec(Me.txtPrudentialAmount.Text) - subTotal

                                'Check if the remaining amount is enough to pay the Total Payable
                                If AmountToAllocate < Math.Abs(CDec(.Cells("colTotalPayable").Value)) Then
                                    Dim defaultInterest As Decimal, defaultWithhold As Decimal

                                    defaultInterest = CDec(.Cells("colDefaultInterest").Value)
                                    defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)

                                    'Check if the remaining amount is enough for default interest
                                    If AmountToAllocate < defaultInterest + defaultWithhold Then
                                        Exit Sub
                                    End If
                                Else
                                    AmountToAllocate = Math.Abs(CDec(.Cells("colTotalPayable").Value))
                                End If
                                .Cells("colDrawdown").ReadOnly = True
                                .Cells("colDrawdown").Value = AmountToAllocate
                                Select Case ChargeType
                                    Case EnumChargeType.E
                                        Me.DrawdownEnergy()
                                End Select
                            End If
                            'Compute the remaining prudential
                            Me.ComputeRemainingPrudential()
                            Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        End With

                    ElseIf Me.LoadType = CollectionLoadType.Edit And Me.rbManual.Checked = True Then
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                        With Me.DGridView.CurrentRow

                            'Enable/Disable AmoutToPay column
                            If CBool(.Cells("colChckPay").Value) = False Then
                                .Cells("colCash").ReadOnly = False

                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                            Else
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)

                                'Compute the amount allocated
                                Dim subTotal As Decimal = 0
                                For i As Integer = 0 To Me.DGridView.Rows.Count - 1
                                    If i <> .Index Then
                                        subTotal += CDec(Me.DGridView.Rows(i).Cells("colCash").Value)
                                    End If
                                Next

                                Dim AmountToAllocate As Decimal = CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) -
                                                                  CDec(Me.txtPrudentialReplenishment.Text) - subTotal

                                'Check if the remaining amount is enough to pay the Total Payable
                                If AmountToAllocate < Math.Abs(CDec(.Cells("colTotalPayable").Value)) Then
                                    Dim defaultInterest As Decimal, defaultWithhold As Decimal

                                    defaultInterest = CDec(.Cells("colDefaultInterest").Value)
                                    defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)

                                    'Check if the remaining amount is enough for default interest
                                    Select Case ChargeType
                                        Case EnumChargeType.MF
                                            If AmountToAllocate < defaultInterest + defaultWithhold Then
                                                Exit Sub
                                            End If
                                        Case EnumChargeType.E
                                            'Old
                                            'If AmountToAllocate + CDec(Me.txtRemainingPrudential.Text) < defaultInterest + defaultWithhold Then
                                            '    Exit Sub
                                            'End If
                                            'New - Removed the PR Amount by LAVV as of June 06, 2022
                                            If AmountToAllocate < defaultInterest + defaultWithhold Then
                                                Exit Sub
                                            End If
                                    End Select


                                Else
                                    AmountToAllocate = Math.Abs(CDec(.Cells("colTotalPayable").Value))
                                End If

                                .Cells("colCash").ReadOnly = True
                                .Cells("colCash").Value = AmountToAllocate

                                Select Case ChargeType
                                    Case EnumChargeType.MF
                                        Me.MarketFeesApplication()

                                    Case EnumChargeType.E, EnumChargeType.EV
                                        Me.CashEnergyAndVatOnEnergyApplication()

                                End Select
                            End If

                            'Compute the remaining cash, prudential and applied amount
                            Me.ComputeRemainingCash()
                            Me.ComputeRemainingPrudential()
                            Me.ComputeTotalAppliedAmount()
                        End With
                    End If
                Case 20 'For Drawdown Column
                    If Me.DGridView.CurrentCell.ColumnIndex = 20 Then
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                        With Me.DGridView.CurrentRow
                            If IsNumeric(.Cells("colDrawdown").Value) Then
                                If CDec(.Cells("colDrawdown").Value) > 0 Then
                                    Me.DrawdownEnergy()
                                Else
                                    .Cells("colDrawdown").Value = 0
                                    .Cells("colDrawdownEnergy").Value = 0
                                    .Cells("colDrawdownDefaultEnergy").Value = 0
                                End If
                            Else
                                .Cells("colDrawdown").Value = 0
                                .Cells("colDrawdownEnergy").Value = 0
                                .Cells("colDrawdownDefaultEnergy").Value = 0
                            End If
                        End With

                        'Compute the remaining prudential
                        Me.ComputeRemainingPrudential()
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub chckParticipantList_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) _
                                              Handles chckParticipantList.ItemCheck

        Dim PaidTo As String = CType(Me.chckParticipantList.Items.Item(e.Index), AMParticipants).IDNumber

        If e.NewValue = CheckState.Checked Then
            'Get the WESM Bill Summary for collection
            Dim items = WBillHelper.GetWESMBillSummaryForCollectionAllocation(PaidTo)
            Dim getListofInvoiceForBIRRuling As List(Of String) = items.Where(Function(c) c.INVDMCMNo.StartsWith("TS-W")).OrderBy(Function(z) z.INVDMCMNo).Select(Function(x) x.INVDMCMNo).Distinct.ToList

            If getListofInvoiceForBIRRuling.Count <> 0 Then
                Dim transItems = WBillHelper.GetListWESMTransCoverSummary2(getListofInvoiceForBIRRuling)
                Me.LoadWESMBillSummary(items, transItems)
            Else
                Dim transItems As List(Of WESMBillAllocCoverSummary) = New List(Of WESMBillAllocCoverSummary)
                Me.LoadWESMBillSummary(items, transItems)
            End If
            Me.ListWESMBillSummaries.AddRange(items)
        Else
            Dim chck As Boolean = True
            While chck = True
                Dim cnt = Me.DGridView.RowCount - 1
                For index As Integer = 0 To Me.DGridView.RowCount - 1
                    If CStr(Me.DGridView.Rows(index).Cells("colIDNumber").Value) = PaidTo Then
                        Me.DGridView.Rows.RemoveAt(index)

                        chck = True
                        Exit For
                    End If

                    If index = cnt Then
                        chck = False
                    End If
                Next

                If Me.DGridView.RowCount = 0 Then
                    chck = False
                End If
            End While

            For index As Integer = Me.ListWESMBillSummaries.Count - 1 To 0 Step -1
                If Me.ListWESMBillSummaries(index).IDNumber.IDNumber = PaidTo Then
                    Me.ListWESMBillSummaries.RemoveAt(index)
                End If
            Next
        End If

        Me.ResetGrid()
        Me.txtRemainingPrudential.Text = Me.txtPrudentialAmount.Text
        Me.txtRemainingCash.Text = FormatNumber(CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) - CDec(Me.txtPrudentialReplenishment.Text), 2)

        Me.ComputeTotals()
    End Sub

    Private Sub txtForTheAcoountOf_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtForTheAcountOf.GotFocus
        Me.panelHeight = 0
        Me.PanelParticipants.Size = New Size(Me.PanelParticipants.Size.Width, panelHeight)
        Me.PanelParticipants.Visible = True
        Me.Timer1.Enabled = True
    End Sub

    Private Sub linkClose_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) _
                                      Handles linkClose.LinkClicked

        Dim participants As String = ""
        For index As Integer = 0 To Me.chckParticipantList.Items.Count - 1
            If Me.chckParticipantList.GetItemChecked(index) Then
                If participants.Length = 0 Then
                    participants = CType(Me.chckParticipantList.Items.Item(index), AMParticipants).ParticipantID
                Else
                    participants = participants & " ," & CType(Me.chckParticipantList.Items.Item(index), AMParticipants).ParticipantID
                End If
            End If
        Next

        Me.txtForTheAcountOf.Text = participants
        Me.PanelParticipants.Visible = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        panelHeight += 10
        Me.PanelParticipants.Size = New Size(Me.PanelParticipants.Size.Width, panelHeight)

        If Me.PanelParticipants.Size.Height = 260 Or Me.PanelParticipants.Size.Height > 260 Then
            Me.PanelParticipants.Size = New Size(Me.PanelParticipants.Size.Width, 269)
            Me.Timer1.Enabled = False
        End If
    End Sub

    Private Sub ddlParticipantID_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.ddlParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim chck As Boolean = True
        While chck = True
            Dim cnt = Me.DGridView.RowCount - 1
            For index As Integer = 0 To Me.DGridView.RowCount - 1
                If CStr(Me.DGridView.Rows(index).Cells("colIDNumber").Value) = Me.SelectedIDNumber.IDNumber Then
                    Me.DGridView.Rows.RemoveAt(index)

                    chck = True
                    Exit For
                End If

                If index = cnt Then
                    chck = False
                End If
            Next

            If Me.DGridView.RowCount = 0 Then
                chck = False
            End If
        End While

        'Clear the summaries of the current selected participant
        For index As Integer = Me.ListWESMBillSummaries.Count - 1 To 0 Step -1
            If Me.ListWESMBillSummaries(index).IDNumber.IDNumber = Me.SelectedIDNumber.IDNumber Then
                Me.ListWESMBillSummaries.RemoveAt(index)
            End If
        Next

        'Uncheck the current selected participant in check box
        For index As Integer = 0 To Me.chckParticipantList.Items.Count - 1
            If Me.SelectedIDNumber.IDNumber = CType(Me.chckParticipantList.Items(index), AMParticipants).IDNumber Then
                Me.chckParticipantList.SetItemChecked(index, False)
            End If
        Next

        'Set the full name of the selected participants
        Me.SelectedIDNumber = WBillHelper.GetAMParticipants(CStr(Me.ddlParticipantID.SelectedValue)).First()

        Me.txtParticipantName.Text = SelectedIDNumber.FullName
        Me.txtIDNumber.Text = SelectedIDNumber.IDNumber.ToString()

        'Check the list of participants where it's equal to participant selected
        For index As Integer = 0 To Me.chckParticipantList.Items.Count - 1           
            If Me.SelectedIDNumber.IDNumber = CType(Me.chckParticipantList.Items(index), AMParticipants).IDNumber Then
                Me.chckParticipantList.SetItemChecked(index, True)
                Exit For
            End If
        Next

        Dim participants As String = ""
        For index As Integer = 0 To Me.chckParticipantList.Items.Count - 1
            If Me.chckParticipantList.GetItemChecked(index) Then
                If participants.Length = 0 Then
                    participants = CType(Me.chckParticipantList.Items.Item(index), AMParticipants).ParticipantID
                Else
                    participants = participants & " ," & CType(Me.chckParticipantList.Items.Item(index), AMParticipants).ParticipantID
                End If
            End If
        Next

        Me.txtForTheAcountOf.Text = participants

        'Get the prudential
        Dim prudential = WBillHelper.GetParticipantPrudential(Me.SelectedIDNumber.IDNumber).PrudentialAmount

        Me.txtPrudentialAmount.Text = FormatNumber(prudential, 2)
        Me.txtRemainingPrudential.Text = FormatNumber(prudential, 2)

        'Get the held
        Me.ListHeldCollection = WBillHelper.GetCollectionMonitoringPerParticipant(Me.SelectedIDNumber.IDNumber, _
                                                                                  EnumCollectionMonitoringType.TransferToHeldCollection)
        Dim held As Decimal = 0

        If ListHeldCollection.Count > 0 Then
            Me.itemCollectionMonitoring = ListHeldCollection.First
            held = Me.itemCollectionMonitoring.Amount
        End If
        Me.txtHeldPayment.Text = FormatNumber(held, 2)

        'Compute for the totals
        Me.ComputeTotals()

        'Compute the Remaining Cash
        Me.ComputeRemainingCash()
    End Sub

    Private Sub txtPrudentialReplenishment_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrudentialReplenishment.LostFocus
        If Not IsNumeric(Me.txtPrudentialReplenishment.Text) Then
            MsgBox("Prudential replenishment must be numeric!", MsgBoxStyle.Exclamation, "Warning")
            Me.txtPrudentialReplenishment.Text = "0.00"
            Me.txtPrudentialReplenishment.Select()
            Exit Sub
        ElseIf Not IsNumeric(Me.txtAmountCollected.Text) Then
            'MsgBox("Collected amount must be numeric!", MsgBoxStyle.Critical, "Warning")
            'Me.txtAmountCollected.Text = "0.00"
            Exit Sub
        ElseIf CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtPrudentialReplenishment.Text) <> 0 Then
            MsgBox("Please specify first the collected amount!", MsgBoxStyle.Exclamation, "Warning")
            Me.txtPrudentialReplenishment.Text = "0.00"
            Me.txtAmountCollected.Select()
            Exit Sub
        ElseIf CDec(Me.txtAmountCollected.Text) < CDec(Me.txtPrudentialReplenishment.Text) Then
            MsgBox("Collected amount must be greater than prudential replenishment!", MsgBoxStyle.Exclamation, "Warning")
            Me.txtPrudentialReplenishment.Text = "0.00"
            Me.txtPrudentialReplenishment.Select()
            Exit Sub
        ElseIf CDec(Me.txtPrudentialReplenishment.Text) < 0 Then
            MsgBox("Prudential replenishment must not be negative!", MsgBoxStyle.Exclamation, "Warning")
            Me.txtPrudentialReplenishment.Text = "0.00"
            Me.txtPrudentialReplenishment.Select()
            Exit Sub
        End If

        Me.txtPrudentialReplenishment.Text = FormatNumber(Me.txtPrudentialReplenishment.Text, 2)
    End Sub

    Private Sub txtPrudentialReplenishment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrudentialReplenishment.TextChanged
        Try
            If Me.rbManual.Checked Then
                Me.ResetGrid()
                Me.ComputeTotals()
                Me.ComputeRemainingCash()
                Me.ComputeRemainingPrudential()
                Me.ComputeTotalAppliedAmount()
            End If

            Me.txtRemainingPrudential.Text = Me.txtPrudentialAmount.Text
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


#Region "Methods/Functions"

    Private Function FormValidation() As Boolean

        Try
            If Me.ddlParticipantID.SelectedIndex = -1 Then
                MsgBox("Please select the participant!", MsgBoxStyle.Critical, "Specify the inputs")
                Me.ddlParticipantID.Select()
                Exit Function
            ElseIf Not IsNumeric(Me.txtAmountCollected.Text) Then
                MsgBox("Collected amount must be numeric!", MsgBoxStyle.Critical, "Error")
                Me.txtAmountCollected.Select()
                Exit Function
            ElseIf CDec(Me.txtAmountCollected.Text) > 100000000000 Then
                MsgBox("Collected amount must not be more than 100,000,000,000.00!", MsgBoxStyle.Critical, "Error")
                Me.txtAmountCollected.Select()
                Exit Function
            ElseIf CDec(Me.txtAmountCollected.Text) < 0 Then
                MsgBox("Collected amount must not be negative!", MsgBoxStyle.Critical, "Error")
                Me.txtAmountCollected.Select()
                Exit Function
            ElseIf Me.rbAutomatic.Checked And CDec(Me.txtAmountCollected.Text) = 0 Then
                MsgBox("Please specify the collected amount!", MsgBoxStyle.Critical, "Specify the inputs")
                Me.txtAmountCollected.Select()
                Exit Function
            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtPrudentialAmount.Text) = 0 _
                And CDec(Me.txtHeldPayment.Text) = 0 Then

                MsgBox("Please specify the collected amount!", MsgBoxStyle.Critical, "Specify the inputs")
                Me.txtAmountCollected.Select()
                Exit Function
            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtPrudentialAmount.Text) <> 0 _
                And Me.DGridView.RowCount = 0 Then
                MsgBox("No oustanding balance to be paid!", MsgBoxStyle.Critical, "Specify the inputs")
                Me.txtAmountCollected.Select()
                Exit Function

            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtHeldPayment.Text) <> 0 _
                And CDec(Me.txtPrudentialAmount.Text) = 0 Then

                Dim chck As Boolean = False
                For index As Integer = 0 To Me.DGridView.RowCount - 1
                    With Me.DGridView.Rows(index)
                        If CDec(.Cells("colCash").Value) <> 0 Then
                            chck = True
                            Exit For
                        End If
                    End With
                Next

                If chck = False Then
                    MsgBox("Please select the invoice(s) to be paid!", MsgBoxStyle.Critical, "Specify the inputs")
                    Exit Function
                End If

            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtHeldPayment.Text) <> 0 _
                And CDec(Me.txtPrudentialAmount.Text) <> 0 Then

                Dim chck As Boolean = False
                For index As Integer = 0 To Me.DGridView.RowCount - 1
                    With Me.DGridView.Rows(index)
                        If CDec(.Cells("colDrawdown").Value) <> 0 Or CDec(.Cells("colCash").Value) <> 0 Then
                            chck = True
                            Exit For
                        End If
                    End With
                Next

                If chck = False Then
                    MsgBox("Please select the invoice(s) to be paid!", MsgBoxStyle.Critical, "Specify the inputs")
                    Exit Function
                End If

            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtPrudentialAmount.Text) <> 0 Then
                Dim chck As Boolean = False
                For index As Integer = 0 To Me.DGridView.RowCount - 1
                    With Me.DGridView.Rows(index)
                        If CDec(.Cells("colDrawdown").Value) <> 0 Or CDec(.Cells("colCash").Value) <> 0 Then
                            chck = True
                            Exit For
                        End If

                    End With
                Next

                If chck = False Then
                    MsgBox("Please select the invoice(s) to be paid!", MsgBoxStyle.Critical, "Specify the inputs")
                    Exit Function
                End If

            End If

            Dim selectedDay As Integer = Microsoft.VisualBasic.DateAndTime.Day(CDate(Me.DTCollection.Value.ToString("MM/dd/yyyy")))
            Dim thisDay As Integer = Microsoft.VisualBasic.DateAndTime.Day(CDate(SystemDate.ToString("MM/dd/yyyy")))
            Dim ans As MsgBoxResult

            If selectedDay >= 25 And selectedDay <= 31 And CDate(Me.DTCollection.Value.ToString("MM/dd/yyyy")) <> CDate(SystemDate.ToString("MM/dd/yyyy")) Then
                ans = MsgBox("Do you really want to apply this as advanced collecion?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

                If ans = MsgBoxResult.No Then
                    Exit Function
                End If
            ElseIf selectedDay < 25 And CDate(Me.DTCollection.Value.ToString("MM/dd/yyyy")) > CDate(SystemDate.ToString("MM/dd/yyyy")) Then
                MsgBox("Collection date must not be later than date today!", MsgBoxStyle.Critical, "Invalid")
                Exit Function
            End If

            'If CDate(Me.DTCollection.Value.ToString("MM/dd/yyyy")) > CDate(SystemDate.ToString("MM/dd/yyyy")) Then
            '    MsgBox("Collection date must not be later than date today!", MsgBoxStyle.Critical, "Invalid")
            '    Exit Function
            'End If
            If Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) <> 0 And Me.LoadType = CollectionLoadType.Edit Then
                Dim totalAmountApplied As Decimal = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    totalAmountApplied += CDec(Me.DGridView.Rows(i).Cells("colCash").Value)
                Next
                If Math.Abs(totalAmountApplied) > 0 Then
                    If CDec(txtAmountCollected.Text) < Math.Abs(totalAmountApplied) Then
                        MessageBox.Show("Total Applied Amount is greater than the amount collected! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.cb_CheckAll.Checked = False
                        Exit Function
                    End If
                End If
            ElseIf Me.rbManual.Checked And CDec(Me.txtAmountCollected.Text) = 0 And CDec(Me.txtPrudentialAmount.Text) <> 0 And Not Me.LoadType = CollectionLoadType.Edit Then
                Dim totalPRDrawn As Decimal = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    totalPRDrawn += CDec(Me.DGridView.Rows(i).Cells("colDrawdown").Value)
                Next
                If Math.Abs(totalPRDrawn) > 0 Then
                    If CDec(txtPrudentialAmount.Text) < Math.Abs(totalPRDrawn) Then
                        MessageBox.Show("Total Applied PR is greater than the Prudential! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.cb_CheckAll.Checked = False
                        Exit Function
                    End If
                End If
            End If
        Catch ex1 As OverflowException
            MsgBox("Invalid Amount", MsgBoxStyle.Critical, "Error")
            Call ResetGrid()
            Call ComputeRemainingCash()
            Call ComputeRemainingPrudential()
            Exit Function
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Function
        End Try

        Return True
    End Function

    Private Sub ClearInputs()
        Me.ddlParticipantID.SelectedIndex = -1
        Me.txtIDNumber.Text = ""
        Me.txtParticipantName.Text = ""
        Me.DGridView.Rows.Clear()
        Me.rbAutomatic.Select()
        Me.ddlParticipantID.Select()
        Me.txtAmountCollected.Text = "0.00"
        Me.txtPrudentialAmount.Text = "0.00"
        Me.txtRemainingPrudential.Text = "0.00"
        Me.txtHeldPayment.Text = "0.00"
        Me.txtRemainingCash.Text = "0.00"
        Me.txtPrudentialReplenishment.Text = "0.00"
        Me.txtTotalAllocatedAmount.Text = "0.00"
        Me.txtTotalOutstandingBalance.Text = "0.00"
        Me.txtForTheAcountOf.Text = ""
        Me.cb_CheckAll.Checked = False
        checkAllBox = False
        For Index As Integer = 0 To Me.chckParticipantList.Items.Count - 1
            Me.chckParticipantList.SetItemChecked(index, False)
        Next

        Me.ListWESMBillSummaries.Clear()
    End Sub

    Private Sub LoadWESMBillSummary(ByVal listSummary As List(Of WESMBillSummary), ByVal listWESMAllocCoverSummary As List(Of WESMBillAllocCoverSummary))
        Dim interestRate As Decimal
        Dim InvDMCM As String = ""
        Dim CollectionDate As Date = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

        If Me._dicInterestRate.ContainsKey(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))) Then
            interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))
        End If


        Dim getLastDayOfMonth As Date = AMModule.SystemDate.AddDays(-(AMModule.SystemDate.Day - 1)).AddMonths(1).AddDays(-1)

        'Get the list of Energy and VAT on Energy
        Dim listEnergyAndVAT = From x In listSummary Join y In Me.Participants
                               On x.IDNumber.IDNumber Equals y.IDNumber
                               Where (x.ChargeType = EnumChargeType.E Or x.ChargeType = EnumChargeType.EV) _
                               And (x.DueDate <= getLastDayOfMonth)
                               Select x, y.ParticipantID

        'Get the list of Market Fees and VAT on Market Fees
        Dim listMFandMFV = (From x In listSummary
                            Where x.ChargeType = EnumChargeType.MF Or x.ChargeType = EnumChargeType.MFV
                            Group x By x.BillPeriod, x.IDNumber.IDNumber, x.INVDMCMNo, x.SummaryType,
                           x.DueDate, x.NewDueDate
                           Into
                           Amount = Sum(x.EndingBalance)
                            Select New With {.BillPeriod = BillPeriod, .IDNumber = IDNumber,
                                            .INVDMCMNo = INVDMCMNo, .SummaryType = SummaryType,
                                            .DueDate = DueDate, .NewDueDate = NewDueDate}).ToList

        'Dispay in the grid the Energy and VAT on Market Fees        
        For Each item In listEnergyAndVAT
            Dim defaultInterestAmount As Decimal = 0D
            Dim totalPayable As Decimal = 0D
            Dim chargeType As String = ""
            Dim EnergyWithhold As Decimal = 0D
            Dim computedEndingBalance As Decimal = 0D
            Dim getEWT As WESMBillAllocCoverSummary = (From z In listWESMAllocCoverSummary Where z.TransactionNo = item.x.INVDMCMNo Select z).FirstOrDefault
            With item
                Select Case .x.ChargeType
                    Case EnumChargeType.E

                        chargeType = "Energy"
                        computedEndingBalance = Math.Abs(.x.EndingBalance - .x.EnergyWithhold)

                        If item.x.NoDefInt = True Then
                            defaultInterestAmount = 0
                        Else
                            defaultInterestAmount = Math.Round(BFactory.ComputeDefaultInterest(.x.DueDate, .x.NewDueDate,
                                                                                               CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                                                               computedEndingBalance, interestRate), 2)
                        End If

                        EnergyWithhold = .x.EnergyWithhold
                        If .x.EndingBalance <> 0 Then
                            totalPayable = Math.Abs(.x.EndingBalance) + defaultInterestAmount + EnergyWithhold
                        Else
                            totalPayable = 0
                        End If

                        'Format the Invoice Number and DMCM
                        If .x.SummaryType = EnumSummaryType.INV Or .x.SummaryType = EnumSummaryType.SPA Or .x.INVDMCMNo.Length > 15 Then
                            InvDMCM = .x.INVDMCMNo
                        Else
                            InvDMCM = BFactory.GenerateBIRDocumentNumber(CLng(.x.INVDMCMNo), BIRDocumentsType.DMCM)
                        End If
                    Case EnumChargeType.EV
                        chargeType = "VatOnEnergy"
                        EnergyWithhold = 0D
                        totalPayable = Math.Abs(.x.EndingBalance)

                        'Format the Invoice Number and DMCM
                        If .x.SummaryType = EnumSummaryType.INV Or .x.SummaryType = EnumSummaryType.SPA Or .x.INVDMCMNo.Length > 15 Then
                            InvDMCM = .x.INVDMCMNo
                        Else
                            InvDMCM = BFactory.GenerateBIRDocumentNumber(CLng(.x.INVDMCMNo), BIRDocumentsType.DMCM)
                        End If
                End Select
                Me.DGridView.Rows.Add(.x.WESMBillSummaryNo, 0, .x.BillPeriod, .x.IDNumber.IDNumber, .x.IDNumber.ParticipantID,
                                     .x.SummaryType.ToString(), .x.INVDMCMNo, InvDMCM,
                                     .x.ChargeType.ToString(), chargeType, .x.DueDate.ToString("MM/dd/yyyy"),
                                     .x.NewDueDate.ToString("MM/dd/yyyy"), Math.Abs(.x.EndingBalance), defaultInterestAmount,
                                     CDec(IIf(.x.ChargeType = EnumChargeType.E, .x.EnergyWithhold, 0D)),
                                     CDec(IIf(.x.ChargeType = EnumChargeType.EV, .x.EnergyWithhold, 0D)), 0D,
                                     totalPayable, 0D, CheckState.Unchecked, 0D, totalPayable, defaultInterestAmount, 0D, 0D, 0D, 0D, 0D,
                                     CDec(IIf(.x.ChargeType = EnumChargeType.E, .x.EnergyWithhold, 0D)),
                                     CDec(IIf(.x.ChargeType = EnumChargeType.EV, .x.EnergyWithhold, 0D)),
                                     0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D)
            End With
        Next

        For Each item In listMFandMFV
            Dim totalPayable As Decimal = 0
            Dim selectedItem = item

            Dim listSelectedItems = From x In listSummary Join y In Me.Participants
                                    On x.IDNumber.IDNumber Equals y.IDNumber
                                    Where x.INVDMCMNo = selectedItem.INVDMCMNo _
                                    And x.SummaryType = selectedItem.SummaryType
                                    Select x, y.ParticipantID, y.MarketFeesWHTax, y.MarketFeesWHVAT

            Dim WBSummaryNo As Long = 0, WBSummaryNo1 As Long = 0
            Dim WithholdMF As Decimal = 0, WithholdMFV As Decimal = 0
            Dim DefaultWithholdMF As Decimal = 0, DefaultWithholdMFV As Decimal = 0
            Dim MFValue As Decimal = 0, MFVValue As Decimal = 0
            Dim DefaultMF As Decimal = 0, DefaultMFV As Decimal = 0

            'Aggregate first MF and MFV but it must display as one item in datagrid
            For Each itemMF In listSelectedItems
                With itemMF
                    If .x.ChargeType = EnumChargeType.MF Then
                        WBSummaryNo = .x.WESMBillSummaryNo
                        MFValue = Math.Abs(.x.EndingBalance)
                        If .x.NoDefInt = True Then
                            DefaultMF = 0
                        Else
                            DefaultMF = Math.Round(BFactory.ComputeDefaultInterest(.x.DueDate, .x.NewDueDate,
                                                                               CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                                               Math.Abs(.x.EndingBalance), interestRate), 2)
                        End If

                        If .MarketFeesWHTax <> 0 Then
                            WithholdMF = Math.Round(.x.EndingBalance * .MarketFeesWHTax, 2)
                            DefaultWithholdMF = Math.Round(BFactory.ComputeDefaultInterest(.x.DueDate, .x.NewDueDate,
                                                           CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                           WithholdMF, interestRate), 2)

                        ElseIf .MarketFeesWHVAT <> 0 Then
                            'added by lance -- 11/04/2019
                            WithholdMFV = Math.Round(.x.EndingBalance * .MarketFeesWHVAT, 2)
                        End If

                    Else
                        WBSummaryNo1 = .x.WESMBillSummaryNo
                        MFVValue = Math.Abs(.x.EndingBalance)

                        'No default interest related to VAT. Edited by Vloody --- 10/23/2017
                        'DefaultMFV = Math.Round(BFactory.ComputeDefaultInterest(.x.DueDate, .x.NewDueDate, _
                        '                                                       CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), _
                        '                                                       Math.Abs(.x.EndingBalance), interestRate), 2)
                        'If .VatOnMarketFeesTax <> 0 Then
                        '    WithholdMFV = Math.Round(.x.EndingBalance * (.VatOnMarketFeesTax / AMModule.VatValue), 2)

                        '    '    DefaultWithholdMFV = Math.Round(BFactory.ComputeDefaultInterest(.x.DueDate, .x.NewDueDate, _
                        '    '                                              CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), _
                        '    '                                              WithholdMFV, interestRate), 2)
                        'End If
                    End If
                End With
            Next

            'Compute the total payable
            totalPayable = (MFValue + MFVValue + WithholdMF + WithholdMFV + DefaultMF + DefaultMFV + DefaultWithholdMF + DefaultWithholdMFV)

            'Format the Invoice Number and DMCM
            If item.SummaryType = EnumSummaryType.INV Then
                InvDMCM = item.INVDMCMNo
            Else
                InvDMCM = BFactory.GenerateBIRDocumentNumber(CLng(item.INVDMCMNo), BIRDocumentsType.DMCM)
            End If

            'Make MF and MFv as one row
            Me.DGridView.Rows.Add(WBSummaryNo, WBSummaryNo1, item.BillPeriod, item.IDNumber, listSelectedItems.First.ParticipantID,
                                  item.SummaryType.ToString(), item.INVDMCMNo, InvDMCM,
                                  EnumChargeType.MF.ToString(), "MarketFees", item.DueDate.ToString("MM/dd/yyyy"),
                                  item.NewDueDate.ToString("MM/dd/yyyy"), MFValue + MFVValue, DefaultMF + DefaultMFV,
                                  WithholdMF, WithholdMFV, DefaultWithholdMF + DefaultWithholdMFV, totalPayable, 0D, CheckState.Unchecked,
                                  0D, totalPayable, DefaultMF + DefaultMFV, MFValue, MFVValue, DefaultWithholdMF + DefaultWithholdMFV, 0D, 0D, 0D, 0D,
                                  0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D, 0D)
        Next


    End Sub

    Private Function GetPaidAmount() As Decimal
        GetPaidAmount = 0D
        For i As Integer = 0 To Me.DGridView.Rows.Count - 1
            With Me.DGridView.Rows(i)
                GetPaidAmount += CDec(Me.DGridView.Rows(i).Cells("colAmountToPay").Value)
            End With
        Next
    End Function

    Private Sub ResetGrid()
        For index As Integer = 0 To Me.DGridView.RowCount - 1
            With Me.DGridView.Rows(index)
                .Cells("colDefaultInterest").Value = .Cells("colDefaultInterestOrig").Value
                .Cells("colRemainingBalance").Value = .Cells("colTotalPayable").Value
                .Cells("colCash").Value = 0D
                .Cells("colMF").Value = 0D
                .Cells("colMFV").Value = 0D
                .Cells("colTax").Value = 0D
                .Cells("colVat").Value = 0D
                .Cells("colChckPay").Value = False
                .Cells("colDefaultMF").Value = 0D
                .Cells("colDefaultMFV").Value = 0D
                .Cells("colDefaultWithholdMFTax").Value = 0D
                .Cells("colDefaultWithholdMFVat").Value = 0D
                .Cells("colDrawdown").Value = 0D
                .Cells("colCashEnergyAndVAT").Value = 0D
                .Cells("colCashDefaultEnergy").Value = 0D
                .Cells("colDrawdownEnergy").Value = 0D
                .Cells("colDrawdownDefaultEnergy").Value = 0D
            End With
        Next
    End Sub

    Private Sub ComputeTotals()
        Dim totalAllocatedAmount As Decimal = 0D
        Dim totalRemainingBalance As Decimal = 0D

        For index As Integer = 0 To Me.DGridView.RowCount - 1
            totalAllocatedAmount += CDec(Me.DGridView.Rows(index).Cells("colCash").Value)
            totalRemainingBalance += CDec(Me.DGridView.Rows(index).Cells("colRemainingBalance").Value)
            'totalRemainingBalance += CDec(Me.DGridView.Rows(index).Cells("colRemainingBalance").Value)
        Next

        Me.txtTotalAllocatedAmount.Text = FormatNumber(totalAllocatedAmount, 2)
        Me.txtTotalOutstandingBalance.Text = FormatNumber(totalRemainingBalance, 2)
    End Sub

    Private Sub ComputeRemainingCash()
        Dim subTotal As Decimal = 0

        For index As Integer = 0 To Me.DGridView.Rows.Count - 1
            If IsNumeric(Me.DGridView.Rows(index).Cells("colCash").Value) Then
                subTotal += CDec(Me.DGridView.Rows(index).Cells("colCash").Value)
            Else
                Me.txtRemainingCash.Text = "0.00"
                Exit Sub
            End If
        Next

        Dim remainingCash As Decimal = 0

        If IsNumeric(Me.txtAmountCollected.Text) And IsNumeric(Me.txtPrudentialReplenishment.Text) Then
            remainingCash = CDec(Me.txtHeldPayment.Text) + CDec(Me.txtAmountCollected.Text) - _
                                       CDec(Me.txtPrudentialReplenishment.Text) - subTotal
        End If

        If remainingCash > 0 Then
            Me.txtRemainingCash.Text = FormatNumber(remainingCash, 2)
        Else
            Me.txtRemainingCash.Text = "0.00"
        End If
    End Sub

    Private Sub ComputeRemainingPrudential()
        Dim subTotal As Decimal = 0

        For index As Integer = 0 To Me.DGridView.Rows.Count - 1
            If IsNumeric(Me.DGridView.Rows(index).Cells("colDrawdown").Value) Then
                subTotal += CDec(Me.DGridView.Rows(index).Cells("colDrawdown").Value)
            Else
                Me.txtRemainingPrudential.Text = "0.00"
                Exit Sub
            End If
        Next

        Dim remainingPrudential As Decimal = CDec(Me.txtPrudentialAmount.Text) - subTotal

        If remainingPrudential > 0 Then
            Me.txtRemainingPrudential.Text = FormatNumber(remainingPrudential, 2)
        Else
            Me.txtRemainingPrudential.Text = FormatNumber(remainingPrudential, 2)
        End If
    End Sub

    Private Sub ComputeTotalAppliedAmount()
        Dim subTotal As Decimal = 0

        For index As Integer = 0 To Me.DGridView.Rows.Count - 1
            If IsNumeric(Me.DGridView.Rows(index).Cells("colCash").Value) Then
                subTotal += CDec(Me.DGridView.Rows(index).Cells("colCash").Value)
            Else
                Me.txtTotalAllocatedAmount.Text = "0.00"
                Exit Sub
            End If
        Next

        Dim totalCash As Decimal = 0

        If IsNumeric(Me.txtAmountCollected.Text) And IsNumeric(Me.txtPrudentialReplenishment.Text) Then
            totalCash = CDec(Me.txtHeldPayment.Text) + CDec(Me.txtAmountCollected.Text) - _
                        CDec(Me.txtPrudentialReplenishment.Text)
        End If

        'Disabled by LAVV as requested for checking all 11/03/2023
        'If totalCash >= subTotal Then
        '    Me.txtTotalAllocatedAmount.Text = FormatNumber(subTotal, 2)
        'Else
        '    Me.txtTotalAllocatedAmount.Text = "0.00"
        'End If
        If totalCash >= subTotal Then
            Me.txtTotalAllocatedAmount.Text = FormatNumber(subTotal, 2)
        Else
            Me.txtTotalAllocatedAmount.Text = FormatNumber(totalCash - subTotal, 2)
        End If
    End Sub

    Private Function SaveManualCollection(ByVal IDNumber As String, ByRef listCollections As List(Of Collection), _
                                          ByVal allocationDateValue As Date) As Boolean
        Dim listColAllocationCash As New List(Of CollectionAllocation)
        Dim listColAllocationDrawdown As New List(Of CollectionAllocation)
        Dim listWESMBillSummaries As New List(Of WESMBillSummary)
        Dim listWesmBillSummariesClone As New List(Of WESMBillSummary)
        Dim dicWESMBillSummaries As New Dictionary(Of Long, WESMBillSummary)
        Dim itemCollection As Collection
        Dim PRCollectionNo As Long

        'Clone property ListWESMBillSummaries
        listWesmBillSummariesClone = CType(BFactory.CloneObject(Me.ListWESMBillSummaries), List(Of WESMBillSummary))

        itemCollection = listCollections.First()
        PRCollectionNo = itemCollection.CollectionNumber + 1

        For index As Integer = 0 To Me.DGridView.Rows.Count - 1
            If CDec(Me.DGridView.Rows(index).Cells("colCash").Value) = 0 And _
               CDec(Me.DGridView.Rows(index).Cells("colDrawdown").Value) = 0 Then

                Continue For
            End If

            Dim itemWESMBillSummary1 As New WESMBillSummary
            Dim itemWESMBillSummary2 As New WESMBillSummary

            Dim wbSummaryNo1 As Long = 0
            Dim wbSummaryNo2 As Long = 0
            Dim dueDate As Date = Nothing
            Dim newDueDate As Date = Nothing
            Dim defaultInterest As Decimal = 0
            Dim defaultWithhold As Decimal = 0
            Dim endingBalanceMF As Decimal = 0
            Dim endingBalanceMFV As Decimal = 0
            Dim endingBalanceEnergyVAT As Decimal = 0
            Dim defaultMF As Decimal = 0
            Dim defaultMFV As Decimal = 0
            Dim defaultWithholdTax As Decimal = 0
            Dim defaultWithholdVat As Decimal = 0
            Dim MFValue As Decimal = 0
            Dim MFVValue As Decimal = 0
            Dim WithholdTaxOnMF As Decimal = 0
            Dim WithholdVatOnMF As Decimal = 0
            Dim defaultCashEnergy As Decimal = 0
            Dim cashEnergyVAT As Decimal = 0
            Dim WithholdTaxOnEnergy As Decimal = 0
            Dim drawdownEnergy As Decimal = 0
            Dim defaultDrawdownEnergy As Decimal = 0
            Dim chargeType As EnumChargeType = Nothing
            Dim totalCashPerItem As Decimal = 0
            Dim totalDrawdownPerItem As Decimal = 0

            'Get the values of the variables
            With Me.DGridView.Rows(index)
                chargeType = CType(System.Enum.Parse(GetType(EnumChargeType), _
                                 CStr(.Cells("colChargeType").Value)), EnumChargeType)
                wbSummaryNo1 = CLng(.Cells("colWBSummaryNo1").Value)
                wbSummaryNo2 = CLng(.Cells("colWBSummaryNo2").Value)
                dueDate = CDate(.Cells("colDueDate").Value)
                newDueDate = CDate(.Cells("colNewDueDate").Value)
                totalCashPerItem = CDec(.Cells("colCash").Value)
                totalDrawdownPerItem = CDec(.Cells("colDrawdown").Value)
            End With

            Select Case chargeType
                Case EnumChargeType.MF
                    With Me.DGridView.Rows(index)
                        endingBalanceMF = CDec(.Cells("colMFOrig").Value)
                        endingBalanceMFV = CDec(.Cells("colMFVOrig").Value)
                        defaultMF = CDec(.Cells("colDefaultMF").Value)
                        defaultMFV = CDec(.Cells("colDefaultMFV").Value)
                        defaultWithholdTax = CDec(.Cells("colDefaultWithholdMFTax").Value)
                        defaultWithholdVat = CDec(.Cells("colDefaultWithholdMFVat").Value)
                        MFValue = CDec(.Cells("colMF").Value)
                        MFVValue = CDec(.Cells("colMFV").Value)
                        WithholdTaxOnMF = CDec(.Cells("colTax").Value)
                        WithholdVatOnMF = CDec(.Cells("colVat").Value)
                    End With

                    'Check if the applied amount is equal to the distributed amount
                    If totalCashPerItem + totalDrawdownPerItem <> defaultMF + defaultMFV + defaultWithholdTax + defaultWithholdVat + _
                                                                  MFValue + MFVValue + WithholdTaxOnMF + WithholdVatOnMF Then
                        Throw New ApplicationException("The allocated amount for " & CStr(Me.DGridView.Rows(index).Cells("colInvDMCMNoText").Value) & " " & _
                                                       "is not equal to the specified cash and drawdown amount!")

                    End If

                    'Get the WESM Bill Summary of Market Fees
                    If wbSummaryNo1 <> 0 Then
                        itemWESMBillSummary1 = (From x In listWesmBillSummariesClone _
                                                Where x.WESMBillSummaryNo = wbSummaryNo1 _
                                                Select x).First()

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.MarketFees.ToString() & "-Get " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If
                    End If

                    'Get the WESM Bill Summary of VAT on Market Fees
                    If wbSummaryNo2 <> 0 Then
                        itemWESMBillSummary2 = (From x In listWesmBillSummariesClone _
                                                Where x.WESMBillSummaryNo = wbSummaryNo2 _
                                                Select x).First()

                        If itemWESMBillSummary2.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.VatOnMarketFees.ToString() & "-Get " & itemWESMBillSummary2.WESMBillSummaryNo.ToString() & " " & _
                                                 itemWESMBillSummary2.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If
                    End If



                    'For Default Interest on Withholding Tax on Market Fees
                    If defaultWithholdTax <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultWithholdTax
                            .EndingBalance = defaultMF
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.WithholdingTaxOnDefaultInterest

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Default Interest on Market Fees
                    If defaultMF <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultMF
                            .EndingBalance = endingBalanceMF
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.DefaultInterestOnMF

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

                                'Update the NewDueDate of WESMBillSummary
                                itemWESMBillSummary1.NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.DefaultInterestOnMF.ToString() & " " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Default Interest on Withholding VAT on Market Fees
                    If defaultWithholdVat <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary2
                            .ReferenceNumber = itemWESMBillSummary2.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary2.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary2.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultWithholdVat
                            .EndingBalance = defaultMFV
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.WithholdingVatonDefaultInterest

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Default Interest on VAT on Market Fees
                    If defaultMFV <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary2
                            .ReferenceNumber = itemWESMBillSummary2.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary2.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary2.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultMFV
                            .EndingBalance = endingBalanceMFV
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

                                'Update the NewDueDate of WESMBillSummary
                                itemWESMBillSummary2.NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        If itemWESMBillSummary2.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.DefaultInterestOnVatOnMF.ToString() & " " & itemWESMBillSummary2.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary2.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        dicWESMBillSummaries.Add(itemWESMBillSummary2.WESMBillSummaryNo, itemWESMBillSummary2)

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Withholding Tax on Market Fees
                    If WithholdTaxOnMF <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = WithholdTaxOnMF
                            .EndingBalance = endingBalanceMF
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.WithholdingTaxOnMF

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultWithholdTax <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Market Fees
                    If MFValue <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = MFValue
                            .EndingBalance = endingBalanceMF
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.MarketFees

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultMF <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If

                            'Update the WESMBillSummary EndingBalance
                            itemWESMBillSummary1.EndingBalance = itemWESMBillSummary1.EndingBalance + MFValue
                        End With

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.MarketFees.ToString() & " " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString() & " " & MFValue.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary1.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary1.WESMBillSummaryNo) = itemWESMBillSummary1
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)
                        End If

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Withholding VAT on Market Fees
                    If WithholdVatOnMF <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = WithholdVatOnMF
                            .EndingBalance = endingBalanceMF
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.WithholdingVatOnMF

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultWithholdVat <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For VAT on Market Fees
                    If MFVValue <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary2
                            .ReferenceNumber = itemWESMBillSummary2.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary2.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary2.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = MFVValue
                            .EndingBalance = endingBalanceMFV
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.VatOnMarketFees

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultMF <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If


                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If

                            If defaultMF <> 0 Then
                                Dim itemColAlloc2 As New CollectionAllocation
                                With itemColAlloc2
                                    .WESMBillSummaryNo = itemWESMBillSummary2
                                    .ReferenceNumber = itemWESMBillSummary2.INVDMCMNo
                                    .ReferenceType = itemWESMBillSummary2.SummaryType
                                    .BatchCode = ""
                                    .BillingPeriod = itemWESMBillSummary2.BillPeriod
                                    .CollectionNumber = itemCollection.CollectionNumber
                                    .Amount = 0
                                    .EndingBalance = 0
                                    .DueDate = newDueDate
                                    .AllocationDate = allocationDateValue
                                    .CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF

                                    If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                        .NewDueDate = newDueDate
                                    Else
                                        .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

                                        'Update the NewDueDate of WESMBillSummary
                                        itemWESMBillSummary2.NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                                    End If
                                End With

                                listColAllocationCash.Add(itemColAlloc2)
                            End If

                        End With

                        'Update the WESMBillSummary EndingBalance
                        itemWESMBillSummary2.EndingBalance = itemWESMBillSummary2.EndingBalance + MFVValue

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.VatOnMarketFees.ToString() & " " & itemWESMBillSummary2.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary2.EndingBalance.ToString() & " " & MFVValue.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary2.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary2.WESMBillSummaryNo) = itemWESMBillSummary2
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary2.WESMBillSummaryNo, itemWESMBillSummary2)
                        End If

                        listColAllocationCash.Add(itemColAlloc)
                    End If


                Case EnumChargeType.E
                    'Lance Added 09212016
                    With Me.DGridView.Rows(index)
                        defaultInterest = CDec(.Cells("colDefaultInterest").Value)
                        defaultWithhold = CDec(.Cells("colDefaultWithhold").Value)
                        endingBalanceEnergyVAT = Math.Abs(CDec(.Cells("colEndingBalance").Value))
                        defaultCashEnergy = CDec(.Cells("colCashDefaultEnergy").Value)
                        cashEnergyVAT = CDec(.Cells("colCashEnergyAndVAT").Value)
                        defaultDrawdownEnergy = CDec(.Cells("colDrawdownDefaultEnergy").Value)
                        drawdownEnergy = CDec(.Cells("colDrawdownEnergy").Value)
                        WithholdTaxOnEnergy = CDec(.Cells("colTax").Value)
                    End With

                    'Check if the applied amount is equal to the distributed amount
                    Dim TotalTagAmount As Decimal = 0D

                    If WithholdTaxOnEnergy < 0 Then
                        TotalTagAmount = defaultCashEnergy + cashEnergyVAT + defaultDrawdownEnergy + drawdownEnergy
                    Else
                        TotalTagAmount = defaultCashEnergy + cashEnergyVAT + defaultDrawdownEnergy + drawdownEnergy + WithholdTaxOnEnergy
                    End If

                    If (totalCashPerItem + totalDrawdownPerItem) <> TotalTagAmount Then
                        Throw New ApplicationException("The allocated amount for " & CStr(Me.DGridView.Rows(index).Cells("colInvDMCMNoText").Value) & " " & _
                                                       "is not equal to the specified cash and drawdown amount!")

                    End If

                    'Get the WESM Bill Summary of Market Fees
                    itemWESMBillSummary1 = (From x In listWesmBillSummariesClone _
                                            Where x.WESMBillSummaryNo = wbSummaryNo1 _
                                            Select x).First()

                    If itemWESMBillSummary1.EndingBalance > 0 Then
                        Dim logs As String = EnumCollectionType.Energy.ToString() & "-Get " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                             itemWESMBillSummary1.EndingBalance.ToString()

                        BFactory.SaveLogFile(logs)

                        Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                    End If

                    If defaultCashEnergy <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultCashEnergy
                            .EndingBalance = endingBalanceEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.DefaultInterestOnEnergy

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

                                'Update the NewDueDate of WESMBillSummary
                                itemWESMBillSummary1.NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.DefaultInterestOnEnergy.ToString() & "-Cash " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    'For Withholding Tax on Energy
                    If WithholdTaxOnEnergy > 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = WithholdTaxOnEnergy
                            .EndingBalance = endingBalanceEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.WithholdingTaxonEnergy

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultWithholdTax <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With
                        itemWESMBillSummary1.EnergyWithholdStatus = EnumEnergyWithholdStatus.PaidEWT
                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary1.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary1.WESMBillSummaryNo) = itemWESMBillSummary1
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)
                        End If

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    If cashEnergyVAT <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = cashEnergyVAT
                            .EndingBalance = endingBalanceEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.Energy

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultCashEnergy <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With


                        'Update the WESMBillSummary EndingBalance
                        itemWESMBillSummary1.EndingBalance = itemWESMBillSummary1.EndingBalance + (cashEnergyVAT)

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.Energy.ToString() & "-Cash " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString() & " " & cashEnergyVAT.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary1.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary1.WESMBillSummaryNo) = itemWESMBillSummary1
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)
                        End If

                        listColAllocationCash.Add(itemColAlloc)
                    End If

                    If defaultDrawdownEnergy <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = defaultDrawdownEnergy
                            'Edited by Vloody 10/09/2014. Ending balance of Energy did not reflect the deducted amount from cash before drawdown.
                            .EndingBalance = endingBalanceEnergyVAT - cashEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.DefaultInterestOnEnergy

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))

                                'Update the NewDueDate of WESMBillSummary
                                itemWESMBillSummary1.NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.DefaultInterestOnEnergy.ToString() & "-Drawdown " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary1.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary1.WESMBillSummaryNo) = itemWESMBillSummary1
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)
                        End If

                        listColAllocationDrawdown.Add(itemColAlloc)
                    End If

                    If drawdownEnergy <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = drawdownEnergy
                            'Edited by Vloody 10/09/2014. Ending balance of Energy did not reflect the deducted amount from cash before drawdown.
                            .EndingBalance = endingBalanceEnergyVAT - cashEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.Energy

                            'If there is default, the due date will be NewDueDate, else, retain the Due Date
                            If defaultCashEnergy <> 0 Or defaultDrawdownEnergy <> 0 Then
                                .DueDate = newDueDate
                            Else
                                .DueDate = dueDate
                            End If

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        'Update the WESMBillSummary EndingBalance
                        itemWESMBillSummary1.EndingBalance = itemWESMBillSummary1.EndingBalance + drawdownEnergy

                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.Energy.ToString() & "-Drawdown " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                  itemWESMBillSummary1.EndingBalance.ToString() & " " & drawdownEnergy.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        If dicWESMBillSummaries.ContainsKey(itemWESMBillSummary1.WESMBillSummaryNo) Then
                            dicWESMBillSummaries(itemWESMBillSummary1.WESMBillSummaryNo) = itemWESMBillSummary1
                        Else
                            dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)
                        End If

                        listColAllocationDrawdown.Add(itemColAlloc)
                    End If

                Case EnumChargeType.EV
                    'Lance Added 09212016
                    With Me.DGridView.Rows(index)
                        endingBalanceEnergyVAT = Math.Abs(CDec(.Cells("colEndingBalance").Value))
                        cashEnergyVAT = CDec(.Cells("colCashEnergyAndVAT").Value)
                    End With

                    'Check if the applied amount is equal to the distributed amount
                    If totalCashPerItem + totalDrawdownPerItem <> cashEnergyVAT Then
                        Throw New ApplicationException("The allocated amount for " & CStr(Me.DGridView.Rows(index).Cells("colInvDMCMNoText").Value) & " " & _
                                                       "is not equal to the specified cash and drawdown amount!")

                    End If

                    'Get the WESM Bill Summary of Market Fees
                    itemWESMBillSummary1 = (From x In listWesmBillSummariesClone _
                                            Where x.WESMBillSummaryNo = wbSummaryNo1 _
                                            Select x).First()

                    If itemWESMBillSummary1.EndingBalance > 0 Then
                        Dim logs As String = EnumCollectionType.Energy.ToString() & "-Get " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                             itemWESMBillSummary1.EndingBalance.ToString()

                        BFactory.SaveLogFile(logs)

                        Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                    End If

                    If cashEnergyVAT <> 0 Then
                        Dim itemColAlloc As New CollectionAllocation
                        With itemColAlloc
                            .WESMBillSummaryNo = itemWESMBillSummary1
                            .ReferenceNumber = itemWESMBillSummary1.INVDMCMNo
                            .ReferenceType = itemWESMBillSummary1.SummaryType
                            .BatchCode = ""
                            .BillingPeriod = itemWESMBillSummary1.BillPeriod
                            .CollectionNumber = itemCollection.CollectionNumber
                            .Amount = cashEnergyVAT
                            .EndingBalance = endingBalanceEnergyVAT
                            .DueDate = newDueDate
                            .AllocationDate = allocationDateValue
                            .CollectionType = EnumCollectionType.VatOnEnergy

                            If newDueDate > CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) Then
                                .NewDueDate = newDueDate
                            Else
                                .NewDueDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                            End If
                        End With

                        'Update the WESMBillSummary EndingBalance
                        itemWESMBillSummary1.EndingBalance = itemWESMBillSummary1.EndingBalance + cashEnergyVAT


                        If itemWESMBillSummary1.EndingBalance > 0 Then
                            Dim logs As String = EnumCollectionType.VatOnEnergy.ToString() & "-Cash " & itemWESMBillSummary1.WESMBillSummaryNo.ToString() & " " & _
                                                 itemWESMBillSummary1.EndingBalance.ToString() & " " & cashEnergyVAT.ToString()

                            BFactory.SaveLogFile(logs)

                            Throw New ApplicationException("Ending balance becomes positive, contact administrator")
                        End If

                        'Add the WESMBillSummary
                        dicWESMBillSummaries.Add(itemWESMBillSummary1.WESMBillSummaryNo, itemWESMBillSummary1)

                        listColAllocationCash.Add(itemColAlloc)
                    End If

            End Select
        Next

        'Update collection cash status
        itemCollection.Status = EnumCollectionStatus.Allocated
        itemCollection.IsPosted = EnumIsPosted.NotPost
        itemCollection.AllocationDate = allocationDateValue

        'Add the list of collection allocation into cash collection
        itemCollection.ListOfCollectionAllocation.AddRange(listColAllocationCash)

        Dim listPRHistory As New List(Of PrudentialHistory)
        Dim itemPrudential As New Prudential
        Dim totalCash As Decimal
        Dim totalReplenishment As Decimal
        Dim totalDrawdown As Decimal
        Dim totalPrudential As Decimal
        Dim totalExcess As Decimal
        Dim chckExistPrudential As Boolean

        'Get the Total Cash and Drawdown
        For index As Integer = 0 To Me.DGridView.RowCount - 1
            totalCash += CDec(Me.DGridView.Rows(index).Cells("colCash").Value)
            totalDrawdown += CDec(Me.DGridView.Rows(index).Cells("colDrawdown").Value)
        Next

        'Check the applied amount if its greater than collected amount, held payment and prudential
        If totalCash > CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) Then '- totalReplenishment Then
            Throw New ApplicationException("The applied cash is greater than the collected amount, held payment and replenishment")

        ElseIf totalDrawdown > CDec(Me.txtPrudentialAmount.Text) Then
            Throw New ApplicationException("The applied drawdown is greater than the prudential amount")
        End If

        totalPrudential = CDec(Me.txtPrudentialAmount.Text) - totalDrawdown
        totalExcess = CDec(Me.txtAmountCollected.Text) + CDec(Me.txtHeldPayment.Text) - totalCash

        'Create collection for drawdown
        If totalDrawdown > 0 Then
            Dim itemPR As New Collection
            With itemPR
                .DMCMNumber = 1
                .CollectionNumber = PRCollectionNo
                .CollectionDate = CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))
                .IDNumber = CStr(Me.ddlParticipantID.SelectedValue)
                .CollectedAmount = totalDrawdown
                .AmountForAllocation = totalDrawdown
                .AllocationType = If(Me.rbAutomatic.Checked, EnumAllocationType.Automatic, EnumAllocationType.Manual)
                .CollectionCategory = EnumCollectionCategory.Drawdown
                .Status = EnumCollectionStatus.Allocated
                .IsPosted = EnumIsPosted.NotPost
                .AllocationDate = allocationDateValue
                .ListOfCollectionAllocation.AddRange(listColAllocationDrawdown)
            End With
            listCollections.Add(itemPR)
        End If


        If totalDrawdown > 0 Then
            listPRHistory.Add(New PrudentialHistory(0, New AMParticipants(IDNumber), totalDrawdown, EnumPrudentialTransType.Drawdown, _
                                                    CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), "", 1))
        End If

        Dim participant = (From x In Me.Participants _
                           Where x.IDNumber = itemCollection.IDNumber _
                           Select x).First()

        'Generation of the transfering of Excess Collection
        Dim listCollectionMonitoring = New List(Of CollectionMonitoring)

        If (totalExcess > 0 And totalCash <> 0) Or (totalExcess > 0 And CDec(Me.txtAmountCollected.Text) <> 0) Then
            With itemCollection
                listCollectionMonitoring.Add(New CollectionMonitoring(0, "", .CollectionNumber, allocationDateValue, _
                                                                      EnumAllocationType.Manual, participant, .ORNo, _
                                                                      totalExcess, EnumCollectionMonitoringType.TransferToExcessCollection, _
                                                                      EnumStatus.Active))
            End With

            Dim frm As New frmTransferAdvancePayment
            With frm
                .AllocationType = EnumAllocationType.Manual
                .ListCollectionMonitoring = listCollectionMonitoring
                .AllocationDate = allocationDateValue
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    listCollectionMonitoring = .ListCollectionMonitoring
                Else
                    Return False
                    Exit Function
                End If
            End With
        End If

        'Generate the collection monitoring for drawdown
        If totalDrawdown > 0 Then
            listCollectionMonitoring.Add(New CollectionMonitoring(0, "", PRCollectionNo, allocationDateValue, _
                                                                  EnumAllocationType.Manual, participant, 0, _
                                                                  totalDrawdown, EnumCollectionMonitoringType.TransferToPRDrawdown, _
                                                                  EnumStatus.Active))
        End If

        For Each item In listCollectionMonitoring
            If item.TransType = EnumCollectionMonitoringType.TransferToPRReplenishment Then
                listPRHistory.Add(New PrudentialHistory(New AMParticipants(IDNumber), item.Amount, EnumPrudentialTransType.Replenishment, _
                                                            CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))))
                totalReplenishment = item.Amount
            End If
        Next

        'Generate the total prudential
        If totalDrawdown > 0 Or totalReplenishment > 0 Then
            itemPrudential = New Prudential(IDNumber, totalPrudential + totalReplenishment, 0)

            'Check if the participant does exist in PR table. This applicable if replenishment
            If totalDrawdown = 0 Then
                If WBillHelper.GetParticipantPrudential(IDNumber).IDNumber.Length <> 0 Then
                    chckExistPrudential = True
                End If
            Else
                chckExistPrudential = True
            End If
        End If

        'Remove the collection with no values
        For index As Integer = 0 To listCollections.Count - 1
            With listCollections(index)
                If .CollectedAmount = 0 And .CollectedHeld = 0 Then
                    listCollections.RemoveAt(index)
                    Exit For
                End If
            End With
        Next

        'Remove the collection monitoring with zero amount
        For index As Integer = 0 To listCollectionMonitoring.Count - 1
            With listCollectionMonitoring(index)
                If .Amount = 0 Then
                    listCollectionMonitoring.RemoveAt(index)
                    Exit For
                End If
            End With
        Next

        'Get the WESM Bill Summary
        For Each item In dicWESMBillSummaries
            listWESMBillSummaries.Add(item.Value)
        Next
        listWESMBillSummaries.TrimExcess()

        'Get the WESM Bill Sales and Purchases
        Dim listInvoiceNo As New List(Of String)
        For Each item In listWESMBillSummaries
            If item.SummaryType = EnumSummaryType.INV And (item.ChargeType = EnumChargeType.E Or item.ChargeType = EnumChargeType.EV) Then
                listInvoiceNo.Add(item.INVDMCMNo)
            End If
        Next
        listInvoiceNo.TrimExcess()

        Dim listWESMBillSalesAndPurchases As New List(Of WESMBillSalesAndPurchased)
        If listInvoiceNo.Count > 0 Then
            listWESMBillSalesAndPurchases = WBillHelper.GetWESMInvoiceSalesAndPurchased(listInvoiceNo)
        End If

        'Update the collection no tag of Held Collection
        If totalCash <> 0 Or totalDrawdown = 0 Then
            For Each item In Me.ListHeldCollection
                item.CollectionNoTag = itemCollection.CollectionNumber
            Next
            listCollectionMonitoring.AddRange(ListHeldCollection)
        End If

        'Generate the OR
        Dim listOR = BFactory.GenerateCollectionOR(listCollections, listCollectionMonitoring, listWESMBillSalesAndPurchases, participant)

        'Generate DMCM Set-up
        Dim listDMCMSetup = BFactory.GenerateCollectionDMCM(listCollections, WBillHelper.GetSignatories("DMCM").First(), _
                                                            WBillHelper.GetDailyInterestRate())
        'Generate the DMCM Drawdown
        Dim listDMCMDrawdown = BFactory.GenerateDMCMDrawdown(listCollections, WBillHelper.GetSignatories("DMCM").First(), _
                                                             WBillHelper.GetDailyInterestRate())

        'Check if the collection allocated is greater than the collected amount
        Dim totalAllocated As Decimal = 0

        For Each item In itemCollection.ListOfCollectionAllocation
            totalAllocated += item.Amount
        Next

        If totalAllocated > itemCollection.CollectedAmount Then
            MsgBox("Allocated amount is greater than collected. Allocated amount is " & totalAllocated.ToString() & " and collected is " & itemCollection.CollectedAmount.ToString())
        End If

        'Check for duplicate
        Dim CollAllocDupCount As Integer = 0
        For Each item In listCollections
            Dim oItem As Collection = item

            Dim GetDuplicatesCollectionAlloc = item.ListOfCollectionAllocation.GroupBy(Function(s) s).SelectMany(Function(grp) grp.Skip(1))

            If GetDuplicatesCollectionAlloc.Count > 0 Then
                CollAllocDupCount += GetDuplicatesCollectionAlloc.Count
            End If
        Next


        If CollAllocDupCount <> 0 Then
            MessageBox.Show("Duplicate data found in collection allocation list. Please contact administrator." & vbNewLine & "Saving has been cancelled!", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return True
        End If

        'Save the collection and collections allocation
        WBillHelper.SaveCollectionWithCollectionAllocation(listWESMBillSummaries, listCollections, listCollectionMonitoring, _
                                                           listOR, itemPrudential, listPRHistory, listDMCMSetup, listDMCMDrawdown, chckExistPrudential)
        Return True
    End Function

    Private Sub MarketFeesApplication(ByVal currRow As Integer)
        Dim TotalValue As Decimal = 0, AmountToAllocate As Decimal = 0
        Dim MFValue As Decimal = 0, MFVValue As Decimal = 0
        Dim DefaultMF As Decimal = 0, DefaultMFV As Decimal = 0
        Dim WithholdTax As Decimal = 0, WithholdVAT As Decimal = 0
        Dim DefaultWithholdMF As Decimal = 0, DefaultWithholdMFV As Decimal = 0
        Dim DueDate As Date, NewDueDate As Date

        'Get daily interest rate
        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.Rows(currRow)
            Dim TotalCash = CDec(.Cells("colCash").Value)

            If TotalCash = 0 Then
                Exit Sub
            End If

            DueDate = CDate(.Cells("colDueDate").Value)
            NewDueDate = CDate(.Cells("colNewDueDate").Value)

            MFValue = CDec(.Cells("colMFOrig").Value)
            MFVValue = CDec(.Cells("colMFVorig").Value)
            WithholdTax = CDec(.Cells("colTaxOrig").Value)
            WithholdVAT = CDec(.Cells("colVatOrig").Value)

            'Check first if the default interest is edited
            If MFValue <> 0 And CDec(.Cells("colDefaultInterest").Value) <> 0 Then

                'For Default Interest on Market Fees
                DefaultMF = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                       CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                       MFValue, interestRate), 2)

                'For Default Interest on Withholding Tax on Market Fees
                If Me.SelectedIDNumber.MarketFeesWHTax <> 0 Then
                    DefaultWithholdMF = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                                  CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                  WithholdTax, interestRate), 2)
                End If

                'For Default Interest on Withholding VAT on Market Fees
                If SelectedIDNumber.MarketFeesWHVAT <> 0 Then
                    DefaultWithholdMFV = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                                    CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                    WithholdVAT, interestRate), 2)
                End If

            End If

            'Check first if the default interest is edited
            'If MFVValue <> 0 And CDec(.Cells("colDefaultInterest").Value) <> 0 Then

            '    'For Default Interest on VAT on Market Fees
            '    DefaultMFV = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate, _
            '                            CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), _
            '                            MFVValue, interestRate), 2)


            'End If

            'Check if default interest is bigger than cash
            If DefaultMF + DefaultMFV + DefaultWithholdMF + DefaultWithholdMFV > TotalCash Then

                'Reset the datagrid row
                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - TotalCash

                Exit Sub
            End If

            'Get the total
            TotalValue = MFValue + MFVValue + WithholdTax + WithholdVAT

            'Get the amount to allocate
            AmountToAllocate = TotalCash - DefaultMF - DefaultMFV - DefaultWithholdMF - DefaultWithholdMFV

            Dim totalDF As Decimal = DefaultMF + DefaultMFV + DefaultWithholdMF + DefaultWithholdMFV

            'For partial application
            If TotalValue > AmountToAllocate Then
                If MFValue <> 0 Then
                    MFValue = Math.Round((MFValue / TotalValue) * AmountToAllocate, 2)

                    If WithholdTax <> 0 Then
                        WithholdTax = Math.Round((WithholdTax / TotalValue) * AmountToAllocate, 2)
                    End If
                    'added by lance 11/04/2019
                    If WithholdVAT <> 0 Then
                        WithholdVAT = Math.Round((WithholdVAT / TotalValue) * AmountToAllocate, 2)
                    End If
                End If

                If MFVValue <> 0 Then
                    MFVValue = Math.Round((MFVValue / TotalValue) * AmountToAllocate, 2)

                    'If WithholdVAT <> 0 Then
                    '    WithholdVAT = Math.Round((WithholdVAT / TotalValue) * AmountToAllocate, 2)
                    'End If
                End If

                'If there is .01 difference, adjust market fees
                Dim allocatedAmount = MFValue + MFVValue + WithholdTax + WithholdVAT
                If allocatedAmount <> AmountToAllocate Then
                    MFValue = MFValue - (allocatedAmount - AmountToAllocate)
                End If
            End If

            .Cells("colDefaultMF").Value = DefaultMF
            .Cells("colDefaultMFV").Value = DefaultMFV
            .Cells("colDefaultWithholdMFTax").Value = DefaultWithholdMF
            .Cells("colDefaultWithholdMFVAT").Value = DefaultWithholdMFV
            .Cells("colMF").Value = MFValue
            .Cells("colMFV").Value = MFVValue
            .Cells("colTax").Value = WithholdTax
            .Cells("colVat").Value = WithholdVAT
            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - TotalCash
        End With
    End Sub

    Private Sub MarketFeesApplication()
        Dim TotalValue As Decimal = 0, AmountToAllocate As Decimal = 0
        Dim MFValue As Decimal = 0, MFVValue As Decimal = 0
        Dim DefaultMF As Decimal = 0, DefaultMFV As Decimal = 0
        Dim WithholdTax As Decimal = 0, WithholdVAT As Decimal = 0
        Dim DefaultWithholdMF As Decimal = 0, DefaultWithholdMFV As Decimal = 0
        Dim DueDate As Date, NewDueDate As Date

        'Get daily interest rate
        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.CurrentRow
            Dim TotalCash = CDec(.Cells("colCash").Value)

            If TotalCash = 0 Then
                Exit Sub
            End If

            DueDate = CDate(.Cells("colDueDate").Value)
            NewDueDate = CDate(.Cells("colNewDueDate").Value)

            MFValue = CDec(.Cells("colMFOrig").Value)
            MFVValue = CDec(.Cells("colMFVorig").Value)
            WithholdTax = CDec(.Cells("colTaxOrig").Value)
            WithholdVAT = CDec(.Cells("colVatOrig").Value)

            'Check first if the default interest is edited
            If MFValue <> 0 And CDec(.Cells("colDefaultInterest").Value) <> 0 Then

                'For Default Interest on Market Fees
                DefaultMF = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                       CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                       MFValue, interestRate), 2)

                'For Default Interest on Withholding Tax on Market Fees
                If Me.SelectedIDNumber.MarketFeesWHTax <> 0 Then
                    DefaultWithholdMF = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                                  CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                  WithholdTax, interestRate), 2)
                End If

                'For Default Interest on Withholding VAT on Market Fees
                If SelectedIDNumber.MarketFeesWHVAT <> 0 Then
                    DefaultWithholdMFV = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate,
                                                    CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)),
                                                    WithholdVAT, interestRate), 2)
                End If

            End If

            'Check first if the default interest is edited
            'If MFVValue <> 0 And CDec(.Cells("colDefaultInterest").Value) <> 0 Then

            '    'For Default Interest on VAT on Market Fees
            '    DefaultMFV = Math.Round(BFactory.ComputeDefaultInterest(DueDate, NewDueDate, _
            '                            CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)), _
            '                            MFVValue, interestRate), 2)


            'End If

            'Check if default interest is bigger than cash
            If DefaultMF + DefaultMFV + DefaultWithholdMF + DefaultWithholdMFV > TotalCash Then

                'Reset the datagrid row
                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - TotalCash

                Exit Sub
            End If

            'Get the total
            TotalValue = MFValue + MFVValue + WithholdTax + WithholdVAT

            'Get the amount to allocate
            AmountToAllocate = TotalCash - DefaultMF - DefaultMFV - DefaultWithholdMF - DefaultWithholdMFV

            Dim totalDF As Decimal = DefaultMF + DefaultMFV + DefaultWithholdMF + DefaultWithholdMFV

            'For partial application
            If TotalValue > AmountToAllocate Then
                If MFValue <> 0 Then
                    MFValue = Math.Round((MFValue / TotalValue) * AmountToAllocate, 2)

                    If WithholdTax <> 0 Then
                        WithholdTax = Math.Round((WithholdTax / TotalValue) * AmountToAllocate, 2)
                    End If
                    'added by lance 11/04/2019
                    If WithholdVAT <> 0 Then
                        WithholdVAT = Math.Round((WithholdVAT / TotalValue) * AmountToAllocate, 2)
                    End If
                End If

                If MFVValue <> 0 Then
                    MFVValue = Math.Round((MFVValue / TotalValue) * AmountToAllocate, 2)

                    'If WithholdVAT <> 0 Then
                    '    WithholdVAT = Math.Round((WithholdVAT / TotalValue) * AmountToAllocate, 2)
                    'End If
                End If

                'If there is .01 difference, adjust market fees
                Dim allocatedAmount = MFValue + MFVValue + WithholdTax + WithholdVAT
                If allocatedAmount <> AmountToAllocate Then
                    MFValue = MFValue - (allocatedAmount - AmountToAllocate)
                End If
            End If

            .Cells("colDefaultMF").Value = DefaultMF
            .Cells("colDefaultMFV").Value = DefaultMFV
            .Cells("colDefaultWithholdMFTax").Value = DefaultWithholdMF
            .Cells("colDefaultWithholdMFVAT").Value = DefaultWithholdMFV
            .Cells("colMF").Value = MFValue
            .Cells("colMFV").Value = MFVValue
            .Cells("colTax").Value = WithholdTax
            .Cells("colVat").Value = WithholdVAT
            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - TotalCash
        End With
    End Sub

    Private Sub CashEnergyAndVatOnEnergyApplication()
        Dim EndingBalance As Decimal, DefaultInterest As Decimal
        Dim WithholdingTax As Decimal, WithholdingVat As Decimal
        Dim TotalCash As Decimal, TotalEnergyOrVATNetWithholding As Decimal

        'Get daily interest rate
        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.CurrentRow
            EndingBalance = Math.Abs(CDec(.Cells("colEndingBalance").Value))
            WithholdingTax = CDec(.Cells("colTaxOrig").Value)
            WithholdingVat = CDec(.Cells("colVatOrig").Value)
            DefaultInterest = CDec(.Cells("colDefaultInterest").Value)
            TotalCash = CDec(.Cells("colCash").Value)

            'Reset the fields
            .Cells("colCashEnergyAndVAT").Value = 0
            .Cells("colCashDefaultEnergy").Value = 0
            .Cells("colDrawdown").Value = 0
            .Cells("colDrawdownEnergy").Value = 0
            .Cells("colDrawdownDefaultEnergy").Value = 0

            'Check if default interest is bigger than cash and remaining prudential
            If TotalCash + CDec(Me.txtRemainingPrudential.Text) < DefaultInterest Then

                'Reset the datagrid row
                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                Exit Sub
            End If

            'No bearing if Energy or VAT on Energy
            .Cells("colTax").Value = .Cells("colTaxOrig").Value
            .Cells("colVat").Value = .Cells("colVatOrig").Value

            'Check if total cash is greater than default interest vice versa
            If TotalCash <= DefaultInterest Then
                .Cells("colCashDefaultEnergy").Value = TotalCash
                TotalCash = 0
            Else
                .Cells("colCashDefaultEnergy").Value = DefaultInterest
                TotalCash = TotalCash - DefaultInterest
            End If

            'Compute the Energy and VAT Net Withholding
            If TotalCash <> 0 Then
                'Lance for Edit

                'Get the Energy and Vat Net Withholding
                TotalEnergyOrVATNetWithholding = EndingBalance
                If TotalCash <= Math.Abs(TotalEnergyOrVATNetWithholding) Then
                    If WithholdingTax = TotalCash And EndingBalance = 0 Then
                        .Cells("colTax").Value = TotalCash
                    ElseIf WithholdingTax > 0 And EndingBalance > 0 Then
                        .Cells("colTax").Value = WithholdingTax
                        .Cells("colCashEnergyAndVAT").Value = TotalCash - WithholdingTax
                    Else
                        'Lance added on 09202016
                        .Cells("colCashEnergyAndVAT").Value = TotalCash
                    End If
                Else
                    .Cells("colTax").Value = WithholdingTax
                    .Cells("colCashEnergyAndVAT").Value = TotalCash
                End If

            End If

            'Update the remaining balance
            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - _
                                                  CDec(.Cells("colCashEnergyAndVAT").Value) - _
                                                  CDec(.Cells("colCashDefaultEnergy").Value)
        End With
    End Sub

    Private Sub CashEnergyAndVatOnEnergyApplication(ByVal currRow As Integer)
        Dim EndingBalance As Decimal, DefaultInterest As Decimal
        Dim WithholdingTax As Decimal, WithholdingVat As Decimal
        Dim TotalCash As Decimal, TotalEnergyOrVATNetWithholding As Decimal

        'Get daily interest rate
        If Not Me._dicInterestRate.ContainsKey(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))) Then
            Throw New Exception("No available default interest for the selected collection date (" & CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) & ")!")
        End If

        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.Rows(currRow)
            EndingBalance = Math.Abs(CDec(.Cells("colEndingBalance").Value))
            WithholdingTax = CDec(.Cells("colTaxOrig").Value)
            WithholdingVat = CDec(.Cells("colVatOrig").Value)

            DefaultInterest = CDec(.Cells("colDefaultInterest").Value)
            TotalCash = CDec(.Cells("colCash").Value)
            'Reset the fields
            .Cells("colCashEnergyAndVAT").Value = 0
            .Cells("colCashDefaultEnergy").Value = 0
            .Cells("colDrawdown").Value = 0
            .Cells("colDrawdownEnergy").Value = 0
            .Cells("colDrawdownDefaultEnergy").Value = 0

            'Check if default interest is bigger than cash and remaining prudential
            If TotalCash + CDec(Me.txtRemainingPrudential.Text) < DefaultInterest Then

                'Reset the datagrid row
                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                Exit Sub
            End If

            'No bearing if Energy or VAT on Energy
            .Cells("colTax").Value = .Cells("colTaxOrig").Value
            .Cells("colVat").Value = .Cells("colVatOrig").Value

            'Check if total cash is greater than default interest vice versa
            If TotalCash <= DefaultInterest Then
                .Cells("colCashDefaultEnergy").Value = TotalCash
                TotalCash = 0
            Else
                .Cells("colCashDefaultEnergy").Value = DefaultInterest
                TotalCash = TotalCash - DefaultInterest
            End If

            'Compute the Energy and VAT Net Withholding
            If TotalCash <> 0 Then
                'Lance for Edit

                'Get the Energy and Vat Net Withholding
                TotalEnergyOrVATNetWithholding = EndingBalance
                If TotalCash <= Math.Abs(TotalEnergyOrVATNetWithholding) Then
                    If WithholdingTax = TotalCash And EndingBalance = 0 Then
                        .Cells("colTax").Value = TotalCash
                    ElseIf WithholdingTax > 0 And EndingBalance > 0 Then
                        .Cells("colTax").Value = WithholdingTax
                        .Cells("colCashEnergyAndVAT").Value = TotalCash - WithholdingTax
                    Else
                        'Lance added on 09202016
                        .Cells("colCashEnergyAndVAT").Value = TotalCash
                    End If
                Else
                    .Cells("colTax").Value = WithholdingTax
                    .Cells("colCashEnergyAndVAT").Value = TotalCash
                End If

            End If

            'Update the remaining balance
            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) -
                                                  CDec(.Cells("colCashEnergyAndVAT").Value) -
                                                  CDec(.Cells("colCashDefaultEnergy").Value)
        End With
    End Sub

    Private Sub PREnergyApplication(ByVal currRow As Integer)
        Dim EndingBalance As Decimal, DefaultInterest As Decimal
        Dim WithholdingTax As Decimal, WithholdingVat As Decimal
        Dim TotalPRDrawdown As Decimal, TotalEnergyOrVATNetWithholding As Decimal

        'Get daily interest rate
        If Not Me._dicInterestRate.ContainsKey(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))) Then
            Throw New Exception("No available default interest for the selected collection date (" & CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)) & ")!")
        End If

        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.Rows(currRow)
            EndingBalance = Math.Abs(CDec(.Cells("colEndingBalance").Value))
            WithholdingTax = CDec(.Cells("colTaxOrig").Value)
            WithholdingVat = CDec(.Cells("colVatOrig").Value)

            DefaultInterest = CDec(.Cells("colDefaultInterest").Value)
            TotalPRDrawdown = CDec(.Cells("colDrawdown").Value)
            'Reset the fields
            .Cells("colCashEnergyAndVAT").Value = 0
            .Cells("colCashDefaultEnergy").Value = 0
            .Cells("colCash").Value = 0
            .Cells("colDrawdownEnergy").Value = 0
            .Cells("colDrawdownDefaultEnergy").Value = 0

            'Check if default interest is bigger than cash and remaining prudential
            If TotalPRDrawdown + CDec(Me.txtRemainingPrudential.Text) < DefaultInterest Then

                'Reset the datagrid row
                Me.ResetDatagridRow(Me.DGridView.CurrentRow.Index)
                Exit Sub
            End If

            'No bearing if Energy or VAT on Energy
            .Cells("colTax").Value = .Cells("colTaxOrig").Value
            .Cells("colVat").Value = .Cells("colVatOrig").Value

            'Check if total cash is greater than default interest vice versa
            If TotalPRDrawdown <= DefaultInterest Then
                .Cells("colDrawdownDefaultEnergy").Value = TotalPRDrawdown
                TotalPRDrawdown = 0
            Else
                .Cells("colDrawdownDefaultEnergy").Value = DefaultInterest
                TotalPRDrawdown = TotalPRDrawdown - DefaultInterest
            End If

            'Compute the Energy and VAT Net Withholding
            If TotalPRDrawdown <> 0 Then
                'Get the Energy and Vat Net Withholding
                TotalEnergyOrVATNetWithholding = EndingBalance
                If TotalPRDrawdown <= Math.Abs(TotalEnergyOrVATNetWithholding) Then
                    If WithholdingTax = TotalPRDrawdown And EndingBalance = 0 Then
                        .Cells("colTax").Value = TotalPRDrawdown
                    ElseIf WithholdingTax > 0 And EndingBalance > 0 Then
                        .Cells("colTax").Value = WithholdingTax
                        .Cells("colDrawdownEnergy").Value = TotalPRDrawdown - WithholdingTax
                    Else
                        'Lance added on 09202016
                        .Cells("colDrawdownEnergy").Value = TotalPRDrawdown
                    End If
                Else
                    .Cells("colTax").Value = WithholdingTax
                    .Cells("colDrawdownEnergy").Value = TotalPRDrawdown
                End If

            End If
            'Update the remaining balance
            .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) -
                                                  CDec(.Cells("colDrawdownEnergy").Value) -
                                                  CDec(.Cells("colDrawdownDefaultEnergy").Value)
        End With
    End Sub
    Private Sub DrawdownEnergy()
        Dim TotalPayable As Decimal, DefaultInterest As Decimal
        Dim WithholdingTax As Decimal, WithholdingVat As Decimal
        Dim TotalDrawdown As Decimal, TotalEnergyNetWithholding As Decimal
        Dim TotalDrawdownDefault As Decimal, CashEnergy As Decimal, CashDefaultEnergy As Decimal

        'Get daily interest rate
        If Not Me._dicInterestRate.ContainsKey(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate))) Then
            MessageBox.Show("No available default interest rate for the date of " & CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)).ToShortDateString)
            Exit Sub
        End If
        Dim interestRate = Me._dicInterestRate(CDate(FormatDateTime(Me.DTCollection.Value, DateFormat.ShortDate)))

        With Me.DGridView.CurrentRow
            TotalPayable = Math.Abs(CDec(.Cells("colTotalPayable").Value))
            WithholdingTax = CDec(.Cells("colTaxOrig").Value)
            WithholdingVat = CDec(.Cells("colVatOrig").Value)
            DefaultInterest = CDec(.Cells("colDefaultInterest").Value)
            TotalDrawdown = CDec(.Cells("colDrawdown").Value)
            CashEnergy = CDec(.Cells("colCashEnergyAndVAT").Value)
            CashDefaultEnergy = CDec(.Cells("colCashDefaultEnergy").Value)

            'Reset the fields
            .Cells("colDrawdownEnergy").Value = 0
            .Cells("colDrawdownDefaultEnergy").Value = 0

            'Check if default interest is bigger than cash and remaining prudential
            If TotalDrawdown + CashDefaultEnergy < DefaultInterest Then

                'Reset the Drawdown Energy and Default Interest
                .Cells("colDrawdown").Value = 0

                .Cells("colRemainingBalance").Value = CDec(.Cells("colTotalPayable").Value) - _
                                                      CDec(.Cells("colCashEnergyAndVAT").Value) - _
                                                      CDec(.Cells("colCashDefaultEnergy").Value) - _
                                                      CDec(.Cells("colDrawdownDefaultEnergy").Value) - _
                                                      CDec(.Cells("colDrawdownEnergy").Value)
                Exit Sub
            End If

            'No bearing if Energy or VAT on Energy
            .Cells("colTax").Value = .Cells("colTaxOrig").Value
            .Cells("colVat").Value = .Cells("colVatOrig").Value

            TotalDrawdownDefault = DefaultInterest - CashDefaultEnergy
            TotalDrawdown = TotalDrawdown - TotalDrawdownDefault
            TotalEnergyNetWithholding = TotalPayable - CashEnergy - CashDefaultEnergy - TotalDrawdownDefault

            If TotalDrawdown >= TotalEnergyNetWithholding Then
                .Cells("colDrawdownEnergy").Value = TotalEnergyNetWithholding
            Else
                .Cells("colDrawdownEnergy").Value = TotalDrawdown
            End If

            .Cells("colDrawdownDefaultEnergy").Value = TotalDrawdownDefault

            'Update the remaining balance
            '.Cells("colRemainingBalance").Value = TotalPayable - CashEnergy - CashDefaultEnergy - _
            'TotalDrawdown -TotalDrawdownDefault + WithholdingTax + WithholdingVat

            'Removed withholding tax: 10/11/2015 
            .Cells("colRemainingBalance").Value = TotalPayable - CashEnergy - CashDefaultEnergy - _
                                                 TotalDrawdown - TotalDrawdownDefault
        End With
    End Sub

    Private Sub ResetDatagridRow(ByVal index As Integer)
        With Me.DGridView.Rows(index)
            .Cells("colRemainingBalance").Value = .Cells("colTotalPayable").Value
            .Cells("colCash").Value = 0
            .Cells("colMF").Value = 0
            .Cells("colMFV").Value = 0
            .Cells("colTax").Value = 0
            .Cells("colVat").Value = 0
            .Cells("colDefaultMF").Value = 0
            .Cells("colDefaultMFV").Value = 0
            .Cells("colDefaultWithholdMFTax").Value = 0
            .Cells("colDefaultWithholdMFVat").Value = 0

            .Cells("colDrawdown").Value = 0
            .Cells("colCashEnergyAndVAT").Value = 0
            .Cells("colCashDefaultEnergy").Value = 0
            .Cells("colDrawdownEnergy").Value = 0
            .Cells("colDrawdownDefaultEnergy").Value = 0
        End With
    End Sub

    Private Sub EditCollection(ByRef item As Collection)

        Dim itemOriginal As Collection

        itemOriginal = Me.WBillHelper.GetCollectionByORNo(item.ORNo)

        If Math.Abs(item.CollectedAmount) <> Math.Abs(itemOriginal.CollectedAmount) Or item.CollectionDate <> itemOriginal.CollectionDate Then
            Dim ListCollections As New List(Of Collection)
            Dim itemJV As New JournalVoucher
            Dim itemGPPosted As New WESMBillGPPosted
            Dim jvDetails As New List(Of JournalVoucherDetails)
            Dim SignatoriesJV As DocSignatories

            SignatoriesJV = Me.WBillHelper.GetSignatories("JV").First()

            Dim total = Math.Abs(item.CollectedAmount) + Math.Abs(itemOriginal.CollectedAmount)

            'Entry for the updated collection
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashInbankSettlementcode, Math.Abs(item.CollectedAmount), 0))
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.ClearingAccountCode, 0, Math.Abs(item.CollectedAmount)))

            'Entry for the edited collection
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.CashInbankSettlementcode, 0, Math.Abs(itemOriginal.CollectedAmount)))
            jvDetails.Add(New JournalVoucherDetails(0, AMModule.ClearingAccountCode, Math.Abs(itemOriginal.CollectedAmount), 0))

            'For Journal Voucher
            With itemJV
                .JVNumber = 0
                .Status = 1
                .JVDate = item.CollectionDate
                .PreparedBy = AMModule.FullName
                .CheckedBy = SignatoriesJV.Signatory_1
                .ApprovedBy = SignatoriesJV.Signatory_2
                .PostedType = EnumPostedType.DC.ToString()
                .JVDetails = jvDetails
                .Remarks = "Edit collection with OR Number:" & item.ORNo.ToString()
            End With

            'Create GP Posted
            itemGPPosted = BFactory.GenerateDailyCollectionGPPosted(itemJV)

            'Add the collection
            ListCollections.Add(item)

            'Save/Update Journal Voucher and GP Posted
            WBillHelper.PostDailyCollections(ListCollections, itemJV, itemGPPosted)
        End If

        'Update the collection
        WBillHelper.SaveCollection(item)
    End Sub
    Private Sub cb_CheckAll_CheckedChanged(sender As Object, e As EventArgs) Handles cb_CheckAll.CheckedChanged
        Dim totalPayable As Decimal = 0
        If checkAllBox = False Then
            'For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
            '    totalPayable += CDec(Me.DGridView.Rows(i).Cells("colTotalPayable").Value)
            'Next

            'If CDec(txtAmountCollected.Text) < Math.Abs(totalPayable) Then
            '    MessageBox.Show("Total Payables is greater than the amount collected! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Me.cb_CheckAll.Checked = False
            '    Exit Sub
            'End If

            If CDec(txtAmountCollected.Text) = 0 And CDec(txtPrudentialAmount.Text) = 0 Then
                MessageBox.Show("No amount collected, nor available Prudential! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.cb_CheckAll.Checked = False
                Exit Sub
            End If
            If CDec(txtAmountCollected.Text) <> 0 Then
                Dim checkCounter As Integer = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    Dim ChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType),
                                                   CStr(Me.DGridView.Rows(i).Cells("colChargeType").Value)), EnumChargeType)
                    If CBool(Me.DGridView.Rows(i).Cells("colChckPay").Value) = False And Me.DGridView.Rows(i).Cells("colInvDMCMNo").Value.ToString().Contains("TS-W") Then
                        Me.DGridView.Rows(i).Cells("colChckPay").Value = True
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)

                        With Me.DGridView.Rows(i)
                            'Enable/Disable AmoutToPay column
                            If CBool(.Cells("colChckPay").Value) = False Then
                                .Cells("colCash").ReadOnly = False
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.Rows(i).Index)
                            Else
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.Rows(i).Index)
                                Dim AmountToAllocate As Decimal = Math.Abs(CDec(.Cells("colTotalPayable").Value))

                                .Cells("colCash").ReadOnly = True
                                .Cells("colCash").Value = AmountToAllocate
                                Select Case ChargeType
                                    Case EnumChargeType.MF
                                        Me.MarketFeesApplication(i)
                                    Case EnumChargeType.E, EnumChargeType.EV
                                        Me.CashEnergyAndVatOnEnergyApplication(i)
                                End Select
                            End If
                        End With
                        checkCounter += 1
                    End If
                Next
                If checkCounter > 0 Then
                    'Compute the remaining cash, prudential and applied amount
                    Me.ComputeRemainingCash()
                    Me.ComputeRemainingPrudential()
                    Me.ComputeTotalAppliedAmount()
                    checkAllBox = True
                Else
                    MessageBox.Show("No available invoices to check! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            ElseIf CDec(txtAmountCollected.Text) = 0 And CDec(txtPrudentialAmount.Text) <> 0 Then
                Dim checkCounterPR As Integer = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    Dim ChargeType As EnumChargeType = CType(System.Enum.Parse(GetType(EnumChargeType),
                                                   CStr(Me.DGridView.Rows(i).Cells("colChargeType").Value)), EnumChargeType)
                    If ChargeType = EnumChargeType.E Then
                        If CBool(Me.DGridView.Rows(i).Cells("colChckPay").Value) = False And Me.DGridView.Rows(i).Cells("colInvDMCMNo").Value.ToString().Contains("TS-W") Then
                            Me.DGridView.Rows(i).Cells("colChckPay").Value = True
                            Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                            With Me.DGridView.Rows(i)
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.Rows(i).Index)
                                Dim AmountToAllocate As Decimal = Math.Abs(CDec(.Cells("colTotalPayable").Value))
                                .Cells("colDrawdown").ReadOnly = True
                                .Cells("colDrawdown").Value = AmountToAllocate
                                Me.PREnergyApplication(i)
                            End With
                            checkCounterPR += 1
                        End If
                    End If
                Next
                If checkCounterPR > 0 Then
                    'Compute the remaining cash, prudential and applied amount                    
                    Me.ComputeRemainingPrudential()
                    Me.ComputeTotalAppliedAmount()
                    checkAllBox = True
                Else
                    MessageBox.Show("No available invoices to check! Check All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        Else
            If CDec(txtAmountCollected.Text) <> 0 Then
                Dim uncheckCounter As Integer = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    If CBool(Me.DGridView.Rows(i).Cells("colChckPay").Value) = True Then
                        Me.DGridView.Rows(i).Cells("colChckPay").Value = False
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        With Me.DGridView.Rows(i)
                            'Enable/Disable AmoutToPay column
                            If CBool(.Cells("colChckPay").Value) = False Then
                                .Cells("colCash").ReadOnly = False
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.Rows(i).Index)
                            End If
                        End With
                        uncheckCounter += 1
                    End If
                Next
                If uncheckCounter > 0 Then
                    'Compute the remaining cash, prudential and applied amount
                    Me.ComputeRemainingCash()
                    Me.ComputeRemainingPrudential()
                    Me.ComputeTotalAppliedAmount()
                    checkAllBox = False
                Else
                    MessageBox.Show("No available invoices to uncheck! Uncheck All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            ElseIf CDec(txtAmountCollected.Text) = 0 And CDec(txtPrudentialAmount.Text) <> 0 Then
                Dim uncheckCounterPR As Integer = 0
                For i As Integer = 0 To Me.DGridView.Rows.Count() - 1
                    If CBool(Me.DGridView.Rows(i).Cells("colChckPay").Value) = True Then
                        Me.DGridView.Rows(i).Cells("colChckPay").Value = False
                        Me.DGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
                        With Me.DGridView.Rows(i)
                            'Enable/Disable AmoutToPay column
                            If CBool(.Cells("colChckPay").Value) = False Then
                                .Cells("colDrawdown").ReadOnly = False
                                'Reset the datagrid row
                                Me.ResetDatagridRow(Me.DGridView.Rows(i).Index)
                            End If
                        End With
                        uncheckCounterPR += 1
                    End If
                Next
                If uncheckCounterPR > 0 Then
                    'Compute the remaining cash, prudential and applied amount
                    Me.ComputeRemainingCash()
                    Me.ComputeRemainingPrudential()
                    Me.ComputeTotalAppliedAmount()
                    checkAllBox = False
                Else
                    MessageBox.Show("No available invoices to uncheck! Uncheck All is cancelled.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End If
    End Sub
#End Region

End Class