'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmParticipantMapping
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     September 9, 2013
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



Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmParticipantMapping
    Dim WBillHelper As WESMBillHelper
    Dim AllParticipants As New List(Of AMParticipants)
    Dim dicIDToName As New Dictionary(Of String, String)
    Dim dicNameToID As New Dictionary(Of String, String)
    Dim OffsetDetails As New List(Of ParticipantParentChildMapping)

    Private Enum EnumParentChild
        Parent
        Child
    End Enum

    Private Sub frmParticipantMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        AllParticipants = WBillHelper.GetAMParticipants()

        For Each itmParticipant In AllParticipants
            dicIDToName.Add(itmParticipant.IDNumber, itmParticipant.ParticipantID)
            dicNameToID.Add(itmParticipant.ParticipantID, itmParticipant.IDNumber)
        Next

        Me.cbo_billPeriod.DataSource = Me.FillBillPeriod()
        Me.cbo_ParticipantID.DataSource = Me.FillParticipantID()

        Me.cbo_billPeriod.Enabled = True
        Me.cbo_ParticipantID.Enabled = True
        Me.chk_BillPeriod.Checked = True
        Me.chk_participantId.Checked = True

    End Sub

    Function FillBillPeriod() As List(Of String)
        Dim retBillPeriod As New List(Of String)

        Dim ListBillPeriodDueDate = WBillHelper.GetCalendarBP()

        retBillPeriod = (From x In ListBillPeriodDueDate _
                         Select CStr(x.BillingPeriod)).ToList

        Return retBillPeriod
    End Function

    Function FillParticipantID() As List(Of String)
        Dim retParticipantId As New List(Of String)


        retParticipantId = (From x In AllParticipants _
                            Select x.ParticipantID).ToList

        Return retParticipantId
    End Function

    Private Sub chk_participantId_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_participantId.CheckedChanged
        Me.cbo_ParticipantID.Enabled = Me.chk_participantId.Checked
    End Sub

    Private Sub chk_BillPeriod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_BillPeriod.CheckedChanged
        Me.cbo_billPeriod.Enabled = Me.chk_BillPeriod.Checked
    End Sub

    Private Sub cmd_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_close.Click
        Me.Close()
    End Sub

    Private Sub btn_TSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_TSearch.Click
        Dim tblOffsettingDetails As New DataTable
        With tblOffsettingDetails.Columns
            .Add("Parent ID")
            .Add("Child ID")
            .Add("Billing Period")
            .Add("Flag")
            .Add("Status")
        End With
        tblOffsettingDetails.AcceptChanges()

        OffsetDetails = Me.FilterSearch(OffsetDetails)
        For Each itmResult In OffsetDetails
            Dim dr As DataRow
            dr = tblOffsettingDetails.NewRow
            With itmResult
                dr("Parent ID") = dicIDToName(.PCNumber)
                dr("Child ID") = dicIDToName(.IDNumber)
                dr("Billing Period") = .BillPeriod

                Dim PFlag As String = ""
                If .ParentFlag = 1 Then
                    PFlag = EnumParentChild.Parent.ToString
                Else
                    PFlag = EnumParentChild.Child.ToString
                End If
                dr("Flag") = PFlag

                Dim strStat As String = ""
                If .Status = 1 Then
                    strStat = "Active"
                Else
                    strStat = "In-Active"
                End If
                dr("Status") = strStat

            End With
            tblOffsettingDetails.Rows.Add(dr)
            tblOffsettingDetails.AcceptChanges()
        Next

        If OffsetDetails.Count = 0 Then
            Me.dgv_ViewDetails.DataSource = Nothing
            MsgBox("No Records Found!", CType(MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, MsgBoxStyle), "No Records")
        Else
            Me.dgv_ViewDetails.DataSource = tblOffsettingDetails
        End If
    End Sub

    Function FilterSearch(ByVal OffsettingDetails As List(Of ParticipantParentChildMapping)) As List(Of ParticipantParentChildMapping)

        If Me.chk_participantId.Checked Then
            Dim _chkParticipant = (From x In AllParticipants _
                                  Where x.ParticipantID = Me.cbo_ParticipantID.Text _
                                  Select x).ToList

            If _chkParticipant.Count = 0 Then
                Return OffsettingDetails
            End If

            OffsettingDetails = Me.WBillHelper.GetParentChildMapping(dicNameToID(Me.cbo_ParticipantID.SelectedItem.ToString), True)

            If chk_ParentID.Checked Or chk_childID.Checked Then
                If chk_ParentID.Checked And chk_childID.Checked = False Then
                    OffsettingDetails = (From x In OffsettingDetails _
                                         Where x.ParentFlag = 1 _
                                         Select x).ToList
                ElseIf chk_ParentID.Checked = False And chk_childID.Checked Then
                    OffsettingDetails = (From x In OffsettingDetails _
                                         Where x.ParentFlag = 0 _
                                         Select x).ToList
                End If
            End If

            If chk_BillPeriod.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.BillPeriod = CInt(Me.cbo_billPeriod.Text.ToString) _
                                     Select x).ToList
            End If

            If rb_Active.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status = 1 _
                                     Select x).ToList
            ElseIf rb_InActive.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status <> 1 _
                                     Select x).ToList
            End If

            Return OffsettingDetails
        End If

        If Me.chk_BillPeriod.Checked Then
            OffsettingDetails = Me.WBillHelper.GetParentChildMappingPerBillingPeriod(CInt(Me.cbo_billPeriod.Text.ToString), True)

            If chk_ParentID.Checked Or chk_childID.Checked Then
                If chk_ParentID.Checked And chk_childID.Checked = False Then
                    OffsettingDetails = (From x In OffsettingDetails _
                                         Where x.ParentFlag = 1 _
                                         Select x).ToList
                ElseIf chk_ParentID.Checked = False And chk_childID.Checked Then
                    OffsettingDetails = (From x In OffsettingDetails _
                                         Where x.ParentFlag = 0 _
                                         Select x).ToList
                End If
            End If

            If rb_Active.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status = 1 _
                                     Select x).ToList
            ElseIf rb_InActive.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status <> 1 _
                                     Select x).ToList
            End If

            Return OffsettingDetails
        End If

        If Me.chk_BillPeriod.Checked = False And Me.chk_participantId.Checked = False Then
            OffsettingDetails = Me.WBillHelper.GetP2CMappingAllBP()

            If rb_Active.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status = 1 _
                                     Select x).ToList
            ElseIf rb_InActive.Checked Then
                OffsettingDetails = (From x In OffsettingDetails _
                                     Where x.Status <> 1 _
                                     Select x).ToList
            End If

            Return OffsettingDetails
        End If

        Return OffsettingDetails
    End Function

    Private Sub btn_New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_New.Click
        Dim frmEditParticipant As New frmParticipantMappingAddEdit
        With frmEditParticipant
            .isAddParent = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btn_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Edit.Click
        'Get selected Participant and Billing Period
        If Me.dgv_ViewDetails.RowCount = 0 Then
            Exit Sub
        End If

        Dim Status As String = Me.dgv_ViewDetails.CurrentRow.Cells("Status").Value.ToString
        If Status = "In-Active" Then
            MsgBox("Cannot modify In-Active records.", MsgBoxStyle.Exclamation, "Cannot Modify")
            Exit Sub
        End If

        Dim ParticipantID As String = Me.dgv_ViewDetails.CurrentRow.Cells("Child ID").Value.ToString
        Dim BillingPeriod As Integer = CInt(Me.dgv_ViewDetails.CurrentRow.Cells("Billing Period").Value.ToString)
        Dim ParentID As String = Me.dgv_ViewDetails.CurrentRow.Cells("Parent ID").Value.ToString

        Dim ParentFlag As String = Me.dgv_ViewDetails.CurrentRow.Cells("Flag").Value.ToString
        Dim ForEdit As Boolean = True

        If ParentFlag = EnumParentChild.Parent.ToString Then
            ForEdit = True
        ElseIf ParentFlag = EnumParentChild.Child.ToString Then
            ForEdit = False
        End If

        Dim GetParticipant As New AMParticipants
        GetParticipant = (From x In AllParticipants _
                          Where x.ParticipantID = ParticipantID _
                          Select x).FirstOrDefault

        Dim frmEditParticipant As New frmParticipantMappingAddEdit
        With frmEditParticipant
            .BillPeriod = BillingPeriod
            .EditParticipant = GetParticipant
            .isAddParent = ForEdit
            .isEdit = True
            .ShowDialog()
        End With
    End Sub

    Private Sub btn_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Del.Click
        Dim ans As New MsgBoxResult
        Try

            If Me.dgv_ViewDetails.RowCount = 0 Then
                MsgBox("No records found", MsgBoxStyle.Exclamation, "No Records")
                Exit Sub
            End If

            Dim Status As String = Me.dgv_ViewDetails.CurrentRow.Cells("Status").Value.ToString
            If Status = "In-Active" Then
                MsgBox("Cannot modify In-Active records.", MsgBoxStyle.Exclamation, "Cannot Modify")
                Exit Sub
            End If

            ans = MsgBox("Do you really want to disable the selected record?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Disable record")
            If ans = MsgBoxResult.No Then
                Exit Sub
            End If

            Dim ParticipantID As String = Me.dgv_ViewDetails.CurrentRow.Cells("Child ID").Value.ToString
            Dim BillingPeriod As Integer = CInt(Me.dgv_ViewDetails.CurrentRow.Cells("Billing Period").Value.ToString)
            Dim ParentID As String = Me.dgv_ViewDetails.CurrentRow.Cells("Parent ID").Value.ToString

            Dim isExisting As Integer = 0
            isExisting = Me.WBillHelper.IsParentExisting(dicNameToID(ParticipantID), CInt(BillingPeriod))

            If isExisting > 1 Then
                MsgBox("The system cannot process the transaction " & _
                       "There are still child participants assigned to " & ParentID & ".", MsgBoxStyle.Critical, "Cannot Disable")
                Exit Sub
            End If

            Dim ForDisable As New ParticipantParentChildMapping
            With ForDisable
                .PCNumber = dicNameToID(ParentID)
                .IDNumber = dicNameToID(ParticipantID)
                .BillPeriod = BillingPeriod
                .Status = 0
            End With

            Me.WBillHelper.DisableParentChildMapping(ForDisable)
            MsgBox("Successfully disabled the selected record.", CType(MsgBoxStyle.OkOnly + MsgBoxStyle.Information, MsgBoxStyle), "Success")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub btn_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        Me.dgv_ViewDetails.DataSource = Nothing
    End Sub
End Class