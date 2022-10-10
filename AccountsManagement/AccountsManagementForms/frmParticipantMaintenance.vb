'***************************************************************************
'Copyright 2011: Philippine Electricity Market Corporation(PEMC)
'Form Name:              frmParticipantMaintenance
'Orginal Author:         Juan Carlo L. Panopio
'File Creation Date:     November 25, 2011
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
'   November 25, 2011   Juan Carlo L. Panopio                Form initialization and design
'   November 28, 2011   Juan Carlo L. Panopio                Finished functionalities for Set as Parent, Set as Child, Transfer Parent

Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmParticipantMaintenance
    Dim WBillHelper As WESMBillHelper
    Dim Admin As Boolean

    Private Sub frmParticipantMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = MainForm
        'Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        Dim ParentFlagging As String = ""
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Try
            'Fill BillPeriod Combo box
            Me.FillBPCbo()
            RB_ActiveParticipants.Checked = True
            ' Me.RefreshTables()
            CMD_SetParent.Enabled = False
            CMD_TransferParent.Enabled = False
            CMD_SetChild.Enabled = False
            If DGV_ParentParticipant.Rows.Count >= 0 Then
                'DGV_ParentParticipant.Rows(0).Selected = True
            End If
            Dim SelectedRow As Integer
            If DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
                ParentFlagging = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)
            End If

            Dim Colorize As System.Drawing.Color
            Colorize = Color.White

            Me.DGV_ParentParticipant.ColumnHeadersDefaultCellStyle.ForeColor = Colorize

            'Command Button Control 
            If UCase(ParentFlagging) = "PARENT" Then
                Me.ButtonControl(False, True, False)
            ElseIf UCase(ParentFlagging) = "CHILD" Then
                Me.ButtonControl(True, False, True)
            ElseIf Len(Trim(ParentFlagging)) = 0 Then
                Me.ButtonControl(True, True, False)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub RB_ActiveParticipants_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_ActiveParticipants.Click
        Try
            Me.RefreshTables()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub RB_ActiveInactiveParticipants_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RB_ActiveInactiveParticipants.Click
        Try
            Me.RefreshTables()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub CMD_SetParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_SetParent.Click
        Try
            Dim Participants = WBillHelper.GetAMParticipantsAll()
            Dim MappingList = WBillHelper.GetParentChildMapping(1)
            Dim SelectedRow As Integer
            If DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
            End If
            If SelectedRow = -1D Then
                MsgBox("No Participant is selected", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
            Dim ParticipantIds As String = CStr(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)
            Dim ans As MsgBoxResult = MsgBoxResult.No
            Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)
            Dim ForSaveParticipant As New ParticipantParentChildMapping
            Dim strLogs As String = ""
            'CHECK IF CHILD HAS EXISTING PARENT
            If UCase(ParentFlagging) = "CHILD" Then
                Dim CheckChild = (From x In MappingList Where x.IDNumber = ParticipantIds).Count
                If CheckChild <> 0 Then
                    ans = MsgBox("Do you really want to set " & DGV_ParentParticipant.Rows(SelectedRow).Cells(1).Value.ToString & " as parent?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
                    Dim SaveWithExisting = (From x In MappingList Where x.IDNumber = ParticipantIds).ToList
                    For Each item In SaveWithExisting
                        With ForSaveParticipant
                            .IDNumber = item.IDNumber
                            .PCNumber = item.PCNumber
                            .DateCreated = item.DateCreated
                            .BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString)
                        End With
                    Next
                    If ans = MsgBoxResult.Yes Then
                        WBillHelper.SaveParticipantAsParent(ForSaveParticipant)
                        strLogs = "Participant: " & ForSaveParticipant.IDNumber & " is set as Parent from child of " & SaveWithExisting.FirstOrDefault.PCNumber & "."
                        'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, strLogs)
                        'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ParticipantsWindow.ToString(), strLogs, "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccesffullySaved.ToString(), 'LDAPModule.LDAP.Username)            'New Syntax based on the standard Log Code 4/22/2014 Edited By Lance

                        MsgBox("Participant " & DGV_ParentParticipant.Rows(SelectedRow).Cells(1).Value.ToString & " is set as Parent successfully.")
                        Me.RefreshTables()
                    End If
                End If
            Else
                ans = MsgBox("Do you really want to set " & DGV_ParentParticipant.Rows(SelectedRow).Cells(1).Value.ToString & " as parent?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question)
                With ForSaveParticipant
                    .IDNumber = CStr(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)
                    .BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString)
                End With
                If ans = MsgBoxResult.Yes Then
                    WBillHelper.SaveParticipantAsParent(ForSaveParticipant)
                    MsgBox("Participant " & DGV_ParentParticipant.Rows(SelectedRow).Cells(1).Value.ToString & " is set as Parent successfully.")

                    strLogs = "Participant: " & ForSaveParticipant.IDNumber & " is set as Parent."
                    'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, strLogs)
                    'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ParticipantsWindow.ToString(), strLogs, "", "", EnumColorCode.Blue.ToString(), EnumLogType.SuccesffullySaved.ToString(), 'LDAPModule.LDAP.Username)            'New Syntax based on the standard Log Code 4/22/2014 Edited By Lance

                    Dim PCMapping = WBillHelper.GetParentChildMapping(1)
                    PCMapping = (From x In PCMapping Where x.IDNumber = ParticipantIds).ToList
                    Dim i = (From x In Participants Join y In PCMapping On x.IDNumber Equals y.IDNumber Where x.IDNumber = ParticipantIds _
                             And y.BillPeriod = CDbl(Me.cbo_BillPeriod.SelectedItem.ToString) Select x.IDNumber, x.ParticipantID, x.FullName _
                            , y.ParentFlag).ToList
                    With Me.DGV_ParentParticipant.Rows(Me.DGV_ParentParticipant.CurrentRow.Index)
                        For Each rec In i
                            .Cells(0).Value = rec.IDNumber
                            .Cells(1).Value = rec.ParticipantID
                            .Cells(2).Value = rec.FullName
                            .Cells(3).Value = IIf(rec.ParentFlag = 1, "Parent", "Child")
                            ParentFlagging = CStr(IIf(rec.ParentFlag = 1, "Parent", "Child"))
                        Next
                    End With

                    Me.DGV_ParentParticipant.Rows(Me.DGV_ParentParticipant.CurrentRow.Index).DefaultCellStyle.BackColor = Color.White
                    Me.DGV_ParentParticipant.Rows(Me.DGV_ParentParticipant.CurrentRow.Index).Selected = True

                    Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
                    If DGV_ParentParticipant.Rows.Count <> 0 Then
                        SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
                    End If
                    Dim Participant As Integer = CInt(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)
                    'Command Button Control if Parent/Child/Not Assigned Yet
                    'Command Button Control 
                    If UCase(ParentFlagging) = "PARENT" Then
                        Me.ButtonControl(False, True, False)
                    ElseIf UCase(ParentFlagging) = "CHILD" Then
                        Me.ButtonControl(True, False, True)
                    ElseIf Len(Trim(ParentFlagging)) = 0 Then
                        Me.ButtonControl(True, True, False)
                    End If

                    Dim CheckUnregistered = (From x In Participants Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
                                 Where y.Status = 1 And x.Status = 1 And y.BillPeriod = CInt(Me.cbo_BillPeriod.SelectedItem.ToString) _
                                 Select x Distinct).Count

                    Dim CheckCount = Participants.Count - CheckUnregistered
                    Me.TS_LBL_NewParticipants.Text = "There are still " & CheckCount & " new participants"
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Failed, "Set Participant as parent failed, Error Encountered: " & ex.Message)
            'LDAPModule.LDAP.InsertLog(Me.Name, EnumAMSModules.AMS_ParticipantsWindow.ToString(), ex.Message, "", "", EnumColorCode.Red.ToString(), EnumLogType.ErrorInSaving.ToString(), 'LDAPModule.LDAP.Username)            'New Syntax based on the standard Log Code 4/22/2014 Edited By Lance
            Exit Sub
        End Try
    End Sub

    Private Sub CMD_SetChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_SetChild.Click
        Try
            Dim SelectedRow As Integer
            If DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
            End If
            If SelectedRow = -1D Then
                MsgBox("No Participant is selected", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
            Dim ParticipantName As String = DGV_ParentParticipant.Rows(SelectedRow).Cells(1).Value.ToString
            Dim ParticipantID As String = CStr(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)
            Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)

            Dim ParticipantList = WBillHelper.GetAMParticipantsAll()
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            'if parent
            If UCase(ParentFlagging) = "PARENT" Then
                'Check if there are existing child
                Dim CheckChild = (From x In ParticipantMapping Where x.PCNumber = ParticipantID And x.Status = 1 And x.ParentFlag = 0 And x.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString)).Count
                If CheckChild <> 0 Then
                    MsgBox("Child Participants are found, please transfer them first and try again.", MsgBoxStyle.Critical, "Error")
                    'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Failed, "Set Participant: " & ParticipantID & " still has child.")
                    Exit Sub
                Else
                    frmParticipantMaintenanceMgt.SetAsChild(ParticipantName, CInt(cbo_BillPeriod.SelectedItem.ToString))
                End If
            ElseIf Len(Trim(ParentFlagging)) = 0 Then
                frmParticipantMaintenanceMgt.SetAsChild(ParticipantName, CInt(cbo_BillPeriod.SelectedItem.ToString))
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_ParentParticipant_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_ParentParticipant.CellDoubleClick
        Try
            Dim ParticipantID As Integer = CInt(DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(0).Value.ToString)
            frmParticipantMaintenanceLogs.LoadLogs(CStr(ParticipantID))
            'Dim ParticipantList = WBillHelper.GetAMParticipantsAll()
            'Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            'Dim SelectedRow As Integer

            'Try
            '    If DGV_ParentParticipant.Rows.Count <> 0 Then
            '        SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
            '    End If
            '    'MsgBox(e.RowIndex)
            '    If e.RowIndex = -1D Then
            '        Exit Sub
            '    End If

            '    Dim Participant As Integer = CInt(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)

            '    Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)

            '    If UCase(ParentFlagging) = "PARENT" Then

            '        Dim ParticipantIsParent = (From x In ParticipantList Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
            '                                   Where y.PCNumber = Participant And y.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
            '                                   Select y Order By y.UpdatedDate Descending).ToList
            '        Me.CreateDTForDetails(ParticipantIsParent, "P")
            '    ElseIf UCase(ParentFlagging) = "CHILD" Then

            '        Dim ParticipantIsChild = (From x In ParticipantList Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
            '                                  Where y.IDNumber = Participant And y.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
            '                                Select y Order By y.UpdatedDate Descending).ToList
            '        Me.CreateDTForDetails(ParticipantIsChild, "C")
            '    End If

            '    'Command Button Control if Parent/Child/Not Assigned Yet
            '    'Command Button Control 
            '    If UCase(ParentFlagging) = "PARENT" Then
            '        Me.ButtonControl(False, True, False)
            '    ElseIf UCase(ParentFlagging) = "CHILD" Then
            '        Me.ButtonControl(True, False, True)
            '    ElseIf Len(Trim(ParentFlagging)) = 0 Then
            '        Me.ButtonControl(True, True, False)
            '    End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub DGV_ParentParticipant_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_ParentParticipant.CellClick
        Try
            Dim ParticipantList = WBillHelper.GetAMParticipantsAll()
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            Dim SelectedRow As Integer

            If DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
            End If

            If SelectedRow = -1 Then
                Exit Sub
            End If

            Dim Participant As Integer = CInt(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)

            Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)

            'Command Button Control if Parent/Child/Not Assigned Yet
            'Command Button Control 
            If UCase(ParentFlagging) = "PARENT" Then
                Me.ButtonControl(False, True, False)
            ElseIf UCase(ParentFlagging) = "CHILD" Then
                Me.ButtonControl(True, False, True)
            ElseIf Len(Trim(ParentFlagging)) = 0 Then
                Me.ButtonControl(True, True, False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Public Sub RefreshTables()
        Try
            Dim Participants = WBillHelper.GetAMParticipantsAll()
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            Dim SelectedRow As Integer

            If Participants.Count = 0 Then
                MsgBox("No Registrered Participants Found", MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If

            'If ParticipantMapping.Count = 0 Then
            '    MsgBox("No Participant mapping found", MsgBoxStyle.Critical, "Error")
            '    Exit Sub
            'End If

            If DGV_ParentParticipant.Rows.Count <> 0 Then
                SelectedRow = CInt(DGV_ParentParticipant.CurrentRow.Index)
            End If

            If RB_ActiveParticipants.Checked = True Then
                Participants = (From x In Participants Where x.Status = 1).ToList
            End If

            DGV_ParentParticipant.Rows.Clear()

            For Each item In Participants
                Dim ParticipantNumber = item.IDNumber
                Dim Flag = (From x In ParticipantMapping Where x.IDNumber = ParticipantNumber And x.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) Select x.ParentFlag).ToList
                If Flag.Count <> 0 Then
                    DGV_ParentParticipant.Rows.Add(item.IDNumber, item.ParticipantID, item.FullName, IIf(CDbl(Flag.FirstOrDefault.ToString) = 1, "Parent", "Child"))
                Else
                    DGV_ParentParticipant.Rows.Add(item.IDNumber, item.ParticipantID, item.FullName, "")
                    DGV_ParentParticipant.Rows(DGV_ParentParticipant.Rows.Count - 1).DefaultCellStyle.BackColor = Color.Pink
                End If
            Next

            Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)

            Dim CheckUnregistered = (From x In Participants Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
                                     Where y.Status = 1 And x.Status = 1 And y.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                                     Select x Distinct).Count

            Dim CheckCount = Participants.Count - CheckUnregistered
            TS_LBL_NewParticipants.Text = "There are still " & CheckCount & " new participants"
            'Command Button Control if Parent/Child/Not Assigned Yet
            'Command Button Control 
            If UCase(ParentFlagging) = "PARENT" Then
                Me.ButtonControl(False, True, False)
            ElseIf UCase(ParentFlagging) = "CHILD" Then
                Me.ButtonControl(True, False, True)
            ElseIf Len(Trim(ParentFlagging)) = 0 Then
                Me.ButtonControl(True, True, False)
            End If
        Catch ex As Exception
            Throw New ApplicationException(ex.Message)
        End Try
    End Sub

    Private Sub CMD_TransferParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_TransferParent.Click
        Try

            If DGV_ParentParticipant.CurrentRow Is Nothing Then
                Exit Sub
            End If
            Dim ParticipantName As String = DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(1).Value.ToString
            Dim ParticipantID As Integer = CInt(DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(0).Value.ToString)
            Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(3).Value.ToString)
            Dim ParticipantList = WBillHelper.GetAMParticipantsAll()
            Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
            frmParticipantMaintenanceMgt.TransferParent(ParticipantName, CInt(cbo_BillPeriod.SelectedItem.ToString))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub


    Private Sub cmd_ViewLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ViewLogs.Click
        If DGV_ParentParticipant.CurrentRow Is Nothing Then
            Exit Sub
        End If
        Dim ParticipantID As Integer = CInt(DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(0).Value.ToString)
        frmParticipantMaintenanceLogs.LoadLogs(CStr(ParticipantID))
    End Sub

    Private Sub CMD_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CMD_Close.Click
        Me.Close()
    End Sub

    Private Sub FillBPCbo()
        Dim CalendarBP = WBillHelper.GetCalendarBP()

        If CalendarBP.Count = 0 Then
            Throw New ApplicationException("No Billing Period Found")
            Exit Sub
        End If

        Dim BillPeriods = (From x In CalendarBP _
                           Where x.BillingPeriod <> 0 _
                           Select x.BillingPeriod _
                           Order By BillingPeriod Descending).ToList
        cbo_BillPeriod.DataSource = BillPeriods
    End Sub


    Private Sub cmd_CopyToBillPd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_CopyToBillPd.Click
        If cbo_BillPeriod.SelectedIndex = -1 Then
            Exit Sub
        End If
        'Copy current selected Mapping to new billing Period
        frmParticipantMappingChangeBP.ShowDialog()
    End Sub

    Private Sub CBOChanged() Handles cbo_BillPeriod.SelectedIndexChanged
        Try
            Dim GetParticipants = WBillHelper.GetAMParticipantsAll()
            Dim GetParticipantsMapping = WBillHelper.GetParentChildMapping(0)

            Me.RefreshTables()
            Me.ButtonControl(True, True, True)
            
            Dim CheckUnregistered = (From x In GetParticipants Join y In GetParticipantsMapping On x.IDNumber Equals y.IDNumber _
                                     Where y.Status = 1 And x.Status = 1 And y.BillPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                                     Select x Distinct).Count
            If Not RB_ActiveInactiveParticipants.Checked Then
                GetParticipants = (From x In GetParticipants Where x.Status = 1).ToList
            End If

            Dim CheckCount As Integer
            CheckCount = GetParticipants.Count - CheckUnregistered
            MsgBox("There are " & CheckCount & " Unmapped participants", MsgBoxStyle.Exclamation, "Unmapped Participants Found")
            TS_LBL_NewParticipants.Text = "There are still " & CheckCount & " new participants"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

    Private Sub ButtonControl(ByVal ParentBTN As Boolean, ByVal ChildBTN As Boolean, ByVal TransferBTN As Boolean)
        'Primary Validation
        'CHECK CONTROLS IF PARTICIPANT ALREADY HAS OFFSETTING
        Dim GetGPPosted = WBillHelper.GetWESMBillGPPosted()
        Dim ParentFlagging As String

        If cbo_BillPeriod.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim chkBills = (From x In GetGPPosted _
                        Where x.PostType = "O" _
                        And x.BillingPeriod = CInt(cbo_BillPeriod.SelectedItem.ToString) _
                        Select x).ToList
        ' O - Offsetting Type
        ' Current Billing Period
        If chkBills.Count > 0 Then
            CMD_SetParent.Enabled = False
            CMD_SetChild.Enabled = False
            CMD_TransferParent.Enabled = False
            Exit Sub
        End If

        'If Not yet Offset
        If DGV_ParentParticipant.Rows.Count <> 0 Then
            ParentFlagging = (DGV_ParentParticipant.Rows(DGV_ParentParticipant.CurrentRow.Index).Cells(3).Value.ToString)
        End If

        'If UCase(ParentFlagging) = "PARENT" Then
        '    CMD_SetParent.Enabled = ParentBTN
        '    CMD_SetChild.Enabled = ChildBTN
        '    CMD_TransferParent.Enabled = TransferBTN
        'ElseIf UCase(ParentFlagging) = "PARENT" Then
        '    CMD_SetParent.Enabled = ParentBTN
        '    CMD_SetChild.Enabled = ChildBTN
        '    CMD_TransferParent.Enabled = TransferBTN
        'ElseIf Len(Trim(ParentFlagging)) = 0 Then
        '    CMD_SetParent.Enabled = ParentBTN
        '    CMD_SetChild.Enabled = ChildBTN
        '    CMD_TransferParent.Enabled = TransferBTN
        'End If

        CMD_SetParent.Enabled = ParentBTN
        CMD_SetChild.Enabled = ChildBTN
        CMD_TransferParent.Enabled = TransferBTN
    End Sub

    Private Sub CreateDTForDetails(ByVal ForDetails As List(Of ParticipantParentChildMapping), ByVal Flag As String)
        Dim Participants = WBillHelper.GetAMParticipantsAll()
        Dim dt As New DataTable
        With dt
            .Columns.Add("BillPeriod")
            .Columns.Add("IdNumber")
            .Columns.Add("ParticipantID")
            .Columns.Add("Remarks")
            .Columns.Add("Status")
        End With

        For Each item In ForDetails
            Dim dr As DataRow
            dr = dt.NewRow
            With item
                dr("BillPeriod") = .BillPeriod
                If Flag = "C" Then
                    dr("IdNumber") = .PCNumber
                    dr("ParticipantID") = (From x In Participants Where _
                                           x.IDNumber = .PCNumber _
                                           Select x.ParticipantID).First

                Else
                    dr("IdNumber") = .IDNumber
                    dr("ParticipantID") = (From x In Participants Where _
                                           x.IDNumber = .IDNumber _
                                           Select x.ParticipantID).First
                End If

                dr("Remarks") = .Remarks
                If .Status = 1 Then
                    dr("Status") = "Active"
                Else
                    dr("Status") = "In-Active"
                End If

            End With
            dt.Rows.Add(dr)
        Next
        Dim newFrm As New frmViewDetails
        With newFrm
            .ViewParticipantDetails(dt)
            .ShowDialog()
        End With


    End Sub

    Private Sub cmd_viewDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_viewDetails.Click
        Dim Participants = WBillHelper.GetAMParticipantsAll
        Dim ParticipantMapping = WBillHelper.GetParentChildMapping(0)
        If DGV_ParentParticipant.CurrentRow Is Nothing Then
            Exit Sub
        End If
        Dim SelectedRow As Integer = CInt(DGV_ParentParticipant.CurrentRow.Index)
        DGV_ParentParticipant.Rows(SelectedRow).Selected = True

        Dim Participant As String = CStr(DGV_ParentParticipant.Rows(SelectedRow).Cells(0).Value.ToString)
        Dim ParentFlagging As String = (DGV_ParentParticipant.Rows(SelectedRow).Cells(3).Value.ToString)

        If UCase(ParentFlagging) = "PARENT" Then
            Dim ParticipantLst As New List(Of ParticipantParentChildMapping)

            Dim ParticipantIsParent = (From x In Participants Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
                                       Where y.PCNumber = Participant And y.Status = 1 And y.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                       Select y Order By y.UpdatedDate Descending).ToList
            Me.CreateDTForDetails(ParticipantIsParent, "P")
        ElseIf UCase(ParentFlagging) = "CHILD" Then
            Dim ParticipantIsChild = (From x In Participants Join y In ParticipantMapping On x.IDNumber Equals y.IDNumber _
                                      Where y.IDNumber = Participant And y.Status = 1 And y.BillPeriod = CDbl(cbo_BillPeriod.SelectedItem.ToString) _
                                      Select y Order By y.UpdatedDate Descending).ToList
            Me.CreateDTForDetails(ParticipantIsChild, "C")
        End If

        DGV_ParentParticipant.Rows(SelectedRow).Selected = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.RefreshTables()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
    End Sub

End Class