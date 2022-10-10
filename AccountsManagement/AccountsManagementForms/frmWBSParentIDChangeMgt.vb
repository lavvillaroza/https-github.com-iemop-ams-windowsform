Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
Imports WESMLib.Auth.Lib

Public Class frmWBSParentIDChangeMgt
    Public WBSChangeParentIDHelpr As New WBSChangeParentIdHelper
    Public BillingPeriod As CalendarBillingPeriod
    Public isForUPdate As Boolean
    Private selectParentId As String
    Private selectChildId As String
    Private selectNewParentId As String
    Private selectStatus As String

    Private Sub frmWESMBillSummaryParentIdChangeMgt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LoadComboBox()
    End Sub
    Public Sub UpdateParent(ByVal parentid As String, ByVal childid As String, ByVal newparent As String, ByVal status As String)
        Me.selectParentId = parentid
        Me.selectChildId = childid
        Me.selectNewParentId = newparent
        Me.selectStatus = status
    End Sub

    Private Sub LoadComboBox()
        Me.ClearForm()

        Me.cmb_ParentID.Enabled = True
        Me.cmb_ParentID.Items.Clear()
        Me.txtbox_ParentID.Clear()
        For Each item In WBSChangeParentIDHelpr.AMParticipantsList
            Me.cmb_ParentID.Items.Add(item.IDNumber)
        Next

        Me.cmb_ChildID.Enabled = True
        Me.cmb_ChildID.Items.Clear()
        Me.txtbox_ChildID.Clear()
        For Each item In WBSChangeParentIDHelpr.AMParticipantsList
            Me.cmb_ChildID.Items.Add(item.IDNumber)
        Next

        Me.cmb_NewParentID.Enabled = True
        Me.cmb_NewParentID.Items.Clear()
        Me.txtbox_NewParentID.Clear()
        For Each item In WBSChangeParentIDHelpr.AMParticipantsList
            Me.cmb_NewParentID.Items.Add(item.IDNumber)
        Next

        If Me.isForUPdate = True Then
            Me.cmb_ParentID.SelectedIndex = Me.cmb_ParentID.FindStringExact(selectParentId)
            Me.cmb_ChildID.SelectedIndex = Me.cmb_ChildID.FindStringExact(selectChildId)
            Me.cmb_NewParentID.SelectedIndex = Me.cmb_NewParentID.FindStringExact(selectNewParentId)
            If selectStatus = EnumStatus.Active.ToString() Then
                Me.rd_active.Checked = True
            Else
                Me.rd_Inactive.Checked = True
            End If
            If Me.rd_active.Enabled = False And Me.rd_Inactive.Enabled = False Then
                Me.rd_active.Enabled = True
                Me.rd_Inactive.Enabled = True
            End If
            Me.cmb_ParentID.Enabled = False
            Me.cmb_ChildID.Enabled = False
            Me.txtbox_ParentID.Enabled = False
            Me.txtbox_NewParentID.Enabled = False
        Else
            Me.rd_active.Checked = True
            Me.rd_Inactive.Checked = False
            Me.rd_active.Enabled = False
            Me.rd_Inactive.Enabled = False
        End If
    End Sub
    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Me.Close()
    End Sub

    Private Sub cmb_ParentID_LostFocus(sender As Object, e As EventArgs) Handles cmb_ParentID.LostFocus
        Try
            Dim checkInputParticipant As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = cmb_ParentID.SelectedItem Select x.IDNumber).FirstOrDefault
            If checkInputParticipant Is Nothing Or cmb_ParentID.SelectedItem Is Nothing Then
                Me.lbl_asterisk1.ForeColor = Color.Red
            Else
                Me.getParentName()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub cmb_ParentID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ParentID.SelectedIndexChanged
        Me.getParentName()
    End Sub

    Private Sub getParentName()
        Dim getParticipantName As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_ParentID.SelectedItem Select x.ParticipantID).FirstOrDefault
        Me.txtbox_ParentID.Enabled = True
        Me.txtbox_ParentID.Text = getParticipantName
        Me.txtbox_ParentID.Enabled = False
        Me.lbl_asterisk1.ForeColor = Color.Black
    End Sub


    Private Sub cmb_ChildID_LostFocus(sender As Object, e As EventArgs) Handles cmb_ChildID.LostFocus
        Try
            Dim checkInputParticipant As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = cmb_ChildID.SelectedItem Select x.IDNumber).FirstOrDefault
            If checkInputParticipant Is Nothing Or cmb_ChildID.SelectedIndex = -1 Then
                Me.lbl_asterisk2.ForeColor = Color.Red
            Else
                Me.getChildName()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub cmb_ChildID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ChildID.SelectedIndexChanged
        Me.getChildName()
    End Sub

    Private Sub getChildName()
        Dim getParticipantName As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_ChildID.SelectedItem Select x.ParticipantID).FirstOrDefault
        Me.txtbox_ChildID.Enabled = True
        Me.txtbox_ChildID.Text = getParticipantName
        Me.txtbox_ChildID.Enabled = False
        Me.lbl_asterisk2.ForeColor = Color.Black
    End Sub

    Private Sub cmb_NewParentID_LostFocus(sender As Object, e As EventArgs) Handles cmb_NewParentID.LostFocus
        Try
            Dim checkInputParticipant As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = cmb_NewParentID.SelectedItem Select x.IDNumber).FirstOrDefault
            If checkInputParticipant Is Nothing Or cmb_NewParentID.SelectedIndex = -1 Then
                Me.lbl_asterisk3.ForeColor = Color.Red
            Else
                Me.getNewParentName()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub cmb_NewParentID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NewParentID.SelectedIndexChanged
        Me.getNewParentName()
    End Sub

    Private Sub getNewParentName()
        Dim getParticipantName As String = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_NewParentID.SelectedItem Select x.ParticipantID).FirstOrDefault
        Me.txtbox_NewParentID.Enabled = True
        Me.txtbox_NewParentID.Text = getParticipantName
        Me.txtbox_NewParentID.Enabled = False
        Me.lbl_asterisk3.ForeColor = Color.Black
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        If Me.cmb_ParentID.SelectedIndex = -1 Then
            Me.lbl_asterisk1.ForeColor = Color.Red
        Else
            Me.lbl_asterisk1.ForeColor = Color.Black
        End If

        If Me.cmb_ChildID.SelectedIndex = -1 Then
            Me.lbl_asterisk2.ForeColor = Color.Red
        Else
            Me.lbl_asterisk2.ForeColor = Color.Black
        End If

        If Me.cmb_NewParentID.SelectedIndex = -1 Then
            Me.lbl_asterisk3.ForeColor = Color.Red
        Else
            Me.lbl_asterisk3.ForeColor = Color.Black
        End If

        If Me.lbl_asterisk1.ForeColor = Color.Red Or Me.lbl_asterisk2.ForeColor = Color.Red _
            Or Me.lbl_asterisk3.ForeColor = Color.Red Or Me.lbl_asterisk4.ForeColor = Color.Red Then
            MessageBox.Show("Please fillup the required field.", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Me.cmb_ParentID.SelectedItem = Me.cmb_NewParentID.SelectedItem Then
            MessageBox.Show("Old parentid is same in new parentid kindly check.", "System Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim getParent As AMParticipants = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_ParentID.SelectedItem Select x).FirstOrDefault
        Dim getChild As AMParticipants = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_ChildID.SelectedItem Select x).FirstOrDefault
        Dim getNewParent As AMParticipants = (From x In WBSChangeParentIDHelpr.AMParticipantsList Where x.IDNumber = Me.cmb_NewParentID.SelectedItem Select x).FirstOrDefault
        Dim getEnumStatus As EnumStatus = If(Me.rd_active.Checked = True, EnumStatus.Active, EnumStatus.InActive)

        Dim AddUpdateWBSChangeParentID As New WESMBillSummaryChangeParentId(BillingPeriod.BillingPeriod, getParent, _
                                                                            getChild, getNewParent, _
                                                                            getEnumStatus)
        Try
            Dim msgAns As New MsgBoxResult
            msgAns = MsgBox("Do you really want to save?", CType(MsgBoxStyle.YesNo + MsgBoxStyle.Question, MsgBoxStyle), "System Message")

            If msgAns = MsgBoxResult.Yes Then
                If isForUPdate = True Then
                    ProgressThread.Show("Please wait while saving...")
                    WBSChangeParentIDHelpr.Update(AddUpdateWBSChangeParentID)
                    ProgressThread.Close()
                    MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibWESMBillChangeParentIDWindow.ToString,
                                    "The processed data has been successfully saved", "Updating", "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)
                    Me.ClearForm()
                    Me.Close()
                    Dim bpNo As Integer = BillingPeriod.BillingPeriod
                    Dim WBSChangeParticipantIDList As List(Of WESMBillSummaryChangeParentId) = WBSChangeParentIDHelpr.GetobjWESMBillSummaryChangeParent(bpNo)
                    Dim getWBSChangeParentIDSelected = (From x In WBSChangeParticipantIDList
                                                        Where x.BillingPeriod = AddUpdateWBSChangeParentID.BillingPeriod _
                                                        And x.ParentParticipants.IDNumber = AddUpdateWBSChangeParentID.ParentParticipants.IDNumber _
                                                        And x.ChildParticipants.IDNumber = AddUpdateWBSChangeParentID.ChildParticipants.IDNumber _
                                                        Select x).FirstOrDefault
                    With frmWBSParentIdChange
                        Dim i As Integer = .dgv_wbsChangeParentId.CurrentRow.Index
                        With .dgv_wbsChangeParentId
                            .Rows(i).Cells("NewParentIdNo").Value = getWBSChangeParentIDSelected.NewParentParticipants.IDNumber
                            .Rows(i).Cells("NewParentName").Value = getWBSChangeParentIDSelected.NewParentParticipants.ParticipantID
                            .Rows(i).Cells("Status").Value = getWBSChangeParentIDSelected.Status
                            .Rows(i).Cells("UpdatedBy").Value = getWBSChangeParentIDSelected.UpdatedBy
                            .Rows(i).Cells("UpdatedDate").Value = getWBSChangeParentIDSelected.UpdatedDate
                        End With

                    End With



                Else
                    If WBSChangeParentIDHelpr.CheckIfDuplicate(AddUpdateWBSChangeParentID) = True Then
                        MessageBox.Show("The selected data is already added.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ProgressThread.Show("Please wait while saving...")
                    WBSChangeParentIDHelpr.Save(AddUpdateWBSChangeParentID)
                    ProgressThread.Close()
                    MessageBox.Show("The processed data have been successfully saved.", "System Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    _Login.InsertLog(CDate(SystemDate.ToString("MM/dd/yyyy")), "Accounts Management System", EnumAMSModulesFinal.LibWESMBillChangeParentIDWindow.ToString,
                                    "The processed data  has been successfully saved", "Adding", "", CType(EnumColorCode.Green, ColorCode), EnumLogType.SuccesffullySaved.ToString, AMModule.UserName)

                    Me.ClearForm()
                    Dim bpNo As Integer = BillingPeriod.BillingPeriod
                    Dim WBSChangeParticipantIDList As List(Of WESMBillSummaryChangeParentId) = WBSChangeParentIDHelpr.GetobjWESMBillSummaryChangeParent(bpNo)
                    Dim GetUpdatedItem As WESMBillSummaryChangeParentId = (From x In WBSChangeParticipantIDList _
                                                                           Where x.BillingPeriod = AddUpdateWBSChangeParentID.BillingPeriod _
                                                                           And x.ParentParticipants.IDNumber = AddUpdateWBSChangeParentID.ParentParticipants.IDNumber _
                                                                           And x.ChildParticipants.IDNumber = AddUpdateWBSChangeParentID.ChildParticipants.IDNumber _
                                                                           And x.NewParentParticipants.IDNumber = AddUpdateWBSChangeParentID.NewParentParticipants.IDNumber _
                                                                           Select x).FirstOrDefault
                    frmWBSParentIdChange.dgv_wbsChangeParentId.Rows.Add(GetUpdatedItem.NewParentParticipants.IDNumber, GetUpdatedItem.NewParentParticipants.ParticipantID,
                                                                                    GetUpdatedItem.ChildParticipants.IDNumber, GetUpdatedItem.ChildParticipants.ParticipantID, GetUpdatedItem.NewParentParticipants.IDNumber,
                                                                                    GetUpdatedItem.NewParentParticipants.ParticipantID, GetUpdatedItem.Status, GetUpdatedItem.UpdatedBy, GetUpdatedItem.UpdatedDate, "UPDATE")
                End If
            End If
        Catch ex As Exception
            ProgressThread.Close()
            MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error)            
        End Try
    End Sub

    Private Sub cmb_Status_SelectedIndexChanged(sender As Object, e As EventArgs)
        Me.lbl_asterisk4.ForeColor = Color.Black
    End Sub

    Private Sub ClearForm()
        Me.cmb_ParentID.SelectedIndex = -1
        Me.txtbox_ParentID.Clear()
        Me.cmb_ChildID.SelectedIndex = -1
        Me.txtbox_ChildID.Clear()
        Me.cmb_NewParentID.SelectedIndex = -1
        Me.txtbox_NewParentID.Clear()
    End Sub
End Class