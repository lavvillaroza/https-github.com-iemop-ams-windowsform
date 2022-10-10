'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmTransferAdvancePayment
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     January 10, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the transfer of Held Payment and Prudential
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   January 10, 2012        Vladimir E. Espiritu                 GUI design and basic functionalities    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmTransferAdvancePayment
    Private BFactory As BusinessFactory

#Region "Properties"
    Private _AllocationType As EnumAllocationType
    Public Property AllocationType() As EnumAllocationType
        Get
            Return _AllocationType
        End Get
        Set(ByVal value As EnumAllocationType)
            _AllocationType = value
        End Set
    End Property

    Private _ListCollectionMonitoring As List(Of CollectionMonitoring)
    Public Property ListCollectionMonitoring() As List(Of CollectionMonitoring)
        Get
            'Return CType(BFactory.CloneObject(_ListCollectionMonitoring), List(Of CollectionMonitoring))
            Return _ListCollectionMonitoring
        End Get
        Set(ByVal value As List(Of CollectionMonitoring))
            _ListCollectionMonitoring = value
        End Set
    End Property

    Private _ListPrudentials As List(Of Prudential)
    Public Property ListPrudentials() As List(Of Prudential)
        Get
            'Return CType(BFactory.CloneObject(_ListPrudentials), List(Of Prudential))
            Return _ListPrudentials
        End Get
        Set(ByVal value As List(Of Prudential))
            _ListPrudentials = value
        End Set
    End Property

    Private _ListPrudentialHistory As List(Of PrudentialHistory)
    Public Property ListPrudentialHistory() As List(Of PrudentialHistory)
        Get
            'Return CType(BFactory.CloneObject(_ListPrudentialHistory), List(Of PrudentialHistory))
            Return _ListPrudentialHistory
        End Get
        Set(ByVal value As List(Of PrudentialHistory))
            _ListPrudentialHistory = value
        End Set
    End Property

    Private _AllocationDate As Date
    Public Property AllocationDate() As Date
        Get
            Return _AllocationDate
        End Get
        Set(ByVal value As Date)
            _AllocationDate = value
        End Set
    End Property

#End Region

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Select Case Me.AllocationType
            Case EnumAllocationType.Automatic
                Me.TransferAuto()

            Case EnumAllocationType.Manual
                Me.TransferManual()
        End Select

        If Me.DialogResult = Windows.Forms.DialogResult.OK Then
            Me.Close()
        End If
    End Sub

    Private Sub frmTransferAdvancePayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BFactory = BusinessFactory.GetInstance()

        Dim items = (From x In Me.ListCollectionMonitoring _
                     Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                     Select x)

        Select Case Me.AllocationType
            Case EnumAllocationType.Automatic
                For Each item In items
                    With item
                        Dim selectedItem = item

                        Dim heldCollection = (From x In Me.ListCollectionMonitoring _
                                              Where x.ORNo = selectedItem.ORNo And x.TransType = EnumCollectionMonitoringType.TransferToHeldCollection _
                                              Select x.Amount).Sum()

                        Me.DGridView.Rows.Add(.CollectionNo, .ORNo, .IDNumber.IDNumber, .IDNumber.ParticipantID, _
                                              heldCollection, .Amount, .Amount, "0.00", "0.00", "0.00")
                    End With
                Next
                Me.DGridView.Columns("colManualHeld").Visible = False
                Me.DGridView.Columns("colAutoHeld").Visible = False

            Case EnumAllocationType.Manual

                For Each item In items
                    With item
                        Me.DGridView.Rows.Add(.CollectionNo, .ORNo, .IDNumber.IDNumber, .IDNumber.ParticipantID, _
                                              "0.00", .Amount, .Amount, "0.00", "0.00", "0.00")
                    End With
                Next

                Me.DGridView.Columns("colORNo").Visible = False
                'Me.DGridView.Columns("colPrudential").Visible = False
                Me.DGridView.Columns("colAutoHeld").Visible = False
        End Select

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DGridView_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGridView.CellEndEdit
        With Me.DGridView.Rows(e.RowIndex)
            If e.ColumnIndex = 4 Then
                If Not IsNumeric(.Cells("colAutoHeld").Value) Then
                    MsgBox("The held collection should be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
                    .Cells("colAutoHeld").Value = "0.00"
                End If
            End If

            If e.ColumnIndex = 6 Then
                If Not IsNumeric(.Cells("colExcessCollection").Value) Then
                    MsgBox("The excess collection should be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
                    .Cells("colExcessCollection").Value = "0.00"
                End If
            End If

            If e.ColumnIndex = 7 Then
                If Not IsNumeric(.Cells("colManualHeld").Value) Then
                    MsgBox("The held collection should be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
                    .Cells("colManualHeld").Value = "0.00"
                End If
            End If

            If e.ColumnIndex = 8 Then
                If Not IsNumeric(.Cells("colPrudential").Value) Then
                    MsgBox("The prudential should be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
                    .Cells("colPrudential").Value = "0.00"
                End If
            End If

            If e.ColumnIndex = 9 Then
                If Not IsNumeric(.Cells("colBPIAccount").Value) Then
                    MsgBox("The PEMC Account should be numeric!", MsgBoxStyle.Critical, "Specify the inputs")
                    .Cells("colBPIAccount").Value = "0.00"
                End If
            End If

            'To format the column into number, it must be converted to decimal first
            .Cells("colAutoHeld").Value = CDec(.Cells("colAutoHeld").Value)
            .Cells("colExcessCollection").Value = CDec(.Cells("colExcessCollection").Value)
            .Cells("colManualHeld").Value = CDec(.Cells("colManualHeld").Value)
            .Cells("colPrudential").Value = CDec(.Cells("colPrudential").Value)
            .Cells("colBPIAccount").Value = CDec(.Cells("colBPIAccount").Value)
        End With

    End Sub

    Private Sub TransferAuto()
        Dim passPrudentail As List(Of Prudential)
        Dim passPrudentialHistory As List(Of PrudentialHistory)

        passPrudentail = CType(BFactory.CloneObject(Me.ListPrudentials), List(Of Prudential))
        passPrudentialHistory = CType(BFactory.CloneObject(Me.ListPrudentialHistory), List(Of PrudentialHistory))

        For i As Integer = 0 To Me.DGridView.Rows.Count - 1
            With Me.DGridView.Rows(i)
                Dim colNo = CLng(.Cells("colColNo").Value)
                Dim ORNo = CLng(.Cells("colORNo").Value)
                Dim idnumber = CStr(.Cells("colIDNumber").Value)
                Dim participantId = CStr(.Cells("colParticipantID").Value)
                Dim amountUnallocated = CDec(.Cells("colUnallocated").Value)
                Dim amountCollection = CDec(.Cells("colExcessCollection").Value)
                Dim amountPrudential = CDec(.Cells("colPrudential").Value)
                Dim amountPEMC = CDec(.Cells("colBPIAccount").Value)

                If amountCollection < 0 Or amountPrudential < 0 Or amountPEMC < 0 Then
                    MsgBox("Invalid allocation for " & participantId & ". Values must not be negative!", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                If amountCollection + amountPrudential + amountPEMC <> amountUnallocated Then
                    MsgBox("Invalid allocation for " & participantId & _
                           ". Summation Amount for Excess Collection, Prudential and PEMC Account must be equal to Unallocated Amount. ", _
                           MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                'Transfer amount into Excess Collection
                Dim itemExcessCollection = (From x In Me.ListCollectionMonitoring _
                                            Where x.CollectionNo = colNo And x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                            Select x).First()
                itemExcessCollection.Amount = amountCollection

                'Transfer amount into PEMC Account
                If amountPEMC > 0 Then
                    Me.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", colNo, Me.AllocationDate, EnumAllocationType.Automatic, _
                                                    New AMParticipants(idnumber, participantId), ORNo, amountPEMC, _
                                                    EnumCollectionMonitoringType.TransferToPEMCAccount, EnumStatus.Active))
                End If

                'Transfer Prudential
                If amountPrudential > 0 Then
                    Dim cnt = (From x In passPrudentail _
                               Where x.IDNumber = idnumber _
                               Select x).Count()
                    If cnt > 0 Then
                        Dim itemPR = (From x In passPrudentail _
                                      Where x.IDNumber = idnumber _
                                      Select x).First()
                        itemPR.PrudentialAmount += amountPrudential
                    Else
                        passPrudentail.Add(New Prudential(idnumber, amountPrudential, 0))
                    End If

                    'Create the Prudential History
                    passPrudentialHistory.Add(New PrudentialHistory(ORNo, New AMParticipants(idnumber), amountPrudential, _
                                                                    EnumPrudentialTransType.Replenishment, Me.AllocationDate))

                    'Transfer Prudential into collection monitoring
                    Me.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", colNo, Me.AllocationDate, EnumAllocationType.Automatic, _
                                                    New AMParticipants(idnumber, participantId), ORNo, amountPrudential, _
                                                    EnumCollectionMonitoringType.TransferToPRReplenishment, EnumStatus.Active))
                End If
            End With
        Next

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.ListPrudentials = passPrudentail
        Me.ListPrudentialHistory = passPrudentialHistory
    End Sub

    Private Sub TransferManual()
        For i As Integer = 0 To Me.DGridView.Rows.Count - 1
            With Me.DGridView.Rows(i)
                Dim colNo = CLng(.Cells("colColNo").Value)
                Dim ORNo = CLng(.Cells("colORNo").Value)
                Dim idnumber = CStr(.Cells("colIDNumber").Value)
                Dim participantId = CStr(.Cells("colParticipantID").Value)
                Dim amountUnallocated = CDec(.Cells("colUnallocated").Value)
                Dim amountCollection = CDec(.Cells("colExcessCollection").Value)
                Dim amountPrudential = CDec(.Cells("colPrudential").Value)
                Dim amountHeld = CDec(.Cells("colManualHeld").Value)
                Dim amountPEMC = CDec(.Cells("colBPIAccount").Value)

                If amountCollection < 0 Or amountHeld < 0 Or amountPrudential < 0 Or amountPEMC < 0 Then
                    MsgBox("Invalid allocation for " & participantId & ". Values must not be negative!", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                If amountCollection + amountHeld + amountPEMC + amountPrudential <> amountUnallocated Then
                    MsgBox("Invalid allocation for " & participantId & _
                           ". Summation Amount for Excess Collection, Prudential, Held Collection and PEMC Account must be equal to Unallocated Amount. ", _
                           MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                'Transfer amount into Held Collection
                If amountHeld > 0 Then
                    Me.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", colNo, Me.AllocationDate, EnumAllocationType.Automatic, _
                                                    New AMParticipants(idnumber, participantId), ORNo, amountHeld, _
                                                    EnumCollectionMonitoringType.TransferToHeldCollection, EnumStatus.Active))
                End If

                'Transfer amount into Excess Collection
                Dim itemExcessCollection = (From x In Me.ListCollectionMonitoring _
                                            Where x.TransType = EnumCollectionMonitoringType.TransferToExcessCollection _
                                            Select x).First()
                itemExcessCollection.Amount = amountCollection

                'Transfer amount into Prudential
                If amountPrudential > 0 Then
                    Me.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", colNo, Me.AllocationDate, EnumAllocationType.Automatic, _
                                                    New AMParticipants(idnumber, participantId), ORNo, amountPrudential, _
                                                    EnumCollectionMonitoringType.TransferToPRReplenishment, EnumStatus.Active))
                End If

                'Transfer amount into PEMC Account
                If amountPEMC > 0 Then
                    Me.ListCollectionMonitoring.Add(New CollectionMonitoring(0, "", colNo, Me.AllocationDate, EnumAllocationType.Automatic, _
                                                    New AMParticipants(idnumber, participantId), ORNo, amountPEMC, _
                                                    EnumCollectionMonitoringType.TransferToPEMCAccount, EnumStatus.Active))
                End If
            End With
        Next

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

 End Class