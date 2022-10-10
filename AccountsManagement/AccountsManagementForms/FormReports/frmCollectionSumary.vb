'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmCollectionSearch
'Orginal Author:         Vladimir E. Espiritu
'File Creation Date:     February 13, 2012
'Development Group:      Software Development and Support Division
'Description:            GUI for the Collection Summary Report
'Arguments/Parameters:  
'Files/Database Tables:  AM_COLLECTION
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                     Description
'   February 12, 2012       Vladimir E. Espiritu                 GUI design and basic functionalities                    
'

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects


Public Class frmCollectionSumary
    Private WBillHelper As WESMBillHelper
    Private _Participants As List(Of AMParticipants)

    Private Sub frmCollectionSumary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'Get the participants
        Me._Participants = WBillHelper.GetAMParticipants()

        With Me.ddlParticipantID
            .Enabled = False
            .DisplayMember = "ParticipantID"
            .ValueMember = "IDNumber"
            .DataSource = Me._Participants
            .SelectedIndex = -1
        End With

        Me.rbCollectionDate.Checked = True
        Me.chckAuto.CheckState = CheckState.Checked
        Me.chckManual.CheckState = CheckState.Checked
        Me.chckAllocated.CheckState = CheckState.Checked
        Me.chckUnallocated.CheckState = CheckState.Checked
        Me.chckPosted.CheckState = CheckState.Checked
        Me.chckNotPost.CheckState = CheckState.Checked
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Dim collectionItems As New List(Of Collection)
        Dim result As New List(Of Collection)

        Try
            If Not Me.ValidateSearch Then
                Exit Sub
            End If

            Dim idnumber As String = "", IsCollectionDate As Boolean
            Dim typeAllocation As EnumAllocationType?, typeStatus As EnumCollectionStatus?
            Dim IsPosted As EnumIsPosted?

            Dim dateFrom = CDate(FormatDateTime(Me.dtFrom.Value, DateFormat.ShortDate))
            Dim dateTo = CDate(FormatDateTime(Me.dtTo.Value, DateFormat.ShortDate))

            If Me.ddlParticipantID.SelectedIndex <> -1 Then
                idnumber = CStr(Me.ddlParticipantID.SelectedValue)
            End If

            If Me.rbCollectionDate.Checked Then
                IsCollectionDate = True
            Else
                IsCollectionDate = False
            End If

            'Get the collections
            If idnumber = "" Then
                result = WBillHelper.GetCollections(dateFrom, dateTo, IsCollectionDate)
            Else
                result = WBillHelper.GetCollections(dateFrom, dateTo, idnumber, IsCollectionDate)
            End If

            If Me.chckAuto.CheckState = CheckState.Checked And Me.chckManual.CheckState = CheckState.Unchecked Then
                typeAllocation = EnumAllocationType.Automatic
            ElseIf Me.chckAuto.CheckState = CheckState.Unchecked And Me.chckManual.CheckState = CheckState.Checked Then
                typeAllocation = EnumAllocationType.Manual
            Else
                typeAllocation = Nothing
            End If

            If Me.chckAllocated.CheckState = CheckState.Checked And Me.chckUnallocated.CheckState = CheckState.Unchecked Then
                typeStatus = EnumCollectionStatus.Allocated
            ElseIf Me.chckAllocated.CheckState = CheckState.Unchecked And Me.chckUnallocated.CheckState = CheckState.Checked Then
                typeStatus = EnumCollectionStatus.NotAllocated
            Else
                typeStatus = Nothing
            End If

            If Me.chckPosted.CheckState = CheckState.Checked And Me.chckNotPost.CheckState = CheckState.Unchecked Then
                IsPosted = EnumIsPosted.Posted
            ElseIf Me.chckPosted.CheckState = CheckState.Unchecked And Me.chckNotPost.CheckState = CheckState.Checked Then
                IsPosted = EnumIsPosted.NotPost
            Else
                IsPosted = Nothing
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
                MsgBox("No records found!", MsgBoxStyle.Information, "No data")
                Exit Sub
            End If

            Dim items = From x In collectionItems Join y In Me._Participants _
                        On x.IDNumber Equals y.IDNumber _
                        Select x, y.ParticipantID

            Dim dt = New DSReport.CollectionDataTable
            For Each item In items
                With item
                    Dim row = dt.NewRow()
                    row("OR_NO") = .x.ORNo
                    row("COLLECTION_DATE") = .x.CollectionDate
                    row("ID_NUMBER") = .x.IDNumber
                    row("AMOUNT") = .x.CollectedAmount * -1D
                    row("PARTICIPANT_ID") = .ParticipantID
                    row("TYPE") = .x.CollectionCategory
                    dt.Rows.Add(row)
                End With
            Next
            dt.AcceptChanges()

            Dim frm As New frmReportViewer
            With frm
                .LoadCollectionSummary(dt, dateFrom, dateTo)
                .ShowDialog()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chckParticipantID_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.chckParticipantID.CheckState = CheckState.Checked Then
            Me.ddlParticipantID.Enabled = True
            Me.ddlParticipantID.SelectedIndex = -1
        Else
            Me.ddlParticipantID.Enabled = False
            Me.ddlParticipantID.SelectedIndex = -1
        End If
    End Sub

#Region "Methods/Functions"
    Private Function ValidateSearch() As Boolean
        ValidateSearch = False

        If Me.chckParticipantID.CheckState = CheckState.Checked And Me.ddlParticipantID.SelectedIndex = -1 Then
            MsgBox("Please select first the participant id!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf CDate(Me.dtFrom.Value.ToString("MM/dd/yyyy")) > CDate(Me.dtTo.Value.ToString("MM/dd/yyyy")) Then
            MsgBox("Invalid date range!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckAuto.CheckState = CheckState.Unchecked And Me.chckManual.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the type of allocation!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckAllocated.CheckState = CheckState.Unchecked And Me.chckUnallocated.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the status!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        ElseIf Me.chckPosted.CheckState = CheckState.Unchecked And Me.chckNotPost.CheckState = CheckState.Unchecked Then
            MsgBox("Please specify first the isposted!", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Function

        Else
            ValidateSearch = True
        End If

    End Function
#End Region

End Class