'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmParticipantMapping
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 9, 2013
'Development Group:      Software Development and Support Division
'Description:            GUI For the Maintenance of Parent - Child Relationship for finance to be used in their Offsetting (Add/Edit Mapping)
'Arguments/Parameters:  
'Files/Database Tables:  
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description


Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

'Imports LDAPLib
'Imports LDAPLogin

Public Class frmParticipantMappingAddEdit
    Dim WBillHelper As WESMBillHelper
    Dim AllParticipants As New List(Of AMParticipants)
    Dim dicPIDtoName As New Dictionary(Of String, String)
    Dim dicNametoPID As New Dictionary(Of String, String)
    Dim dicIndexOnId As New Dictionary(Of String, Integer)
    Dim ParentParticipant As String

    Private _isAddParent As Boolean
    Public Property isAddParent() As Boolean
        Get
            Return _isAddParent
        End Get
        Set(ByVal value As Boolean)
            _isAddParent = value
        End Set
    End Property

    Private _BillPeriod As Integer
    Public Property BillPeriod() As Integer
        Get
            Return _BillPeriod
        End Get
        Set(ByVal value As Integer)
            _BillPeriod = value
        End Set
    End Property

    Private _EditParticipant As AMParticipants
    Public Property EditParticipant() As AMParticipants
        Get
            Return _EditParticipant
        End Get
        Set(ByVal value As AMParticipants)
            _EditParticipant = value
        End Set
    End Property

    Private _isEdit As Boolean
    Public Property isEdit() As Boolean
        Get
            Return _isEdit
        End Get
        Set(ByVal value As Boolean)
            _isEdit = value
        End Set
    End Property

    Private Sub frmParticipantMappingAddEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            WBillHelper = WESMBillHelper.GetInstance()
            WBillHelper.ConnectionString = AMModule.ConnectionString
            AllParticipants = WBillHelper.GetAMParticipants()
            Dim ctrIndex As Integer = 1
            For Each itmParticipant In AllParticipants
                dicNametoPID.Add(itmParticipant.IDNumber, itmParticipant.ParticipantID)
                dicPIDtoName.Add(itmParticipant.ParticipantID, itmParticipant.IDNumber)
                dicIndexOnId.Add(itmParticipant.ParticipantID, ctrIndex - 1)
                ctrIndex += 1
            Next

            If _isAddParent Then
                Dim BillPeriodToEdit As Integer = 0


                Me.grp_AddChild.Enabled = False
                Me.grp_AddParent.Enabled = True

                Me.grp_AddChild.Visible = False
                Me.grp_AddParent.Visible = True

                If isEdit = True Then
                    BillPeriodToEdit = Me._BillPeriod
                End If

                Me.cbo_BillPeriod.DataSource = Me.FillBillPeriodList()
                Me._BillPeriod = CInt(Me.cbo_BillPeriod.Text.ToString)

                Me.cbo_ParentID.DataSource = Me.FillCBOParticipant()

                If isEdit = True Then
                    Me.cbo_BillPeriod.Text = CStr(BillPeriodToEdit)
                    Me.cbo_ParentID.Text = Me._EditParticipant.ParticipantID

                    Me.cbo_BillPeriod.BackColor = Me.txt_BillPeriod.BackColor
                    Me.cbo_BillPeriod.Enabled = False
                    Me.cbo_ParentID.BackColor = Me.txt_EditParticipant.BackColor
                    Me.cbo_ParentID.Enabled = False

                    'get list of child
                    Dim lstChild As New List(Of ParticipantParentChildMapping)
                    lstChild = Me.WBillHelper.GetParentChildMapping(dicPIDtoName(Me._EditParticipant.ParticipantID), False)
                    lstChild = (From x In lstChild _
                                Where x.BillPeriod = BillPeriodToEdit _
                                And x.IDNumber <> dicPIDtoName(Me._EditParticipant.ParticipantID) _
                                And x.Status = 1 _
                                Select x).ToList

                    For Each itmChild In Me.FillChildParticipant(lstChild)
                        Me.lstbox_ChildList.Items.Add(itmChild)
                    Next

                End If
            Else
                Me.grp_AddParent.Enabled = False
                Me.grp_AddChild.Enabled = True

                Me.grp_AddParent.Visible = False
                Me.grp_AddChild.Visible = True

                Me.txt_BillPeriod.Text = CStr(Me._BillPeriod)
                Me.txt_EditParticipant.Text = Me._EditParticipant.ParticipantID.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

#Region "Functions for Addition of Parent"
    'Load Participant List in Combo
    Function FillCBOParticipant() As List(Of String)
        Dim retParticipants As New List(Of String)

        retParticipants = (From x In AllParticipants _
                           Select x.ParticipantID).ToList

        'Get All Parent Participants
        Dim GetP2CMapping As New List(Of ParticipantParentChildMapping)
        GetP2CMapping = Me.WBillHelper.GetParentChildMappingPerBillingPeriod(Me._BillPeriod)

        GetP2CMapping = (From x In GetP2CMapping _
                         Where x.ParentFlag = 1 _
                         Select x).ToList

        For Each itmMapping In GetP2CMapping
            retParticipants.Remove(dicNametoPID(itmMapping.PCNumber.ToString))
        Next

        Return retParticipants
    End Function

    'Load Billing Period List in Combo
    Function FillBillPeriodList() As List(Of String)
        Dim retLstBillPeriod As New List(Of String)

        Dim BillingPeriod = WBillHelper.GetCalendarBP()
        retLstBillPeriod = (From x In BillingPeriod _
                            Select CStr(x.BillingPeriod)).ToList

        Return retLstBillPeriod
    End Function

    'Load All Participants to List Box except selected participant and based on selected billing period
    Function lstUnmappedParticipants(ByVal ParticipantID As String, ByVal BillingPeriod As Integer) As List(Of String)
        Dim retParticipants As New List(Of String)

        If Me.cbo_BillPeriod.Items.Count <> 0 Then
            BillingPeriod = CInt(Me.cbo_BillPeriod.Text.ToString)
        Else
            BillingPeriod = 0
        End If

        retParticipants = WBillHelper.GetUnmappedParticipants(BillingPeriod)

        If Me._isAddParent = True Then
            If Me.cbo_ParentID.Items.Count <> 0 Then
                retParticipants = (From x In retParticipants _
                               Where x <> ParticipantID _
                               Select x).ToList
            Else
                retParticipants = (From x In retParticipants _
                                   Select x).ToList
            End If
        Else
            retParticipants = (From x In retParticipants _
                               Where x <> ParticipantID _
                               Select x).ToList
        End If

        Return retParticipants
    End Function

    'Load Child Participants
    Function FillChildParticipant(ByVal lstChild As List(Of ParticipantParentChildMapping)) As List(Of String)
        Dim retParticipant As New List(Of String)

        For Each itmChild In lstChild
            retParticipant.Add(dicNametoPID(itmChild.IDNumber))
        Next

        Return retParticipant
    End Function

    'Load All Parent Participants
    Function lstAllParentParticipants() As List(Of String)
        Dim retParticipant As New List(Of String)

        Dim lstParents As New List(Of ParticipantParentChildMapping)
        lstParents = Me.WBillHelper.GetParentChildMappingPerBillingPeriod(Me._BillPeriod, False)
        lstParents = (From x In lstParents _
                      Where x.ParentFlag = 1 _
                      Select x Order By x.PCNumber Ascending).ToList

        For Each itmParent In lstParents
            retParticipant.Add(dicNametoPID(itmParent.PCNumber))
        Next

        retParticipant = (From x In retParticipant _
                          Select x Order By x Ascending).ToList

        Return retParticipant
    End Function

#End Region

    Private Sub cmd_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Cancel.Click
        Me.Close()
    End Sub

#Region "Selection changed for the Add State"

    Private Sub cbo_BillPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_BillPeriod.SelectedIndexChanged
        Try
            If Len(Trim(Me.cbo_BillPeriod.Text.ToString)) <> 0 Then
                Me._BillPeriod = CInt(Me.cbo_BillPeriod.Text.ToString)
            End If

            Me.lstbox_ParticipantList.Items.Clear()
            For Each itmParticipant In Me.lstUnmappedParticipants(Me.cbo_ParentID.Text.ToString, BillPeriod)
                Me.lstbox_ParticipantList.Items.Add(itmParticipant)
            Next

            Me.cbo_ParentID.DataSource = Me.FillCBOParticipant()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub cbo_ParentID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_ParentID.SelectedIndexChanged
        Dim BillPeriod As Integer = 0
        Try
            If Len(Trim(Me.cbo_BillPeriod.SelectedText.ToString)) <> 0 Then
                BillPeriod = CInt(Me.cbo_BillPeriod.SelectedText.ToString)
            End If
            Me.lstbox_ParticipantList.Items.Clear()
            For Each itmParticipant In Me.lstUnmappedParticipants(Me.cbo_ParentID.Text.ToString, BillPeriod)
                If itmParticipant <> Me.cbo_ParentID.Text.ToString Then
                    Me.lstbox_ParticipantList.Items.Add(itmParticipant)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Add/Remove To-From Listboxes"
    Private Sub TransferListbox(ByVal FromListBox As ListBox, ByVal ToListBox As ListBox)
        Try
            If FromListBox.SelectedIndex = -1 Then
                Exit Sub
            End If

            If FromListBox.Items.Count < ToListBox.Items.Count Then
                ToListBox.Items.Insert(dicIndexOnId(FromListBox.SelectedItem.ToString) + 1, FromListBox.SelectedItem.ToString)
            Else
                ToListBox.Items.Add(FromListBox.SelectedItem.ToString)
            End If

            'Index Number of to be removed item
            Dim IndexNo As Integer = 0
            Dim IndexCount As Integer = 0
            IndexNo = FromListBox.SelectedIndex
            IndexCount = FromListBox.Items.Count
            FromListBox.Items.Remove(FromListBox.SelectedItem.ToString)

            If IndexNo = 0 Then
                If FromListBox.Items.Count <> 0 Then
                    FromListBox.SelectedIndex = IndexNo
                End If
            Else
                If FromListBox.Items.Count <= IndexNo Then
                    FromListBox.SelectedIndex = FromListBox.Items.Count - 1
                Else
                    FromListBox.SelectedIndex = IndexNo
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_AddSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_AddSingle.Click
        Me.TransferListbox(Me.lstbox_ParticipantList, Me.lstbox_ChildList)
    End Sub

    Private Sub cmd_RemoveSingle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_RemoveSingle.Click
        Me.TransferListbox(Me.lstbox_ChildList, Me.lstbox_ParticipantList)
    End Sub

    Private Sub cmd_AddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_AddAll.Click
        Try
            Me.lstbox_ParticipantList.Items.Clear()
            Dim billPeriod As Integer = 0
            If Len(Trim(Me.cbo_BillPeriod.SelectedText.ToString)) = 0 Then
                billPeriod = 0
            Else
                billPeriod = CInt(Me.cbo_BillPeriod.SelectedText.ToString)
            End If
            For Each itmParticipant In Me.lstUnmappedParticipants(Me.cbo_ParentID.Text.ToString, billPeriod)
                Me.lstbox_ChildList.Items.Add(itmParticipant)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_RemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_RemoveAll.Click
        Try
            Me.lstbox_ChildList.Items.Clear()
            Dim billPeriod As Integer = 0
            If Len(Trim(Me.cbo_BillPeriod.SelectedText.ToString)) = 0 Then
                billPeriod = 0
            Else
                billPeriod = CInt(Me.cbo_BillPeriod.SelectedText.ToString)
            End If
            For Each itmParticipant In Me.lstUnmappedParticipants(Me.cbo_ParentID.Text.ToString, billPeriod)
                Me.lstbox_ParticipantList.Items.Add(itmParticipant)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub
#End Region

    Private Sub rb_SetParent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_SetParent.CheckedChanged
        If rb_SetParent.Checked Then
            lstBox_ParentList.Items.Clear()
            lstBox_ParentList.Items.Add("Participant will be set as Parent")
            lstBox_ParentList.Enabled = False
        End If
    End Sub

    Private Sub rb_SetChild_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_SetChild.CheckedChanged
        Try
            If rb_SetChild.Checked Then
                Me.lstBox_ParentList.Items.Clear()
                Me.lstBox_ParentList.Enabled = True

                'Get Participant's Parent
                Dim GetParent As New List(Of ParticipantParentChildMapping)
                GetParent = WBillHelper.GetParentChildMappingPerBillingPeriod(Me._BillPeriod, False)

                Dim ParticipantParent As New ParticipantParentChildMapping
                ParticipantParent = (From x In GetParent _
                                     Where x.IDNumber = dicPIDtoName(Me.EditParticipant.ParticipantID.ToString) _
                                     Select x).FirstOrDefault
                Dim IndexCtr As Integer = 0
                Dim ctr As Integer = 0
                For Each itmParticipant In Me.lstAllParentParticipants()
                    If itmParticipant = dicNametoPID(ParticipantParent.PCNumber.ToString) Then
                        ParentParticipant = dicNametoPID(ParticipantParent.PCNumber.ToString)
                        IndexCtr = ctr
                    End If
                    Me.lstBox_ParentList.Items.Add(itmParticipant)
                    ctr += 1
                Next

                Me.lstBox_ParentList.SelectedIndex = IndexCtr
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub cmd_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Save.Click
        Dim ans As New MsgBoxResult
        Try
            'Check if the Selected Billing Period has posted transaction/s already (Offsetting Transaction)
            Dim ChkPosted As New List(Of WESMBillGPPosted)
            ChkPosted = WBillHelper.GetWESMBillGPPosted(CInt(Me._BillPeriod), 0)
            ChkPosted = (From x In ChkPosted _
                         Where x.PostType = "O" _
                         Select x).ToList

            If ChkPosted.Count > 0 Then
                MsgBox("Cannot save mapping details. The selected billing period has transactions that are already posted.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, MsgBoxStyle), "")
                Exit Sub
            End If

            ans = MsgBox("Do you really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "Save Mapping")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            If isAddParent = True Then
                'If Add Parent
                'Create List for saving of mapping
                Dim lstMapping As New List(Of ParticipantParentChildMapping)

                If Me.cbo_BillPeriod.Text.ToString.Trim.Length = 0 Or Me.cbo_ParentID.Text.ToString.Trim.Length = 0 Then
                    MsgBox("Please select a Participant or Billing period.", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                'Add List of child
                If Me.lstbox_ChildList.Items.Count <> 0 Then
                    For Each itmChild In Me.lstbox_ChildList.Items
                        Dim ChildMapping As New ParticipantParentChildMapping
                        With ChildMapping
                            With ChildMapping
                                .IDNumber = dicPIDtoName(itmChild.ToString())
                                .PCNumber = dicPIDtoName(Me.cbo_ParentID.Text.ToString)
                                .ParentFlag = 0
                                .Status = EnumStatus.Active
                                If Len(Trim(Me.txt_remarks.Text)) <> 0 Then
                                    .Remarks = Me.txt_remarks.Text
                                Else
                                    If isEdit = True Then
                                        .Remarks = "Updated mapping by, " & Me.WBillHelper.UserName & " on " & SystemDate & ". Set as child of " & .PCNumber & "."
                                    Else
                                        .Remarks = "Mapping is set by, " & Me.WBillHelper.UserName & " on " & SystemDate & ". Set as child of " & .PCNumber & "."
                                    End If

                                End If
                            End With
                            lstMapping.Add(ChildMapping)
                        End With
                    Next
                End If

                Dim itmMapping As New ParticipantParentChildMapping
                'Create selected Participant as parent
                With itmMapping
                    .IDNumber = dicPIDtoName(Me.cbo_ParentID.Text.ToString)
                    .PCNumber = .IDNumber
                    .ParentFlag = 1
                    .Status = EnumStatus.Active
                    If Len(Trim(Me.txt_remarks.Text)) <> 0 Then
                        .Remarks = Me.txt_remarks.Text
                    Else
                        .Remarks = "Set as parent by, " & Me.WBillHelper.UserName & " on " & SystemDate & "."
                    End If
                End With

                If isEdit = False Then
                    lstMapping.Add(itmMapping)
                End If

                Me.WBillHelper.SaveParentChildMapping(lstMapping, CInt(Me.cbo_BillPeriod.Text.ToString), isEdit, itmMapping)

                'Updated By Lance 08/17/2014
                '_Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_ParentChildWindow.ToString, edtParticipant.Remarks, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString, AMModule.UserName)

            Else
                Dim edtParticipant As New ParticipantParentChildMapping

                'If Edit Child Participant
                If rb_SetParent.Checked Then
                    With edtParticipant
                        .PCNumber = dicPIDtoName(Me.txt_EditParticipant.Text.ToString)
                        .IDNumber = .PCNumber
                        .ParentFlag = 1
                        .Status = EnumStatus.Active
                        .BillPeriod = Me._BillPeriod
                        If Len(Trim(Me.txt_remarks.Text)) <> 0 Then
                            .Remarks = Me.txt_remarks.Text
                        Else
                            .Remarks = "Updated mapping by, " & Me.WBillHelper.UserName & " on " & SystemDate & ". Set as parent."
                        End If
                    End With
                    Me.WBillHelper.SaveParticipantAsParent(edtParticipant)
                End If

                If rb_SetChild.Checked Then
                    If Me.lstBox_ParentList.SelectedItem.ToString = ParentParticipant Then
                        MsgBox("Cannot save changes, " & Me.lstBox_ParentList.SelectedItem.ToString & " is the original parent of " & Me._EditParticipant.ParticipantID & ".", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, MsgBoxStyle), "Cannot edit")
                        Exit Sub
                    End If

                    With edtParticipant
                        .PCNumber = dicPIDtoName(Me.txt_EditParticipant.Text.ToString)
                        .IDNumber = dicPIDtoName(Me.lstBox_ParentList.SelectedItem.ToString)
                        .ParentFlag = 0
                        .Status = EnumStatus.Active
                        If Len(Trim(Me.txt_remarks.Text)) <> 0 Then
                            .Remarks = Me.txt_remarks.Text
                        Else
                            .Remarks = "Updated mapping by, " & Me.WBillHelper.UserName & " on " & SystemDate & ". Set as Child of " & dicPIDtoName(Me.lstBox_ParentList.SelectedItem.ToString) & " from " & ParentParticipant & "."
                        End If
                    End With
                    Me.WBillHelper.SaveParticipantAsChild(edtParticipant, Me._BillPeriod)
                End If

                'Updated By Lance 08/17/2014
                _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModules.AMS_ParentChildWindow.ToString, edtParticipant.Remarks, "", "", CType(EnumColorCode.Blue, ColorCode), EnumLogType.SuccessfullyEdited.ToString, AMModule.UserName)
            End If

            

            MsgBox("The Participant Mapping is successfully saved.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Saved Successfully")
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try

    End Sub
End Class