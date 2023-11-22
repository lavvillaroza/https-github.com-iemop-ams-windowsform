'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollection
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
'   August 05, 2013         Vladimir E. Espiritu                 Added button which untag collection
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin


Public Class frmCollection
    Private WBillHelper As WESMBillHelper
    Private BFactory As BusinessFactory

    Public _CollectionOfResult As FuncAutoCollectionAllocationResult
    Public _ListOfCollections As List(Of Collection)
    Public _AMParticipants As List(Of AMParticipants)
    Public _dicInterestRate As Dictionary(Of Date, Decimal)
    Public _ListCharges As List(Of ChargeId)
    Public _ListCalendar As List(Of CalendarBillingPeriod)

    Private _ListDMCM As List(Of DebitCreditMemo)
    Private _ListDMCMDrawdown As List(Of DebitCreditMemo)
    Private _ListAccountingCodes As List(Of AccountingCode)
    Private _AllocationDate As Date

    Private Enum AllocationCategory
        WithHoldingTax
        DefaultInterest
        NotDefaultInterest
    End Enum

    Private Enum EnumORRemarks
        Energy
        VATonEnergy
        MarketFees
        VATonMarketFees
        ExcessCollection
        PrudentialReplenishment
        HeldCollection
    End Enum

    Private Sub frmCollection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MdiParent = MainForm
        Try            
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            WBillHelper.UserName = AMModule.UserName
            BFactory = BusinessFactory.GetInstance()

            'Initialization
            Me._CollectionOfResult = New FuncAutoCollectionAllocationResult()
            Me._ListOfCollections = New List(Of Collection)

            'Get the daily interest rates
            Me._dicInterestRate = WBillHelper.GetDailyInterestRate()

            'Get AM Participants
            Me._AMParticipants = WBillHelper.GetAMParticipantsAll()

            'Get the Accounting Code
            Me._ListAccountingCodes = WBillHelper.GetAccountingCodes()

            'Get the charges
            Me._ListCharges = WBillHelper.GetChargeIDCodes()

            'Get the billing periods
            Me._ListCalendar = WBillHelper.GetCalendarBP()

            'Compute total
            Me.ComputeTotal()

            Me.EnableControls(False, True, False, False, False, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Errror")            
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim collectionItems As New List(Of Collection)
        Dim result As New List(Of Collection)

        Try
            Dim frmSearch As New frmCollectionSearch
            With frmSearch
                Dim valSize As New System.Drawing.Size
                valSize.Width = 309
                valSize.Height = 323

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.CollectionSearch
                If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
            End With

            Dim dateFrom As Date, dateTo As Date
            Dim idnumber As String = ""
            Dim IsCollectionDate As Boolean
            Dim typeAllocation As EnumAllocationType?
            Dim typeStatus As EnumCollectionStatus?
            Dim IsPosted As EnumIsPosted?

            With frmSearch
                dateFrom = CDate(FormatDateTime(.dtFrom.Value, DateFormat.ShortDate))
                dateTo = CDate(FormatDateTime(.dtTo.Value, DateFormat.ShortDate))

                If .ddlParticipantID.SelectedIndex <> -1 Then
                    idnumber = CStr(frmSearch.ddlParticipantID.SelectedValue)
                End If

                If .rbCollectionDate.Checked Then
                    IsCollectionDate = True
                Else
                    IsCollectionDate = False
                End If

                If .chckAuto.CheckState = CheckState.Checked And .chckManual.CheckState = CheckState.Unchecked Then
                    typeAllocation = EnumAllocationType.Automatic
                ElseIf .chckAuto.CheckState = CheckState.Unchecked And .chckManual.CheckState = CheckState.Checked Then
                    typeAllocation = EnumAllocationType.Manual
                Else
                    typeAllocation = Nothing
                End If

                If .chckAllocated.CheckState = CheckState.Checked And .chckUnallocated.CheckState = CheckState.Unchecked Then
                    typeStatus = EnumCollectionStatus.Allocated
                ElseIf .chckAllocated.CheckState = CheckState.Unchecked And .chckUnallocated.CheckState = CheckState.Checked Then
                    typeStatus = EnumCollectionStatus.NotAllocated
                Else
                    typeStatus = Nothing
                End If

                If .chckPosted.CheckState = CheckState.Checked And .chckNotPost.CheckState = CheckState.Unchecked Then
                    IsPosted = EnumIsPosted.Posted
                ElseIf .chckPosted.CheckState = CheckState.Unchecked And .chckNotPost.CheckState = CheckState.Checked Then
                    IsPosted = EnumIsPosted.NotPost
                Else
                    IsPosted = Nothing
                End If
            End With

            'Get the collections
            If idnumber = "" Then
                result = WBillHelper.GetCollections(dateFrom, dateTo, IsCollectionDate)
            Else
                result = WBillHelper.GetCollections(dateFrom, dateTo, idnumber, IsCollectionDate)
            End If

            If Not typeAllocation.HasValue And Not typeStatus.HasValue And Not IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Select x).ToList()

            ElseIf Not typeAllocation.HasValue And typeStatus.HasValue And Not IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.Status = typeStatus _
                                   Select x).ToList()

            ElseIf Not typeAllocation.HasValue And typeStatus.HasValue And IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.Status = typeStatus And x.IsPosted = IsPosted _
                                   Select x).ToList()

            ElseIf Not typeAllocation.HasValue And Not typeStatus.HasValue And IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.IsPosted = IsPosted _
                                   Select x).ToList()

            ElseIf typeAllocation.HasValue And Not typeStatus.HasValue And Not IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.AllocationType = typeAllocation _
                                   Select x).ToList()

            ElseIf typeAllocation.HasValue And typeStatus.HasValue And Not IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.AllocationType = typeAllocation And x.Status = typeStatus _
                                   Select x).ToList()

            ElseIf typeAllocation.HasValue And typeStatus.HasValue And IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.AllocationType = typeAllocation And x.Status = typeStatus And x.IsPosted = IsPosted _
                                   Select x).ToList()

            ElseIf typeAllocation.HasValue And Not typeStatus.HasValue And IsPosted.HasValue Then
                collectionItems = (From x In result _
                                   Where x.AllocationType = typeAllocation And x.IsPosted = IsPosted _
                                   Select x).ToList()
            End If


            If collectionItems.Count = 0 Then
                Me.DGridViewCollection.Rows.Clear()
                Me.DGridViewAllocCollections.Rows.Clear()
                MsgBox("No records found!", MsgBoxStyle.Information, "No data")

                Me._ListOfCollections = New List(Of Collection)
            Else
                Me._ListOfCollections = New List(Of Collection)
                Me.DGridViewCollection.Rows.Clear()
                Me.DisplayOnGrid(collectionItems)

                'Add the collections which are already allocated
                For Each item In collectionItems
                    If item.Status = EnumCollectionStatus.Allocated Then
                        Me._ListOfCollections.Add(item)
                    End If
                Next
                Me._ListOfCollections.TrimExcess()
            End If

            'Enable/Disable Controls
            Me.EnableControlsMaintenance()

            'Compute total
            Me.ComputeTotal()

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            Me.EnableControls(False, True, False, False, False, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Not _Login.HasAccess(EnumAMSModulesFinal.CollEntryAllocationWindow.ToString) Then
            MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Access Collection Entry Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
            Exit Sub
        End If
        Try
            'Resize the form
            Dim frm As New frmCollectionMgt
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 866
                valSize.Height = 196
                .Size = valSize
                .LoadType = frmCollectionMgt.CollectionLoadType.Add
                .Itemcollection = New Collection()
                .ShowDialog()
            End With

            'Compute Total
            Me.ComputeTotal()

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            Me.EnableControls(False, True, False, False, False, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        'If Not _Login.HasAccess(EnumAMSModules.AMS_CollectionEntryWindow.ToString) Then
        '    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_CollectionEntryWindow.ToString, "Access Collection Entry Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)        
        '    Exit Sub
        'End If
        Try
            If Me.DGridViewCollection.Rows.Count = 0 Then
                MsgBox("No record to edit!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            ElseIf CInt(Me.DGridViewCollection.CurrentRow.Cells("colStatus").Value) = EnumCollectionStatus.PreAllocated Then
                MsgBox("Cannot edit if the collection's status is PreAllocated!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            End If

            'Uncheck the allocate column
            Me.DGridViewCollection.CurrentRow.Cells("colAllocate").Value = False

            'Get the selected collection
            Dim item = WBillHelper.GetCollections(CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value)).First()

            If item.Status = EnumCollectionStatus.Allocated Then
                MsgBox("Allocated record cannot be edited!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
                'ElseIf item.DailyBatchCode.ToString().Trim().Length <> 0 Then
                '    MsgBox("Cannot edit, the collection selected was already posted to daily collection!", MsgBoxStyle.Exclamation, "Warning")
                '    Exit Sub
            ElseIf item.Status = EnumCollectionStatus.Cancelled Then
                MsgBox("Cancelled record cannot be edited!", MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            End If

            Dim frm As New frmCollectionMgt
            With frm
                .LoadType = frmCollectionMgt.CollectionLoadType.Edit
                .Itemcollection = item
                .ShowDialog()
            End With

            'Compute Total
            Me.ComputeTotal()

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            Me.EnableControls(False, True, False, False, False, False)
            Me.EnableControlsMaintenance()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        'If Not _Login.HasAccess(EnumAMSModules.AMS_CollectionDeleteWindow.ToString) Then
        '    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_CollectionDeleteWindow.ToString, "Access Collection Delete Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
        '    Exit Sub
        'End If

        If Me.DGridViewCollection.Rows.Count = 0 Then
            MsgBox("No record to delete!", MsgBoxStyle.Exclamation, "No Data")
            Exit Sub
        End If

        Try
            Dim item As New Collection
            item = WBillHelper.GetCollections(CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value)).First()

            If item.IsPosted = EnumIsPosted.Posted Then
                MsgBox("Cannot delete posted collection!", MsgBoxStyle.Exclamation, "Denied")
                Exit Sub
            ElseIf item.Status = EnumCollectionStatus.Allocated Then
                MsgBox("Please untag first the collection!", MsgBoxStyle.Exclamation, "Denied")
                Exit Sub
            End If

            Dim ans = MsgBox("Do you really want to delete this record?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Delete")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim itemJV As New JournalVoucher
            Dim itemGPPosted As New WESMBillGPPosted

            If item.DailyBatchCode.Length <> 0 Then
                itemJV = BFactory.GenerateDeleteORJournalVoucher(item, WBillHelper.GetSignatories("JV").First())
                itemGPPosted = BFactory.GenerateDeleteORGPPosted(itemJV)
            End If

            WBillHelper.SaveCollection(item, itemJV, itemGPPosted)
            Me.DGridViewCollection.Rows.RemoveAt(Me.DGridViewCollection.CurrentRow.Index)

            MsgBox("Successfully Deleted!", MsgBoxStyle.Information, "Deleted")

            'Updated By 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Record Deleted: Collection No. " & item.CollectionNumber, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccssfullyDeleted.ToString, AMModule.UserName)

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            Me.EnableControls(False, True, False, False, False, False)
            Me.EnableControlsMaintenance()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

    Private Sub DGridViewCollection_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewCollection.CellClick
        If Me.DGridViewCollection.Rows.Count = 0 Then
            Exit Sub
        End If

        'Enable/Disable Controls
        Me.EnableControlsMaintenance()

        'Display collection allocation
        Me.DisplayCollectionAllocationOnGrid()
    End Sub

    Private Sub DGridView_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGridViewCollection.CellFormatting
        If Me.DGridViewCollection.Columns(e.ColumnIndex).Name = "colStatus" Then
            If e.Value.ToString() = EnumCollectionStatus.Allocated.ToString() Then
                Me.DGridViewCollection.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Drawing.Color.Red
            End If
        End If

        'Disable rows which are already allocated
        If e.ColumnIndex = 12 Then
            With Me.DGridViewCollection.Rows(e.RowIndex)
                If CType(.Cells("colStatus").Value, EnumCollectionStatus) = EnumCollectionStatus.NotAllocated Then
                    Me.DGridViewCollection.Rows(e.RowIndex).Cells("colAllocate").ReadOnly = False
                Else
                    Me.DGridViewCollection.Rows(e.RowIndex).Cells("colAllocate").ReadOnly = True
                End If
            End With
        End If
    End Sub

    Private Sub btnAllocate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllocate.Click
        'If Not _Login.HasAccess(EnumAMSModules.AMS_CollectionAllocationWindow.ToString) Then
        '    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_CollectionAllocationWindow.ToString, "Access Collection Allocation Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)
        '    Exit Sub
        'End If

        If Not Me.Validation Then
            MsgBox("Please check first the collection/s to allocate!", MsgBoxStyle.Exclamation, "Warning")
            Exit Sub
        End If

        Try
            'Check if the collection is already posted in daily collection
            For i As Integer = 0 To Me.DGridViewCollection.RowCount - 1
                If CBool(Me.DGridViewCollection.Rows(i).Cells("colAllocate").Value) = True Then
                    If Me.DGridViewCollection.Rows(i).Cells("colDailyBatchCode").Value.ToString().Length = 0 Then

                        'Check in the database if the collection is truly not posted
                        Dim colID = CLng(Me.DGridViewCollection.Rows(i).Cells("colID").Value)
                        Dim itemCol = WBillHelper.GetCollections(colID).First()

                        If itemCol.DailyBatchCode.Length = 0 Then
                            Dim ORNO = CStr(Me.DGridViewCollection.Rows(i).Cells("colORNo").Value)
                            MsgBox("OR No " & ORNO & " is not yet posted for daily collection.", MsgBoxStyle.Exclamation, "Not yet posted")
                            Exit Sub
                        End If
                    End If
                End If
            Next

            'Get the collection date of the first collection and set to the default value of allocation date
            Dim itemCollectionDate As Date = Me.GetUnAllocatedCollectionsOnTheGrid(0).CollectionDate

            'Selection of allocation date
            Dim frm As New frmCollectionSearch
            With frm
                Dim valSize As New System.Drawing.Size
                valSize.Width = 343
                valSize.Height = 130

                .Size = valSize
                .LoadType = frmCollectionSearch.EnumFunctionType.SelectAllocationDate
                .dtAllocationDate.Value = itemCollectionDate

                If frm.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    Exit Sub
                End If
            End With

            Me._AllocationDate = CDate(FormatDateTime(frm.dtAllocationDate.Value, DateFormat.ShortDate))

            'Check if Allocation Date is already exist in AM_PAYMENT
            If WBillHelper.GetCountAMPayment(Me._AllocationDate) <> 0 Then
                MsgBox(Me._AllocationDate.ToString("MM/dd/yyyy") & " was already use in Payment Allocation. " & _
                       "Please select another allocaton date!", MsgBoxStyle.Exclamation, "Invalid Date")
                Exit Sub
            End If

            Dim listDates = (From x In Me.GetUnAllocatedCollectionsOnTheGrid() _
                             Select x.CollectionDate Distinct Order By CollectionDate).ToList()


            'Check if the collections date are greater than allocation date
            If listDates.Count > 0 Then
                If listDates(0) > Me._AllocationDate Then
                    MsgBox("Allocation date must be greater than or equal to collection date/s!", MsgBoxStyle.Exclamation, "Change the allocation date")
                    Exit Sub
                End If
            End If

            ProgressThread.Show("Please wait while processing.")

            'Get the daily interest rates
            Me._dicInterestRate = WBillHelper.GetDailyInterestRate()

            'Get the WESM Bill Summary for allocation
            Dim listWESMBillSummary = WBillHelper.GetWESMBillSummaryForCollectionAllocation()

            Dim listDatesSummary = (From x In listWESMBillSummary _
                                    Select x.NewDueDate Distinct Order By NewDueDate).ToList()

            'Check if the due dates are greater than allocation date
            If listDatesSummary.Count > 0 Then
                If listDatesSummary(0) > Me._AllocationDate Then
                    MsgBox("Allocation date must be greater than or equal to due date/s!", MsgBoxStyle.Exclamation, "Change the allocation date")
                    Exit Sub
                End If
            End If

            Dim item As New FuncAutoCollectionAllocation()
            With item
                .AllocationDate = Me._AllocationDate
                .DicInterestRate = Me._dicInterestRate
                .ListCollections = Me.GetUnAllocatedCollectionsOnTheGrid()
                .ListPrudentials = WBillHelper.GetParticipantsPrudential()
                .ListParticipants = WBillHelper.GetAMParticipants()
                .ListWESMBillSummaries = listWESMBillSummary
                .ListDocSignatories = WBillHelper.GetSignatories()
                .ListCollectionMonitoring = WBillHelper.GetCollectionMonitoring(EnumCollectionMonitoringType.TransferToHeldCollection)
            End With


            'Generate the Collection Allocation
            Me._CollectionOfResult = item.GenerateCollectionAllocation()

            'Add the collections for drawdown
            Dim itemsDrawdown = From x In Me._CollectionOfResult.ListCollections _
                                Where x.CollectionCategory = EnumCollectionCategory.Drawdown _
                                Select x

            'Create the temporary DMCM. This will use for previewing the DMCM report
            Me._ListDMCM = Me.BFactory.GenerateCollectionDMCM(Me._CollectionOfResult.ListCollections, _
                                                              Me.WBillHelper.GetSignatories("DMCM").First, _
                                                              Me.WBillHelper.GetDailyInterestRate())

            'Create the temporary DMCM Drawdown. This will use for previewing the DMCM report
            Me._ListDMCMDrawdown = Me.BFactory.GenerateDMCMDrawdown(Me._CollectionOfResult.ListCollections, _
                                                                    Me.WBillHelper.GetSignatories("DMCM").First, _
                                                                    Me.WBillHelper.GetDailyInterestRate())

            'Display the drawdown
            Me.DisplayOnGrid(itemsDrawdown.ToList())

            'Update the status from NotAllocated into PreAllocated
            For index As Integer = 0 To Me.DGridViewCollection.RowCount - 1
                With Me.DGridViewCollection.Rows(index)
                    Dim colNumber = CLng(.Cells("colID").Value)

                    Dim itemCol = (From x In Me._CollectionOfResult.ListCollections _
                                   Where x.CollectionNumber = colNumber _
                                   Select x).ToList()

                    If itemCol.Count > 0 Then
                        .Cells("colHeld").Value = FormatNumber(Math.Abs(itemCol.First.CollectedHeld), 2)
                        .Cells("colAmountForAllocation").Value = FormatNumber(CDec(.Cells("colAmountForAllocation").Value) _
                                                                              + Math.Abs(CDec(.Cells("colHeld").Value)), 2)
                        .Cells("colDateAllocated").Value = itemCol.First.AllocationDate.ToString("MM/dd/yyyy")
                        .Cells("colStatus").Value = EnumCollectionStatus.PreAllocated
                        .Cells("colAllocate").Value = True
                        .Cells("colAllocate").ReadOnly = True
                    End If                   

                End With
            Next

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            MsgBox("Successfully Allocated!", MsgBoxStyle.Information, "Success")


            Me.EnableControls(True, False, True, True, True, True)
            Me.btnSave.Select()

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
            Me.Close()
        Finally
            ProgressThread.Close()
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Try
        Dim listAllocDate = (From x In Me._CollectionOfResult.ListCollections _
                             Select x.AllocationDate Distinct).ToList()

        If listAllocDate.Count > 1 Then
            MsgBox("Can not save collections with multiple allocation date!", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        End If
        Dim selectedAllocationDate As Date = listAllocDate.First()

        Dim ans As MsgBoxResult
        ans = MsgBox("Do you really want to save allocated collections?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        'Selection of JV date
        Dim frmJVDate As New frmCollectionSearch
        With frmJVDate
            Dim valSize As New System.Drawing.Size
            valSize.Width = 343
            valSize.Height = 130

            .Size = valSize
            .LoadType = frmCollectionSearch.EnumFunctionType.SelectJVDate

            If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
        End With
        Dim JVDate = CDate(FormatDateTime(frmJVDate.dtAllocationDate.Value, DateFormat.ShortDate))

        Dim listPRFinal As New List(Of Prudential)
        Dim listPRHistoryFinal As New List(Of PrudentialHistory)
        Dim listMonitoringFinal As New List(Of CollectionMonitoring)

        Dim listExcessCollection = (From x In Me._CollectionOfResult.ListCollectionMonitoring _
                                    Select x).ToList()

        Dim listPR = (From x In Me._CollectionOfResult.ListPrudentials _
                      Select x).ToList()

        Dim listPRHistory = (From x In Me._CollectionOfResult.ListPrudentialsHistory _
                             Select x).ToList()

        Dim cntExcess = (From x In listExcessCollection _
                         Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                         Select x).Count()

        If cntExcess > 0 Then
            Dim frm As New frmTransferAdvancePayment
            With frm
                .AllocationDate = selectedAllocationDate
                .AllocationType = EnumAllocationType.Automatic
                .ListCollectionMonitoring = CType(BFactory.CloneObject(listExcessCollection), List(Of CollectionMonitoring))
                .ListPrudentials = CType(BFactory.CloneObject(listPR), List(Of Prudential))
                .ListPrudentialHistory = CType(BFactory.CloneObject(listPRHistory), List(Of PrudentialHistory))
                If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    listPRFinal = .ListPrudentials
                    listPRHistoryFinal = .ListPrudentialHistory
                    listMonitoringFinal = .ListCollectionMonitoring
                Else
                    Exit Sub
                End If
            End With
        Else
            With Me._CollectionOfResult
                listPRFinal = .ListPrudentials
                listPRHistoryFinal = .ListPrudentialsHistory
                listMonitoringFinal = .ListCollectionMonitoring
            End With
        End If

        'Update the allocated collections into Allocated word
        For Each item In Me._CollectionOfResult.ListCollections
            item.Status = EnumCollectionStatus.Allocated
            item.IsPosted = EnumIsPosted.Posted
        Next

        With Me._CollectionOfResult

            'Get the WESM Bill Sales and Purchases
            Dim listInvoiceNo As New List(Of String)
            Dim listWESMBillSalesAndPurchases As New List(Of WESMBillSalesAndPurchased)
            For Each item In .ListWESMBillSummaries
                If item.SummaryType = EnumSummaryType.INV And (item.ChargeType = EnumChargeType.E Or item.ChargeType = EnumChargeType.EV) Then
                    listInvoiceNo.Add(item.INVDMCMNo)
                End If
            Next
            listInvoiceNo.TrimExcess()

            If listInvoiceNo.Count > 0 Then
                listWESMBillSalesAndPurchases.AddRange(WBillHelper.GetWESMInvoiceSalesAndPurchased(listInvoiceNo))
                listWESMBillSalesAndPurchases.TrimExcess()
            End If

            'Generate DMCM Setup
            Dim listDMCMSetup = Me.BFactory.GenerateCollectionDMCM(.ListCollections, _
                                                                   Me.WBillHelper.GetSignatories("DMCM").First, _
                                                                   Me.WBillHelper.GetDailyInterestRate())

            'Generate the Official Receipt
            Dim listOR = Me.BFactory.GenerateCollectionOR(.ListCollections, listMonitoringFinal, listWESMBillSalesAndPurchases, New AMParticipants)

            'Generate the DMCM Drawdown
            Dim listDMCMDrawdown = Me.BFactory.GenerateDMCMDrawdown(.ListCollections, _
                                                                    Me.WBillHelper.GetSignatories("DMCM").First, _
                                                                    Me.WBillHelper.GetDailyInterestRate())

            'Generate the FTF
            Dim listFTF = Me.BFactory.GenerateCollectionFTF(selectedAllocationDate, listMonitoringFinal, _
                                                            .ListCollections, Me.WBillHelper.GetSignatories("FTF").First)

            'Generate Journal Voucher
            Dim itemJV = Me.BFactory.GenerateCollectionJournalVoucher(listDMCMSetup, listDMCMDrawdown, listOR, listFTF, _
                                                                      Me.WBillHelper.GetSignatories("JV").First, _
                                                                      JVDate)

            'Generate the GP Posted
            Dim itemGPPosted = Me.BFactory.GenerateCollectionGPPosted(itemJV)

            

            'Generate the EFT
            Dim listEFT = Me.BFactory.GenerateCollectionEFT(listMonitoringFinal)

            'Save of data
            WBillHelper.SaveCollectionAllocations(.ListWESMBillSummaries, .ListCollections, _
                                                  listMonitoringFinal, listPRFinal, listPRHistoryFinal, _
                                                  listFTF, itemJV, itemGPPosted, listDMCMSetup, listDMCMDrawdown, listOR, listEFT)

            'Update collections in Me._ListOfCollections
            For Each item In Me._CollectionOfResult.ListCollections
                Me._ListOfCollections.Add(item)
            Next
            Me._ListOfCollections.TrimExcess()
        End With

        MsgBox("Successfully Saved!", MsgBoxStyle.Information, "Success")

        'Remove the drawdown first to update the collection number
        For index As Integer = Me.DGridViewCollection.RowCount - 1 To 0 Step -1
            With Me.DGridViewCollection.Rows(index)
                Dim typeCategory = CType(System.Enum.Parse(GetType(EnumCollectionCategory), CStr(.Cells("colType").Value)), EnumCollectionCategory)
                Dim status = CType(System.Enum.Parse(GetType(EnumCollectionStatus), CStr(.Cells("colStatus").Value)), EnumCollectionStatus)
                If typeCategory = EnumCollectionCategory.Drawdown And status = EnumCollectionStatus.PreAllocated Then
                    Me.DGridViewCollection.Rows.RemoveAt(index)
                End If
            End With
        Next

        'Get the drawdowns
        Dim itemsDrawdown = (From x In Me._CollectionOfResult.ListCollections _
                             Where x.CollectionCategory = EnumCollectionCategory.Drawdown _
                             Select x).ToList()

        'Display on the grid the drawdowns
        Me.DisplayOnGrid(itemsDrawdown)

        'Update the status from PreAllocated into Allocated
        For index As Integer = 0 To Me.DGridViewCollection.RowCount - 1
            With Me.DGridViewCollection.Rows(index)
                Dim typeStatus = CType(System.Enum.Parse(GetType(EnumCollectionStatus), CStr(.Cells("colStatus").Value)), EnumCollectionStatus)

                If typeStatus = EnumCollectionStatus.PreAllocated Then
                    .Cells("colStatus").Value = EnumCollectionStatus.Allocated
                    .Cells("colAllocate").Value = False
                    .Cells("colAllocate").ReadOnly = True
                    .Cells("colIsPosted").Value = EnumIsPosted.Posted
                End If

                'Updated By Lance 08/18/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Status: " & .Cells("colStatus").Value.ToString & "; Allocate: " & .Cells("colAllocate").Value.ToString, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

            End With
        Next

        'Compute Total
        Me.ComputeTotal()

        'Display the collection allocations of the current selected collection
        Me.DisplayCollectionAllocationOnGrid()

        Me.EnableControls(False, True, False, False, False, False)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        '    'Updated By Lance 08/18/2014
        '    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_CollectionWindow.ToString, ex.Message, "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.ErrorInSaving.ToString, AMModule.UserName)
        '    Me.Close()
        'End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRollBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRollBack.Click
        Dim ans = MsgBox("Do you really want to rollback the allocations?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "RollBack")

        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        Try
            'Removed Drawdown
            For index As Integer = Me.DGridViewCollection.RowCount - 1 To 0 Step -1
                With Me.DGridViewCollection.Rows(index)
                    Dim typeCategory = CType(System.Enum.Parse(GetType(EnumCollectionCategory), CStr(.Cells("colType").Value)), EnumCollectionCategory)
                    Dim status = CType(System.Enum.Parse(GetType(EnumCollectionStatus), CStr(.Cells("colStatus").Value)), EnumCollectionStatus)
                    If typeCategory = EnumCollectionCategory.Drawdown And status = EnumCollectionStatus.PreAllocated Then
                        Me.DGridViewCollection.Rows.RemoveAt(index)
                    End If
                End With
            Next

            'Update the status from PreAllocated into NotAllocated
            For index As Integer = 0 To Me.DGridViewCollection.RowCount - 1

                With Me.DGridViewCollection.Rows(index)
                    Dim status = CType(System.Enum.Parse(GetType(EnumCollectionStatus), CStr(.Cells("colStatus").Value)), EnumCollectionStatus)

                    If status = EnumCollectionStatus.PreAllocated Then
                        .Cells("colAmountForAllocation").Value = FormatNumber(CDec(.Cells("colAmountForAllocation").Value) _
                                                                              - CDec(.Cells("colHeld").Value), 2)
                        .Cells("colHeld").Value = "0.00"
                        .Cells("colStatus").Value = EnumCollectionStatus.NotAllocated
                        .Cells("colDateAllocated").Value = ""
                        .Cells("colAllocate").Value = False
                        .Cells("colAllocate").ReadOnly = True                        
                    End If
                    
                End With
            Next

            'Get the AM Participants
            Me._AMParticipants = WBillHelper.GetAMParticipantsAll()
            Me._CollectionOfResult = New FuncAutoCollectionAllocationResult()

            'Display collection allocation
            Me.DisplayCollectionAllocationOnGrid()

            MsgBox("Successfully rollbacked", MsgBoxStyle.Information, "Rollback")

            Me.EnableControls(False, True, False, False, False, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")            
        End Try
    End Sub

    Private Sub chckAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckAll.CheckedChanged
        For index As Integer = 0 To Me.DGridViewCollection.RowCount - 1
            With Me.DGridViewCollection.Rows(index)
                If Me.chckAll.CheckState = CheckState.Checked Then
                    If CType(.Cells("colStatus").Value, EnumCollectionStatus) = EnumCollectionStatus.NotAllocated _
                        And CLng(.Cells("colORNo").Value) <> 0 Then
                        .Cells("colAllocate").Value = True
                    End If
                Else
                    If CType(.Cells("colStatus").Value, EnumCollectionStatus) = EnumCollectionStatus.NotAllocated _
                        And CLng(.Cells("colORNo").Value) <> 0 Then
                        .Cells("colAllocate").Value = False
                    End If
                End If
            End With
        Next
    End Sub

    Private Sub btnPrintSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSummary.Click        
        Try
            ProgressThread.Show("Please wait while processing Summary Report.")
            Dim TotalCash As Decimal = 0, TotalDrawdown As Decimal = 0

            'Get the Signatories
            Dim Signatories = WBillHelper.GetSignatories("CS").First()

            'Get the date today
            Dim DocumentDate As String = SystemDate.ToString("MM/dd/yyyy") 'SystemDate.ToString("MM/dd/yyyy") 

            'Generate collection summary for manual
            Dim dt = BFactory.GenerateCollectionSummary("C-XXX", 1, Me._CollectionOfResult.ListCollections, _
                                                        Me._CollectionOfResult.ListCollectionMonitoring, Me._AMParticipants, _
                                                        New List(Of DebitCreditMemo), Signatories, TotalCash, _
                                                        TotalDrawdown, New DSReport.CollectionSummaryDataTable, DocumentDate)

            If dt.Rows.Count = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Exclamation, "No data")
                Exit Sub
            End If

            Dim frmViewer As New frmReportViewer()
            With frmViewer
                .LoadCollectionSummaryPerJV(dt, TotalCash, TotalDrawdown)
                ProgressThread.Close()
                .ShowDialog()
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)           
        End Try
    End Sub

    Private Sub btnJV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJV.Click        
        Try
            If Me._CollectionOfResult.ListCollections.Count = 0 Then
                MsgBox("Nothing to view!", MsgBoxStyle.Exclamation, "No data")
                Exit Sub
            End If

            ProgressThread.Show("Please wait while processing Summary Report.")

            'Get the Signatories for DMCM
            Dim SignatoriesDMCM = WBillHelper.GetSignatories("DMCM").First()

            'Get the Signatories for JV
            Dim SignatoriesJV = WBillHelper.GetSignatories("JV").First()

            'Get the daily interest
            Dim dicDailyInterest = WBillHelper.GetDailyInterestRate()

            With Me._CollectionOfResult
                'Generate DMCM Setup
                Dim listDMCMSetup = BFactory.GenerateCollectionDMCM(.ListCollections, SignatoriesDMCM, dicDailyInterest)

                'Generate the Official Receipt
                Dim listOR = BFactory.GenerateCollectionOR(.ListCollections, .ListCollectionMonitoring, New List(Of WESMBillSalesAndPurchased), New AMParticipants)

                'Generate the DMCM Drawdown
                Dim listDMCMDrawdown = BFactory.GenerateDMCMDrawdown(.ListCollections, SignatoriesDMCM, dicDailyInterest)

                'Generate the Fund Transfer Form
                Dim listFTF = Me.BFactory.GenerateCollectionFTF(Me._AllocationDate, .ListCollectionMonitoring, .ListCollections, _
                                                                Me.WBillHelper.GetSignatories("FTF").First)

                'Generate Journal Voucher
                Dim itemJV = BFactory.GenerateCollectionJournalVoucher(listDMCMSetup, listDMCMDrawdown, listOR, listFTF, _
                                                                       SignatoriesJV, SystemDate)

                'Generate dataset for Journal voucher
                Dim ds = BFactory.GenerateJournalVoucherReport(Me._ListAccountingCodes, itemJV, New DSReport.JournalVoucherDataTable, _
                                                               New DSReport.JournalVoucherDetailsDataTable, New DSReport.AccountingCodeDataTable)

                Dim frmViewer As New frmReportViewer()
                With frmViewer
                    frmViewer.LoadJournalVoucher(ds)
                    ProgressThread.Close()
                    .ShowDialog()
                End With
            End With
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)                       
        End Try
    End Sub

    Private Sub DGridViewAllocCollections_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewAllocCollections.CellContentClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If

            Select Case e.ColumnIndex
                Case 1
                    Dim dmcmNo = CLng(Me.DGridViewAllocCollections.Rows(e.RowIndex).Cells("colCreatedDocument").Value)

                    If dmcmNo = 0 Then
                        Exit Sub
                    End If

                    Dim colStatus = CType(System.Enum.Parse(GetType(EnumCollectionStatus), _
                                                            CStr(Me.DGridViewCollection.CurrentRow.Cells("colStatus").Value)),  _
                                                            EnumCollectionStatus)

                    Dim itemDMCM As New DebitCreditMemo
                    If colStatus = EnumCollectionStatus.PreAllocated Then
                        itemDMCM = (From x In Me._ListDMCM _
                                    Where x.DMCMNumber = dmcmNo _
                                    Select x).First()
                    Else
                        itemDMCM = WBillHelper.GetDebitCreditMemoMain(dmcmNo).First()
                    End If

                    Dim listDMCMNo As New List(Of Long)
                    Dim listDMCMMain As New List(Of DebitCreditMemo)

                    'Get the Accounting Codes
                    Dim listAccountCodes = WBillHelper.GetAccountingCodes()

                    'Get the Participants
                    Dim listParticipants = WBillHelper.GetAMParticipantsAll()

                    'Get the Signatory
                    Dim signatory = WBillHelper.GetSignatories("DMCM").First()

                    'Add the DMCM
                    listDMCMMain.Add(itemDMCM)

                    'Get the WESM Bill Sales and Purchase
                    Dim listWESMBillSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)

                    'Add the DMCM Number
                    listDMCMNo.Add(itemDMCM.DMCMNumber)

                    ProgressThread.Show("Please wait while processing.")

                    'Get the datasource for the report
                    Dim dt = BFactory.GenerateDMCMReport1(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCMMain, _
                                                         listAccountCodes, listParticipants, signatory, listWESMBillSalesAndPurchased)

                    Dim frmViewer As New frmReportViewer()
                    With frmViewer
                        .LoadDebitCreditMemo(dt)
                        ProgressThread.Close()
                        .ShowDialog()
                    End With

                Case 10
                    Dim summaryType = CType(System.Enum.Parse(GetType(EnumSummaryType), _
                                      CStr(Me.DGridViewAllocCollections.Rows(e.RowIndex).Cells("colSummaryType").Value)), EnumSummaryType)

                    Dim invDMCMNo = CStr(Me.DGridViewAllocCollections.Rows(e.RowIndex).Cells("colInvDMCMNo").Value)

                    Dim chargeType = CType(System.Enum.Parse(GetType(EnumChargeType), _
                                     CStr(Me.DGridViewAllocCollections.Rows(e.RowIndex).Cells("colChargeType").Value)), EnumChargeType)

                    Dim isDMCMChanged = CInt(Me.DGridViewAllocCollections.Rows(e.RowIndex).Cells("colIsDMCMChanged").Value)

                    'Load the WESM Invoice
                    If summaryType = EnumSummaryType.INV Then
                        ProgressThread.Show("Please wait while processing.")
                        Dim fileType As EnumFileType
                        Dim listWESMSalesAndPurchased As New List(Of WESMBillSalesAndPurchased)
                        Dim listWESMInvoice As New List(Of WESMInvoice)
                        Dim particpantID As New List(Of String)

                        'Get the WESM Invoice
                        listWESMInvoice = WBillHelper.GetWESMInvoices(invDMCMNo)

                        'Get the participant id
                        particpantID.Add(CStr(Me.DGridViewCollection.CurrentRow.Cells("colParticipantID").Value))

                        Dim chargeTypeValue As EnumChargeType
                        If chargeType = EnumChargeType.E Or chargeType = EnumChargeType.EV Then
                            fileType = EnumFileType.Energy

                            'Get the WESM Bill Sales and Purchased
                            listWESMSalesAndPurchased = WBillHelper.GetWESMInvoiceSalesAndPurchased(invDMCMNo)
                        Else
                            fileType = EnumFileType.MarketFees
                        End If

                        'Get the signatories for WESM Invoice
                        Dim Signatory = WBillHelper.GetSignatories("INV").First()

                        Dim getMarketFeesRate As String = ""

                        If fileType = EnumFileType.MarketFees Then
                            getMarketFeesRate = (From x In listWESMInvoice Select x.MarketFeesRate).FirstOrDefault.ToString
                        End If


                        'Get the dataset for WESM Invoice
                        Dim ds = BFactory.GenerateWESMInvoice(New DSReport.WESMInvoiceDataTable, New DSReport.WESMInvoiceDetailsDataTable, _
                                                              listWESMInvoice, listWESMSalesAndPurchased, Me._AMParticipants, _
                                                              Me._ListCharges, Me._ListCalendar, particpantID, Signatory, fileType)

                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadWESMInvoice(ds, chargeTypeValue, getMarketFeesRate, "This is system generated invoice no signature required.")
                            ProgressThread.Close()
                            .ShowDialog()
                        End With

                    Else
                        'Load the DMCM
                        Dim itemDMCM As List(Of DebitCreditMemo)

                        If isDMCMChanged = 0 Then
                            itemDMCM = WBillHelper.GetDebitCreditMemoMain(CLng(invDMCMNo))
                        Else
                            itemDMCM = (From x In Me._ListDMCM _
                                        Where x.DMCMNumber = CLng(invDMCMNo) _
                                        Select x).ToList()
                        End If

                        ProgressThread.Show("Please wait while processing DMCM.")

                        'GetWesm4ward the datasource for the report
                        Dim dt As DataTable = Me.LoadDMCM(itemDMCM.First())
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDebitCreditMemo(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    End If
            End Select
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try        
    End Sub

    Private Sub DGridViewCollection_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridViewCollection.CellContentClick
        Try
            If e.RowIndex = -1 Then
                Exit Sub
            End If
            Select Case e.ColumnIndex
                Case 3
                    Dim dmcmNo = CLng(Me.DGridViewCollection.Rows(e.RowIndex).Cells("colDMCMNo").Value)
                    Dim orNo = CLng(Me.DGridViewCollection.Rows(e.RowIndex).Cells("colORNo").Value)

                    If orNo <> 0 Then

                        'Get the collection Number
                        Dim collectionNumber = CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value)

                        'Get the OR Number
                        Dim ORNumber = CLng(Me.DGridViewCollection.CurrentRow.Cells("colORNo").Value)

                        'Get the Collectiom
                        Dim itemCollection = WBillHelper.GetCollections(collectionNumber).First()

                        Dim itemORReportMain As New OfficialReceiptReportMain
                        With itemORReportMain
                            .ItemOfficialReceipt = WBillHelper.GetOfficialReceipt(ORNumber)
                            .ListOfficialReceiptReportRawDetails = WBillHelper.GetOfficialReceiptRawDetails(ORNumber)
                            .ItemParticipant = WBillHelper.GetAMParticipants(.ItemOfficialReceipt.IDNumber).First()
                            .DefaultInterestRate = WBillHelper.GetDailyInterestRate()(itemORReportMain.ItemOfficialReceipt.ORDate)
                            .BIRPermitNumber = AMModule.BIRPermitNumber
                            .TotalPaymentInWords = BFactory.NumberConvert(.TotalPayment)
                            .ItemCollection = itemCollection
                        End With

                        Dim ORPEMC_Header As Integer = EnumHeaderType.Yes

                        Dim result = BFactory.GenerateOfficialReceiptReport(ORPEMC_Header, itemORReportMain, itemORReportMain.ItemParticipant, New DSReport.OfficialReceiptMainNewDataTable)
                        Dim getStatus As Integer = CInt(Me.DGridViewCollection.CurrentRow.Cells("colStatus").Value)
                        ProgressThread.Show("Please wait while processing OR Report.")
                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            If getStatus = 0 Then
                                .LoadORCancelled(result, WBillHelper.GetSystemDateTime)
                            Else
                                .LoadOR(result)
                            End If
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    ElseIf dmcmNo <> 0 Then
                        Dim colStatus = CType(System.Enum.Parse(GetType(EnumCollectionStatus), _
                                                                CStr(Me.DGridViewCollection.CurrentRow.Cells("colStatus").Value)),  _
                                                                EnumCollectionStatus)

                        Dim listDMCMMain As New List(Of DebitCreditMemo)
                        Dim itemDMCM As New DebitCreditMemo

                        If colStatus = EnumCollectionStatus.PreAllocated Then
                            itemDMCM = (From x In Me._ListDMCMDrawdown _
                                        Where x.DMCMNumber = dmcmNo _
                                        Select x).First()

                        Else
                            itemDMCM = WBillHelper.GetDebitCreditMemoMain(dmcmNo).First()
                        End If

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

                        ProgressThread.Show("Please wait while processing OR Report.")

                        'Get the datasource for the report
                        Dim dt = BFactory.GenerateDMCMReport1(listDMCMNo, New DSReport.DebitCreditMemoDataTable, listDMCMMain, _
                                                             listAccountCodes, listParticipants, signatory, listWESMBillSalesAndPurchased)

                        Dim frmViewer As New frmReportViewer()
                        With frmViewer
                            .LoadDebitCreditMemo(dt)
                            ProgressThread.Close()
                            .ShowDialog()
                        End With
                    End If
            End Select

        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "System Message", MessageBoxButtons.OK, MessageBoxIcon.Error)        
        End Try
    End Sub

    Private Sub DGridViewCollection_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) _
                                        Handles DGridViewCollection.KeyDown, DGridViewCollection.KeyUp

        If Me.DGridViewCollection.RowCount = 0 Then
            Exit Sub
        End If

        'Display collection allocation
        Me.DisplayCollectionAllocationOnGrid()
    End Sub

    Private Sub btnUnTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnTag.Click
        'If Not _Login.HasAccess(EnumAMSModules.AMS_UntagCollectionWindow.ToString) Then
        '    MessageBox.Show("Access Denied", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_UntagCollectionWindow.ToString, "Access Untag Collection Window", "", "", CType(EnumColorCode.Red, ColorCode), EnumLogType.NoAccess.ToString, AMModule.UserName)        
        '    Exit Sub
        'End If

        If Me.DGridViewCollection.Rows.Count = 0 Then
            MsgBox("No record to untag!", MsgBoxStyle.Exclamation, "No Data")
            Exit Sub
        End If

        Try
            'Get the collection
            Dim item As New Collection
            item = WBillHelper.GetCollections(CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value)).First()

            If item.IsPosted = EnumIsPosted.Posted Then
                MsgBox("Cannot untag posted collection!", MsgBoxStyle.Exclamation, "Denied")
                Exit Sub
            ElseIf item.Status <> EnumCollectionStatus.Allocated Then
                MsgBox("Cannot untag unallocated collection!", MsgBoxStyle.Exclamation, "Denied")
                Exit Sub
            End If

            'Get the collection monitoring
            Dim listColMon = WBillHelper.GetCollectionMonitoring(item.CollectionNumber, _
                                                                 EnumCollectionMonitoringType.TransferToHeldCollection)
            'Check if the held collection where already tag
            If listColMon.Count > 0 Then
                If listColMon.First.CollectionNoTag <> 0 And item.ORNo <> 0 Then
                    Dim itemCollection = WBillHelper.GetCollections(listColMon.First.CollectionNoTag).First()

                    MsgBox("The held collection of the selected collection was applied in OR " & itemCollection.ORNo.ToString() & "!, " & _
                           "Please delete first OR Number", MsgBoxStyle.Exclamation, "Denied")
                    Exit Sub
                End If
            End If

            'Get the list of invoices that within the collection allocation for untagging - updated by lance 05/28/2019
            Dim dicCollAlloc As Dictionary(Of Long, Date) = WBillHelper.GetDicCollectionAllocation(CStr(Me.DGridViewCollection.CurrentRow.Cells("colDateAllocated").Value), CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value))            
            For Each dicItem As KeyValuePair(Of Long, Date) In dicCollAlloc
                Dim getCollAllocList As Dictionary(Of Long, Long) = WBillHelper.GetCollectionAllocationList(CStr(Me.DGridViewCollection.CurrentRow.Cells("colDateAllocated").Value), CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value), dicItem.Value)
                If getCollAllocList.ContainsKey(dicItem.Key) Then
                    MsgBox("To untag this selected collection, please untag first the OR No. " & CStr(getCollAllocList.Item(dicItem.Key)) & "! ", MsgBoxStyle.Exclamation, "Denied")
                    Exit Sub
                End If
            Next

            Dim ans = MsgBox("Do you really want to untag this collection?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Delete")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim itemJV As New JournalVoucher
            Dim itemGPPosted As New WESMBillGPPosted

            'Get the WESM Bill Summary History
            Dim listWESMBillSummaryHistory = WBillHelper.GetWESMBillSummaryHistory(item.CollectionNumber)
            Dim listCollectionMonitoring = WBillHelper.GetCollectionMonitoring(item.CollectionNumber)
            If item.CollectionCategory = EnumCollectionCategory.Cash Then                
                WBillHelper.UnTagCashCollection(item, listWESMBillSummaryHistory, listCollectionMonitoring)

                With Me.DGridViewCollection.CurrentRow
                    .Cells("colPrudentialReplenishment").Value = 0
                    .Cells("colHeld").Value = 0
                    .Cells("colCollected").Value = item.CollectedAmount
                    .Cells("colAmountForAllocation").Value = item.CollectedAmount
                    .Cells("colAllocationType").Value = EnumAllocationType.Automatic
                    .Cells("colStatus").Value = EnumCollectionStatus.NotAllocated
                    .Cells("colAllocate").Value = False
                    .Cells("colIsPosted").Value = EnumIsPosted.NotPost
                    .Cells("colDateAllocated").Value = ""
                End With

                'Removed if the collection is purely held collection
                If item.CollectedAmount = 0 And item.CollectedHeld <> 0 Then
                    Me.DGridViewCollection.Rows.RemoveAt(Me.DGridViewCollection.CurrentRow.Index)
                Else
                    Me.DGridViewCollection.Rows(Me.DGridViewCollection.CurrentRow.Index).DefaultCellStyle.ForeColor = Drawing.Color.Black
                End If
            Else
                WBillHelper.UnTagDrawdown(item, listWESMBillSummaryHistory)
                Me.DGridViewCollection.Rows.RemoveAt(Me.DGridViewCollection.CurrentRow.Index)
            End If

            'Clear Collection Allocation
            Me.DisplayCollectionAllocationOnGrid()

            MsgBox("Successfully untagged!", MsgBoxStyle.Information, "Untagging")
            'Updated By Lance 08/18/2014
            _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.CollEntryAllocationWindow.ToString, "Record Untagging: Collection No:" & item.CollectionNumber, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyUntag.ToString, AMModule.UserName)

            Me.EnableControls(False, True, False, False, False, False)
            Me.EnableControlsMaintenance()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")           
        End Try
    End Sub

#Region "Methods/Functions"

    Private Sub DisplayOnGrid(ByVal collectionItems As List(Of Collection))

        Try
            'Get all participants whether in-actice
            Dim listParticipants = Me.WBillHelper.GetAMParticipantsAll()

            Dim items = From x In collectionItems Join y In listParticipants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x.CollectionNumber, x.ORNo, x.DMCMNumber, x.CollectionDate, x.IDNumber, y.ParticipantID, _
                               x.CollectedAmount, x.CollectedPrudential, x.CollectedHeld, x.CollectionCategory, _
                               x.AllocationType, x.Status, x.IsPosted, x.AllocationDate, x.DailyBatchCode _
                        Order By ParticipantID

            For Each item In items
                Dim createdDocument As String = ""

                If item.DMCMNumber <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(item.DMCMNumber, BIRDocumentsType.DMCM)
                End If

                If item.ORNo <> 0 Then
                    createdDocument = BFactory.GenerateBIRDocumentNumber(item.ORNo, BIRDocumentsType.OfficialReceipt)
                End If

                With item
                    Me.DGridViewCollection.Rows.Add(.CollectionNumber, .ORNo, .DMCMNumber, createdDocument, _
                                                    FormatDateTime(.CollectionDate, DateFormat.ShortDate), _
                                                    .IDNumber, .ParticipantID, .CollectedAmount, _
                                                    .CollectedPrudential, .CollectedHeld, _
                                                    (.CollectedAmount + .CollectedHeld - .CollectedPrudential), _
                                                    .CollectionCategory, .AllocationType, .Status, False, _
                                                    .IsPosted, If(.AllocationDate <> New Date(), .AllocationDate.ToString("MM/dd/yyyy"), ""), _
                                                    .DailyBatchCode)
                End With
            Next

        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub DisplayCollectionAllocationOnGrid()
        Me.DGridViewAllocCollections.Rows.Clear()

        If Me.DGridViewCollection.RowCount = 0 Then
            Exit Sub
        End If

        Dim colNumber As Long = CLng(Me.DGridViewCollection.CurrentRow.Cells("colID").Value)
        Dim listCol = WBillHelper.GetCollections(colNumber)
        Dim itemCol As New Collection

        If listCol.Count <> 0 Then
            itemCol = listCol.First()
        Else
            Exit Sub
        End If

        Dim status = CType(System.Enum.Parse(GetType(EnumCollectionStatus), _
                     CStr(Me.DGridViewCollection.CurrentRow.Cells("colStatus").Value)), EnumCollectionStatus)

        Dim items = New List(Of CollectionAllocation)

        If status = EnumCollectionStatus.PreAllocated Then
            items = (From x In Me._CollectionOfResult.ListCollectionAllocation _
                     Where x.CollectionNumber = colNumber _
                     Select x Order By x.CollectionType).ToList()

        ElseIf itemCol.Status = EnumCollectionStatus.Allocated Then
            Dim itemsCollectionAllocation = WBillHelper.GetCollectionAllocation(colNumber)

            items = (From x In itemsCollectionAllocation _
                     Where x.CollectionNumber = colNumber _
                     Select x Order By x.CollectionType).ToList()
        Else
            Exit Sub
        End If

        For Each item In items
            With item
                Dim DMCMValue As String
                Dim ReferenceNumber As String

                If .DMCMNumber <> 0 Then
                    DMCMValue = BFactory.GenerateBIRDocumentNumber(.DMCMNumber, BIRDocumentsType.DMCM)
                Else
                    DMCMValue = ""
                End If

                If .ReferenceType = EnumSummaryType.DMCM Then
                    ReferenceNumber = BFactory.GenerateBIRDocumentNumber(CLng(.ReferenceNumber), BIRDocumentsType.DMCM)
                Else
                    ReferenceNumber = .ReferenceNumber
                End If

                Dim DueDate As Date
                If item.CollectionType = EnumCollectionType.Energy Or item.CollectionType = EnumCollectionType.VatOnEnergy _
                    Or item.CollectionType = EnumCollectionType.MarketFees Or item.CollectionType = EnumCollectionType.VatOnMarketFees Then
                    DueDate = item.DueDate
                Else
                    DueDate = item.NewDueDate
                End If



                Me.DGridViewAllocCollections.Rows.Add(.DMCMNumber, DMCMValue, .WESMBillSummaryNo.WESMBillSummaryNo, .CollectionNumber, _
                                                      .ReferenceNumber, .ReferenceType, .WESMBillSummaryNo.ChargeType, _
                                                      .BillingPeriod, .CollectionType.ToString(), DueDate.ToString("MM/dd/yyyy"), _
                                                      ReferenceNumber, _
                                                      .EndingBalance, .Amount, .IsDMCMChanged)
            End With
        Next

        Dim listColMon As New List(Of CollectionMonitoring)

        If status = EnumCollectionStatus.PreAllocated Then
            listColMon = (From x In Me._CollectionOfResult.ListCollectionMonitoring _
                          Where x.CollectionNo = colNumber _
                          Select x).ToList()
        Else
            listColMon = WBillHelper.GetCollectionMonitoring(colNumber)
        End If


        'Exclude drawdown in displaying of collection allocation
        Dim listColMonFinal = From x In listColMon _
                              Where x.TransType <> EnumCollectionMonitoringType.TransferToPRDrawdown _
                              Select x

        For Each item In listColMonFinal
            With item
                Me.DGridViewAllocCollections.Rows.Add("", "", "", .CollectionNo, "", "", "", _
                                                      "", .TransType.ToString(), "", _
                                                      "", "", .Amount, "")
            End With
        Next

    End Sub

    Private Function GetUnAllocatedCollectionsOnTheGrid() As List(Of Collection)
        Dim result As New List(Of Collection)

        Try
            For row = 0 To Me.DGridViewCollection.Rows.Count - 1
                Dim item As New Collection

                With Me.DGridViewCollection.Rows(row)
                    If CBool(.Cells("colAllocate").Value) = True And _
                            CType(System.Enum.Parse(GetType(EnumCollectionCategory), CStr(.Cells("colType").Value)),  _
                            EnumCollectionCategory) = EnumCollectionCategory.Cash Then

                        item.CollectionNumber = CLng(.Cells("colID").Value)
                        item.ORNo = CLng(.Cells("colORNo").Value)
                        item.CollectionDate = CDate(.Cells("colCollectionDate").Value)
                        item.IDNumber = CStr(.Cells("colIDNumber").Value)
                        item.CollectedAmount = CDec(.Cells("colCollected").Value)
                        item.CollectedPrudential = CDec(.Cells("colPrudentialReplenishment").Value)
                        item.CollectedHeld = CDec(.Cells("colHeld").Value)
                        item.AmountForAllocation = CDec(.Cells("colAmountForAllocation").Value)
                        item.CollectionCategory = CType(System.Enum.Parse(GetType(EnumCollectionCategory), CStr(.Cells("colType").Value)), EnumCollectionCategory)
                        item.AllocationType = CType(System.Enum.Parse(GetType(EnumAllocationType), CStr(.Cells("colAllocationType").Value)), EnumAllocationType)
                        item.Status = CType(System.Enum.Parse(GetType(EnumCollectionStatus), CStr(.Cells("colStatus").Value)), EnumCollectionStatus)
                        item.IsPosted = CType(System.Enum.Parse(GetType(EnumIsPosted), CStr(.Cells("colIsPosted").Value)), EnumIsPosted)
                        item.BatchCode = CStr(.Cells("colDailyBatchCode").Value.ToString())

                        If Not IsDBNull(.Cells("colCollectionDate").Value) Then
                            item.AllocationDate = CDate(.Cells("colCollectionDate").Value)
                        End If

                        result.Add(item)
                    End If
                End With
            Next
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try

        result.TrimExcess()
        Return result
    End Function

    Private Sub EnableControls(ByVal vFake As Boolean, ByVal vAllocateCollection As Boolean, _
                               ByVal vSave As Boolean, ByVal vRollBack As Boolean, ByVal vCollectionSummary As Boolean, _
                               ByVal vJV As Boolean)
        Me.gpFake.Visible = vFake
        Me.btnAllocate.Enabled = vAllocateCollection
        Me.btnSave.Enabled = vSave
        Me.btnRollBack.Enabled = vRollBack
        Me.btnPrintSummary.Enabled = vCollectionSummary
        Me.btnJV.Enabled = vJV
    End Sub

    Private Sub EnableControlsMaintenance()
        If Me.DGridViewCollection.Rows.Count = 0 Then
            Exit Sub
        End If

        If CInt(Me.DGridViewCollection.Rows(Me.DGridViewCollection.CurrentRow.Index).Cells("colIsPosted").Value) = EnumIsPosted.Posted Then
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnUnTag.Enabled = False

        ElseIf CInt(Me.DGridViewCollection.Rows(Me.DGridViewCollection.CurrentRow.Index).Cells("colStatus").Value) = EnumCollectionStatus.Allocated Then
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnUnTag.Enabled = True

        ElseIf CInt(Me.DGridViewCollection.Rows(Me.DGridViewCollection.CurrentRow.Index).Cells("colStatus").Value) = EnumCollectionStatus.NotAllocated Then
            Me.btnEdit.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnUnTag.Enabled = False
        End If
    End Sub

    Private Function Validation() As Boolean
        If Me.DGridViewCollection.RowCount = 0 Then
            Exit Function
        End If

        For i As Integer = 0 To Me.DGridViewCollection.RowCount - 1
            If CBool(Me.DGridViewCollection.Rows(i).Cells("colAllocate").Value) = True Then
                Return True
                Exit Function
            End If
        Next
    End Function

    Private Sub ComputeTotal()
        Dim listColAlloc As New List(Of CollectionAllocation)

        For Each item In Me._ListOfCollections
            listColAlloc.AddRange(item.ListOfCollectionAllocation)
        Next

        listColAlloc.TrimExcess()

        Dim totalDIMF = (From x In listColAlloc _
                         Where x.CollectionType = EnumCollectionType.DefaultInterestOnMF _
                         Select x.Amount).Sum()

        Dim totalDIVatOnMF = (From x In listColAlloc _
                              Where x.CollectionType = EnumCollectionType.DefaultInterestOnVatOnMF _
                              Select x.Amount).Sum()

        Dim totalMF = (From x In listColAlloc _
                       Where x.CollectionType = EnumCollectionType.MarketFees _
                       Select x.Amount).Sum()

        Dim totalVatOnMF = (From x In listColAlloc _
                            Where x.CollectionType = EnumCollectionType.VatOnMarketFees _
                            Select x.Amount).Sum()

        Dim totalDIEnergy = (From x In listColAlloc _
                             Where x.CollectionType = EnumCollectionType.DefaultInterestOnEnergy _
                             Select x.Amount).Sum()

        Dim totalEnergy = (From x In listColAlloc _
                           Where x.CollectionType = EnumCollectionType.Energy _
                           Select x.Amount).Sum()

        Dim totalVatOnEnergy = (From x In listColAlloc _
                                Where x.CollectionType = EnumCollectionType.VatOnEnergy _
                                Select x.Amount).Sum()

        Me.txtTotalDIMF.Text = FormatNumber(totalDIMF, 2)
        Me.txtTotalDIVatMF.Text = FormatNumber(totalDIVatOnMF, 2)
        Me.txtTotalMF.Text = FormatNumber(totalMF, 2)
        Me.txtTotalVatMF.Text = FormatNumber(totalVatOnMF, 2)
        Me.txtTotalDIEnergy.Text = FormatNumber(totalDIEnergy, 2)
        Me.txtTotalEnergy.Text = FormatNumber(totalEnergy, 2)
        Me.txtTotalVatEnergy.Text = FormatNumber(totalVatOnEnergy, 2)
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
            Dim InvDMCM As String
            With item
                Dim row As DataRow = dt.NewRow()
                row("ID_NUMBER") = itemDMCM.IDNumber.ToString()
                row("BUSINESS_STYLE") = itemParticipant.First.BusinessStyle
                row("ADDRESS") = itemParticipant.First.ParticipantAddress
                row("DMCM_NO") = BFactory.GenerateBIRDocumentNumber(itemDMCM.DMCMNumber, BIRDocumentsType.DMCM)
                row("JV_NO") = BFactory.GenerateBIRDocumentNumber(itemDMCM.JVNumber, BIRDocumentsType.JournalVoucher)
                row("PARTICULARS") = itemDMCM.Particulars
                row("ACCOUNT_CODE") = item.x.AccountCode

                If item.x.SummaryType = EnumSummaryType.INV Then
                    InvDMCM = item.x.InvDMCMNo
                Else
                    InvDMCM = BFactory.GenerateBIRDocumentNumber(CLng(item.x.InvDMCMNo), BIRDocumentsType.DMCM)
                End If
                If item.x.InvDMCMNo <> "" Then
                    row("DESCRIPTION") = "(" & InvDMCM & ") " & item.y.Description
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