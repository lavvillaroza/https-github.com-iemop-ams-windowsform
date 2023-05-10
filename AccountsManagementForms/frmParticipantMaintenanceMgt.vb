'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmParticipantMaintenance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 28, 2011
'Development Group:      Software Development and Support Division
'Description:            GUI For the Maintenance of Parent - Child Relationship for finance to be used in their Offsetting
'Arguments/Parameters:  
'Files/Database Tables:  BILL_PARTICIPANTS, AM_PARENT_CHILD_MAPPING
'Return Value:
'Error codes/Exceptions:
'
'****************************************************************************
' Revision History
'	Date Modified		    Programmer		                      Description
'   November 28, 2011   Juan Carlo L. Panopio                Form initialization and design
'   November 28, 2011   Juan Carlo L. Panopio                Finished functionalities for Setting Participant as Child 
'                                                            and for Transfer of Parent.


Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmParticipantMaintenanceMgt

    Private _State As String
    Public Property State() As String
        Get
            Return _State
        End Get
        Set(ByVal value As String)
            Me._State = value
        End Set
    End Property

    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean
    Dim PrevParent As String
    Private BP As Integer

    Private Sub frmParticipantMaintenanceMgt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        'Fill ComboBox
        Dim Participants = WBillHelper.GetAMParticipantsAll()
        Dim ParentChildMapping = WBillHelper.GetParentChildMapping(1)

        Dim ValidParents = (From x In Participants Join y In ParentChildMapping On x.IDNumber Equals y.IDNumber Where y.ParentFlag = 1 _
                            And y.BillPeriod = CInt(frmParticipantMaintenance.cbo_BillPeriod.SelectedItem.ToString) And x.ParticipantID <> TXT_Child.Text Select x.ParticipantID Distinct)
        If ValidParents.Count = 0 Then
            MsgBox("No valid parents found, please set a parent first", MsgBoxStyle.Critical)
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Failed, "Set Participant: " & Me.TXT_Child.Text & " as child failed, No valid parents found.")
            Me.Close()
            Exit Sub
        End If

        CBO_Parent.DataSource = ValidParents.ToList

        'Fill TextBoxes
        Dim ParticipantIDNumber = CStr((From x In Participants Where x.ParticipantID = TXT_Child.Text Select x).FirstOrDefault.IDNumber.ToString)

        PrevParent = CBO_Parent.SelectedItem.ToString
        CBO_Parent.SelectedItem = -1
        ParentChildMapping = (From x In ParentChildMapping Where x.IDNumber = ParticipantIDNumber Select x).ToList

        'Check if there are already records
        If State = "SetAsChild" Then
            If ParentChildMapping.Count <> 0 Then
                For Each item In ParentChildMapping
                    With item
                        TXT_UpdatedBy.Text = .UpdatedBy
                        TXT_UpdatedDate.Text = CStr(.UpdatedDate)
                    End With
                Next
            Else
                TXT_UpdatedBy.Text = "No Records yet."
                TXT_UpdatedDate.Text = "No Records yet."
            End If

            TXT_UpdatedBy.ReadOnly = True
            TXT_UpdatedDate.ReadOnly = True
        ElseIf State = "TransferParent" Then
            If ParentChildMapping.Count <> 0 Then
                For Each item In ParentChildMapping
                    With item
                        TXT_UpdatedBy.Text = .UpdatedBy
                        TXT_UpdatedDate.Text = CStr(.UpdatedDate)
                    End With
                Next
            Else
                TXT_UpdatedBy.Text = "No Records yet."
                TXT_UpdatedDate.Text = "No Records yet."
            End If

            TXT_UpdatedBy.ReadOnly = True
            TXT_UpdatedDate.ReadOnly = True
        End If


    End Sub

    Public Sub SetAsChild(ByVal ChildId As String, ByVal BillPeriod As Integer)
        State = "SetAsChild"
        TXT_Child.Text = ChildId
        TXT_Child.ReadOnly = True
        BP = BillPeriod
        Me.Show()
    End Sub

    Public Sub TransferParent(ByVal ChildId As String, ByVal BillPeriod As Integer)
        State = "TransferParent"
        Label3.Text = "Select new parent"
        TXT_Child.Text = ChildId
        TXT_Child.ReadOnly = True
        BP = BillPeriod
        Me.Show()
    End Sub

    Private Sub CMD_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Cancel.Click
        Me.Close()
    End Sub

    Private Sub CMD_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Save.Click
        Dim SetParticipantAsChild As New ParticipantParentChildMapping
        Dim ParticipantList = WBillHelper.GetAMParticipantsAll
        Dim ans As MsgBoxResult = MsgBoxResult.No
        Dim NewParentId As String = CStr((From x In ParticipantList Where x.ParticipantID = CBO_Parent.SelectedItem.ToString Select x.IDNumber).FirstOrDefault.ToString)
        Dim ParticipantIds As String = CStr((From x In ParticipantList Where x.ParticipantID = TXT_Child.Text Select x.IDNumber).FirstOrDefault.ToString)
        Dim strLogs As String = ""
        With SetParticipantAsChild
            .IDNumber = ParticipantIds
            .PCNumber = NewParentId
            If State = "SetAsChild" Then
                .Remarks = TXT_Remarks.Text & " - Participant " & TXT_Child.Text & " is now a child of " & CBO_Parent.SelectedItem.ToString & " as of " & SystemDate & "."
            ElseIf State = "TransferParent" Then
                .Remarks = TXT_Remarks.Text & " - Participant " & TXT_Child.Text & " is now a child of " & CBO_Parent.SelectedItem.ToString & " from " & PrevParent & " as of " & SystemDate & "."
            End If
        End With

        ans = MsgBox("Do you really want to save the changes?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Save Changes")

        If ans = MsgBoxResult.Yes Then
            WBillHelper.SaveParticipantAsChild(SetParticipantAsChild, BP)
            MsgBox("Participant " & TXT_Child.Text & " is now a child of " & CBO_Parent.SelectedItem.ToString & ".")
            If State = "SetAsChild" Then
                'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, "Participant: " & TXT_Child.Text & " is now child of " & CBO_Parent.SelectedItem.ToString & ".")
            Else
                'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, "Participant: " & TXT_Child.Text & " is now child of " & CBO_Parent.SelectedItem.ToString & " from Participant: " & PrevParent & ".")
            End If
            Dim PCMapping = WBillHelper.GetParentChildMapping(1)
            Dim i = (From x In ParticipantList Join y In PCMapping On x.IDNumber Equals y.IDNumber Where x.IDNumber = ParticipantIds And y.BillPeriod = BP Select x.IDNumber, x.ParticipantID, x.FullName _
                     , y.ParentFlag).ToList
            With frmParticipantMaintenance.DGV_ParentParticipant.Rows(frmParticipantMaintenance.DGV_ParentParticipant.CurrentRow.Index)
                For Each rec In i
                    .Cells(0).Value = rec.IDNumber
                    .Cells(1).Value = rec.ParticipantID
                    .Cells(2).Value = rec.FullName
                    .Cells(3).Value = IIf(rec.ParentFlag = 1, "Parent", "Child")
                Next
            End With
            frmParticipantMaintenance.DGV_ParentParticipant.Rows(frmParticipantMaintenance.DGV_ParentParticipant.CurrentRow.Index).DefaultCellStyle.BackColor = Color.White
            frmParticipantMaintenance.DGV_ParentParticipant.Rows(frmParticipantMaintenance.DGV_ParentParticipant.CurrentRow.Index).Selected = True

            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            Dim SelectedRow As Integer
            If frmParticipantMaintenance.DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(frmParticipantMaintenance.DGV_ParentParticipant.CurrentRow.Index)
            End If
            Dim Participant As Integer = CInt(frmParticipantMaintenance.DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)

            Dim ParentFlagging As String = (frmParticipantMaintenance.DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)


            Dim CheckUnregistered = (From x In ParticipantList Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
                                     Where y.Status = 1 And x.Status = 1 And y.BillPeriod = BP _
                                     Select x Distinct).Count

            Dim CheckCount = ParticipantList.Count - CheckUnregistered
            frmParticipantMaintenance.TS_LBL_NewParticipants.Text = "There are still " & CheckCount & " new participants"

            'Command Button Control if Parent/Child/Not Assigned Yet
            If UCase(ParentFlagging) = "PARENT" Then
                frmParticipantMaintenance.CMD_SetParent.Enabled = False
                frmParticipantMaintenance.CMD_TransferParent.Enabled = False
                frmParticipantMaintenance.CMD_SetChild.Enabled = True
            ElseIf UCase(ParentFlagging) = "CHILD" Then
                frmParticipantMaintenance.CMD_SetChild.Enabled = False
                frmParticipantMaintenance.CMD_SetParent.Enabled = True
                frmParticipantMaintenance.CMD_TransferParent.Enabled = True
            ElseIf Len(Trim(ParentFlagging)) = 0 Then
                frmParticipantMaintenance.CMD_TransferParent.Enabled = False
                frmParticipantMaintenance.CMD_SetChild.Enabled = True
                frmParticipantMaintenance.CMD_SetParent.Enabled = True
            End If

            Me.Close()
        Else
            TXT_Remarks.Text = ""
        End If



    End Sub

    Private Sub TXT_Remarks_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TXT_Remarks.KeyDown, TXT_Child.KeyDown, _
                                                                                                                  TXT_UpdatedBy.KeyDown, TXT_UpdatedDate.KeyDown
        Select Case e.KeyValue
            Case 222
                e.SuppressKeyPress = True
            Case Else
        End Select
    End Sub

End Class