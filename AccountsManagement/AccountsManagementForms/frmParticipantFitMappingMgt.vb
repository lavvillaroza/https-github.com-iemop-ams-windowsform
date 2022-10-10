Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmParticipantFitMappingMgt

    Private WBillHelper As WESMBillHelper

#Region "DicBillingPeriods"
    Private _DicBillingPeriods As Dictionary(Of Integer, CalendarBillingPeriod)
    Public Property DicBillingPeriods() As Dictionary(Of Integer, CalendarBillingPeriod)
        Get
            Return _DicBillingPeriods
        End Get
        Set(ByVal value As Dictionary(Of Integer, CalendarBillingPeriod))
            _DicBillingPeriods = value
        End Set
    End Property

#End Region

#Region "DicParticpants"
    Private _DicParticpants As Dictionary(Of String, AMParticipants)
    Public Property DicParticpants() As Dictionary(Of String, AMParticipants)
        Get
            Return _DicParticpants
        End Get
        Set(ByVal value As Dictionary(Of String, AMParticipants))
            _DicParticpants = value
        End Set
    End Property

#End Region

#Region "ListBillingPeriod"
    Private _ListBillingPeriod As List(Of CalendarBillingPeriod)
    Public Property ListBillingPeriod() As List(Of CalendarBillingPeriod)
        Get
            Return _ListBillingPeriod
        End Get
        Set(ByVal value As List(Of CalendarBillingPeriod))
            _ListBillingPeriod = value
        End Set
    End Property

#End Region

#Region "ListOfParentFIT"
    Private _ListOfParentFIT As List(Of AMParticipants)
    Public Property ListOfParentFIT() As List(Of AMParticipants)
        Get
            Return _ListOfParentFIT
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListOfParentFIT = value
        End Set
    End Property

#End Region

#Region "ListOfFitSelected"
    Private _ListOfFitSelected As List(Of AMParticipantsFit)
    Public Property ListOfFitSelected() As List(Of AMParticipantsFit)
        Get
            Return _ListOfFitSelected
        End Get
        Set(ByVal value As List(Of AMParticipantsFit))
            _ListOfFitSelected = value
        End Set
    End Property
#End Region

#Region "ListOfFitUnselected"
    Private _ListOfFitUnselected As List(Of AMParticipants)
    Public Property ListOfFitUnselected() As List(Of AMParticipants)
        Get
            Return _ListOfFitUnselected
        End Get
        Set(ByVal value As List(Of AMParticipants))
            _ListOfFitUnselected = value
        End Set
    End Property

#End Region

#Region "ParentFit"
    Private _ParentFit As AMParticipants
    Public Property ParentFit() As AMParticipants
        Get
            Return _ParentFit
        End Get
        Set(ByVal value As AMParticipants)
            _ParentFit = value
        End Set
    End Property
#End Region

#Region "ItemParentAndChildFITMapping"
    Private _ItemParentAndChildFITMapping As AMParticipantsFIT
    Public Property ItemParentAndChildFITMapping() As AMParticipantsFIT
        Get
            Return _ItemParentAndChildFITMapping
        End Get
        Set(ByVal value As AMParticipantsFIT)
            _ItemParentAndChildFITMapping = value
        End Set
    End Property

#End Region

#Region "LogType"
    Private _LogType As EnumLogType
    Public Property LogType() As EnumLogType
        Get
            Return _LogType
        End Get
        Set(ByVal value As EnumLogType)
            _LogType = value
        End Set
    End Property

#End Region

#Region "Events"
    Private Sub frmParticipantFitMappingMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Select Case Me.LogType
            Case EnumLogType.Saving
                For Each item In Me.DicBillingPeriods
                    Me.CB_BPeriod.Items.Add(item.Key)
                    Me.CB_BPeriod.SelectedIndex = -1
                Next

                'Add the Parent FIT
                For Each item In Me.ListOfParentFIT
                    Me.CB_ParticipantID.Items.Add(item.ParticipantID)
                Next
                Me.CB_ParticipantID.SelectedIndex = -1

                Me.EnableControls(True, True, True, True)

            Case EnumLogType.Editing
                'Load the Billing Period and Parent FIT
                With Me.ItemParentAndChildFITMapping
                    Me.CB_BPeriod.Items.Add(.BillingPeriod)
                    Me.txtBillingPeriod.Text = Me.DicBillingPeriods(.BillingPeriod).StartDate.ToString("MM/dd/yyyy") & _
                                        "-" & Me.DicBillingPeriods(.BillingPeriod).EndDate.ToString("MM/dd/yyyy")
                    Me.CB_BPeriod.SelectedIndex = 0

                    Me.CB_ParticipantID.Items.Add(.ParentIDNumber.ParticipantID)
                    Me.txtParentFIT.Text = Me.DicParticpants(.ParentIDNumber.ParticipantID).FullName
                    Me.CB_ParticipantID.SelectedIndex = 0
                End With

                Me.EnableControls(False, False, True, True)

            Case EnumLogType.Viewing

                'Load the Billing Period and Parent FIT
                With Me.ItemParentAndChildFITMapping
                    Me.CB_BPeriod.Items.Add(.BillingPeriod)
                    Me.txtBillingPeriod.Text = Me.DicBillingPeriods(.BillingPeriod).StartDate.ToString("MM/dd/yyyy") & _
                                        "-" & Me.DicBillingPeriods(.BillingPeriod).EndDate.ToString("MM/dd/yyyy")
                    Me.CB_BPeriod.SelectedIndex = 0

                    Me.CB_ParticipantID.Items.Add(.ParentIDNumber.ParticipantID)
                    Me.txtParentFIT.Text = Me.DicParticpants(.ParentIDNumber.ParticipantID).FullName
                    Me.CB_ParticipantID.SelectedIndex = 0
                End With

                Me.EnableControls(False, False, False, False)
        End Select

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        With Me.lbUnselectedAdd
            For i = 0 To .Items.Count - 1
                Me.lbSelectedAdd.Items.Add(.Items(i).ToString())
            Next
            .Items.Clear()
        End With
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnUnselectAll_Click(sender As Object, e As EventArgs) Handles btnUnselectAll.Click
        With Me.lbSelectedAdd
            For i = 0 To .Items.Count - 1
                Me.lbUnselectedAdd.Items.Add(.Items(i).ToString())
            Next
            .Items.Clear()
        End With
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        If Me.lbUnselectedAdd.SelectedIndices.Count = 0 Then
            Exit Sub
        End If

        With Me.lbUnselectedAdd
            For Each i As Integer In .SelectedIndices
                Me.lbSelectedAdd.Items.Add(.Items(i))
            Next

            Me.lbSelectedAdd.Sorted = True
            Dim knt As Integer = .SelectedIndices.Count
            Dim j As Integer
            For j = 0 To knt - 1
                .Items.RemoveAt(.SelectedIndex)
            Next

        End With
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnUnselect_Click(sender As Object, e As EventArgs) Handles btnUnselect.Click
        With Me.lbSelectedAdd
            For Each i As Integer In .SelectedIndices
                Me.lbUnselectedAdd.Items.Add(.Items(i))
                .Items.RemoveAt(i)
            Next
            Me.lbUnselectedAdd.Sorted = True
            Dim knt As Integer = .SelectedIndices.Count
            Dim j As Integer
            For j = 0 To knt - 1
                .Items.RemoveAt(.SelectedIndex)
            Next
            .Sorted = True
        End With
        Me.btnSave.Enabled = True
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ans As MsgBoxResult

        'Validate
        If Me.CB_BPeriod.SelectedIndex = -1 Then
            MsgBox("Please specify the Billing Period", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.CB_BPeriod.Focus()
            Exit Sub
        ElseIf Me.CB_ParticipantID.SelectedIndex = -1 Then
            MsgBox("Please specify the Parent FIT", MsgBoxStyle.Exclamation, "Specify the inputs")
            Me.CB_ParticipantID.Focus()
            Exit Sub
        ElseIf Me.lbSelectedAdd.Items.Count = 0 Then
            MsgBox("Please specify the Child FIT", MsgBoxStyle.Exclamation, "Specify the inputs")
            Exit Sub
        End If

        ans = MsgBox("Do you you really want to save?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Save")

        If ans = MsgBoxResult.No Then
            Exit Sub
        End If

        If Me.LogType = EnumLogType.Saving Or Me.LogType = EnumLogType.Editing Then

            'There is a case that the user edit the record by selecting in billing period and parent fit
            'dropdown even the transaction is adding new. The system will load in Me.ItemParentAndChildFITMapping the fetched data
            If Me.ItemParentAndChildFITMapping.FitID = 0 Then
                Me.ItemParentAndChildFITMapping = New AMParticipantsFIT
                With Me.ItemParentAndChildFITMapping
                    .BillingPeriod = Me.DicBillingPeriods(CInt(Me.CB_BPeriod.Text)).BillingPeriod
                    .ParentIDNumber = Me.DicParticpants(Me.CB_ParticipantID.Text)
                End With
            End If
           
            'Update the Child of the Parent FIT
            Me.GetSelectedChild()
        Else
            'Update the Child of the Parent FIT
            Me.GetSelectedChild()
        End If

        'Save the Parent/Child FIT
        WBillHelper.SaveFITMapping(Me.ItemParentAndChildFITMapping)

        MsgBox("Successfully Saved", MsgBoxStyle.Information, "Saved")

        Me.EnableControls(False, False, True, True)

        'Load in the Main Form
        If Me.LogType = EnumLogType.Saving Then
            Dim chck As Boolean = False

            'Find the record if does exist then update
            For index As Integer = 0 To frmParticipantFitMapping.DGV_ParticipantsFit.Rows.Count - 1
                If CLng(frmParticipantFitMapping.DGV_ParticipantsFit.Rows(index).Cells("colFITID").Value) = Me.ItemParentAndChildFITMapping.FitID Then
                    chck = True
                    Exit For
                End If
            Next

            If Not chck Then
                With ItemParentAndChildFITMapping
                    Dim itemBP = Me.DicBillingPeriods(.BillingPeriod)
                    Dim itemParticipant = Me.DicParticpants(.ParentIDNumber.ParticipantID)

                    frmParticipantFitMapping.DGV_ParticipantsFit.Rows.Add(.FitID.ToString(), itemBP.BillingPeriod, _
                                                                             itemBP.BillingPeriod.ToString() & " (" & itemBP.StartDate.ToString("MM/dd/yyyy") & _
                                                                             "-" & itemBP.EndDate.ToString("MM/dd/yyyy") & ")", _
                                                                            itemParticipant.IDNumber, itemParticipant.ParticipantID, itemParticipant.FullName)
                End With
            End If
        End If

        Me.Close()
    End Sub

    Private Sub CB_BPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_BPeriod.SelectedIndexChanged
        Me.lbUnselectedAdd.Items.Clear()
        Me.lbSelectedAdd.Items.Clear()

        If CB_BPeriod.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim bp As CalendarBillingPeriod
        Dim ParentID As String

        'Get the selected billing period
        bp = Me.DicBillingPeriods(CInt(Me.CB_BPeriod.Text))

        Me.txtBillingPeriod.Text = bp.StartDate.ToString("MM/dd/yyyy") & _
                                  " - " & bp.EndDate.ToString("MM/dd/yyyy")

        'Load the Child of the Parent FIT
        Me.LoadUnMappedChildFIT(bp)

        If CB_ParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        'Get the Parent ID
        ParentID = Me.CB_ParticipantID.Text

        Me.LoadMappedChildFIT(bp, ParentID)
    End Sub

    Private Sub CB_ParticipantID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_ParticipantID.SelectedIndexChanged
        Me.lbSelectedAdd.Items.Clear()
        If Me.CB_BPeriod.SelectedIndex = -1 Or Me.CB_ParticipantID.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim bp As CalendarBillingPeriod
        Dim ParentID As String

        'Get the selected billing period
        bp = Me.DicBillingPeriods(CInt(Me.CB_BPeriod.Text))

        'Get the Parent ID
        ParentID = Me.CB_ParticipantID.Text

        Me.txtParentFIT.Text = Me.DicParticpants(ParentID).FullName

        Me.LoadMappedChildFIT(bp, ParentID)
    End Sub

#End Region

#Region "LoadMappedChildFIT"
    Public Sub LoadMappedChildFIT(bp As CalendarBillingPeriod, ParentID As String)
        Dim listMappedChildFIT As List(Of AMParticipantsFIT)

        'Initialize the AMParticipantsFIT
        Me.ItemParentAndChildFITMapping = New AMParticipantsFIT()

        'Get the Child FIT assigned to the Parent FIT
        listMappedChildFIT = WBillHelper.GetMappedChildFIT(bp.BillingPeriod, New AMParticipants("", ParentID))

        If listMappedChildFIT.Count = 0 Then
            Exit Sub
        End If

        'Set the Parent and Child Mapping
        Me.ItemParentAndChildFITMapping = listMappedChildFIT.First()

        'Load in the seledted list the Child FIT
        For Each item In ItemParentAndChildFITMapping.ListofChildFIT
            Me.lbSelectedAdd.Items.Add(item.ParticipantID)
        Next

    End Sub

#End Region

#Region "LoadUnMappedChildFIT"
    Private Sub LoadUnMappedChildFIT(bp As CalendarBillingPeriod)
        Dim listUnmappedChildFIT As List(Of AMParticipants)

        'Get the unmapped participants for the selected billing period
        listUnmappedChildFIT = WBillHelper.GetUnmappedChildFIT(bp.BillingPeriod)

        Me.lbUnselectedAdd.Items.Clear()
        For Each item In listUnmappedChildFIT
            Me.lbUnselectedAdd.Items.Add(item.ParticipantID)
        Next
    End Sub
       
#End Region

#Region "Get Selected Child"
    Private Sub GetSelectedChild()
        Dim listChildFIT As New List(Of AMParticipants)

        For Each item In Me.lbSelectedAdd.Items
            listChildFIT.Add(Me.DicParticpants(item.ToString()))
        Next

        'Get the selected Child FIT
        ItemParentAndChildFITMapping.ListofChildFIT = listChildFIT
    End Sub
#End Region

#Region "EnableControls"
    Private Sub EnableControls(cboBP As Boolean, cboParentFIT As Boolean, btnSelect As Boolean, btnSave As Boolean)
        Me.CB_BPeriod.Enabled = cboBP
        Me.CB_ParticipantID.Enabled = cboParentFIT
        Me.btnSelect.Enabled = btnSelect
        Me.btnSelectAll.Enabled = btnSelect
        Me.btnUnselect.Enabled = btnSelect
        Me.btnUnselectAll.Enabled = btnSelect
        Me.btnSave.Enabled = btnSave
    End Sub
#End Region


End Class