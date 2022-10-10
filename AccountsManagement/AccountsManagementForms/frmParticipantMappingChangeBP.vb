Option Explicit On
Option Strict On
Imports System.IO
Imports System.Windows.Forms
Imports AccountsManagementLogic
Imports AccountsManagementObjects
'Imports LDAPLib
'Imports LDAPLogin

Public Class frmParticipantMappingChangeBP
    Private WBillHelper As WESMBillHelper
    Private FromBP As Integer = 0
    Private ToBP As Integer = 0
    Private Sub frmParticipantMappingChangeBP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.MdiParent = MainForm
        Dim ConnectionString = "Data Source=192.168.1.80:1521/sdev01;USER ID=ismsadm;password=ismsadm;"
        WBillHelper = WESMBillHelper.GetInstance()
        WBillHelper.ConnectionString = AMModule.ConnectionString
        WBillHelper.UserName = AMModule.UserName

        Me.FillCboBoxes()
    End Sub

    Private Sub cmd_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancel.Click
        Me.Close()
    End Sub


    Private Sub FillCboBoxes()
        'Get Values
        Dim GetCalBillPeriods = WBillHelper.GetCalendarBP()
        Dim FromBillBP = (From x In GetCalBillPeriods Select x.BillingPeriod Order By BillingPeriod Descending).ToList
        'Fill FromBP CBO
        cbo_fromBP.DataSource = FromBillBP
        If cbo_fromBP.Items.Count > 0 Then
            cbo_fromBP.SelectedIndex = 0
            FromBP = CInt(cbo_fromBP.SelectedItem.ToString)
        End If

    End Sub


    Private Sub FillCBOToBp() Handles cbo_fromBP.SelectedIndexChanged
        Dim GetCalBillPeriods = WBillHelper.GetCalendarBP
        Dim FillToBP = (From x In GetCalBillPeriods _
                    Where x.BillingPeriod >= CDbl(cbo_fromBP.SelectedItem.ToString) _
                    Select x.BillingPeriod Order By BillingPeriod Ascending).ToList

        cbo_ToBP.DataSource = FillToBP
        If cbo_ToBP.Items.Count > 0 Then
            cbo_ToBP.SelectedIndex = 0
            ToBP = CInt(cbo_ToBP.SelectedItem.ToString)
            FromBP = CInt(cbo_fromBP.SelectedItem.ToString)
        End If
    End Sub


    Private Sub cmd_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_save.Click
        'validations
        Dim ans As MsgBoxResult = MsgBoxResult.No


        'Get Parent/Child Mapping for Chosen BP
        Dim cParentChildMapping = WBillHelper.GetParentChildMapping(0)
        Dim FromPCList = (From x In cParentChildMapping Where x.BillPeriod = FromBP And x.Status = 1).ToList

        If FromPCList.Count = 0 Then
            MsgBox("No Participant Mapping Found in the Billing period " & FromBP & ".", CType(MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, MsgBoxStyle))
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Failed, "Copy from billing period: " & FromBP & " to " & ToBP & " failed, no Mapping found for " & FromBP & ".")
            Exit Sub
        End If

        'Check if Target BP is already Offset
        Dim GPPostedLst = WBillHelper.GetWESMBillGPPosted()
        Dim chkIfOffset = (From x In GPPostedLst _
                           Where x.BillingPeriod = ToBP _
                           And x.PostType = "O" _
                           Select x).ToList
        If chkIfOffset.Count > 0 Then
            MsgBox("The target billing period has an existing transaction/s that is posted in the Great Plains," & _
                   "Please choose another Billing Period.", MsgBoxStyle.Critical, "Error")
            'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Failed, "Target billing period: " & ToBP & " already contains data posted in great plains.")
            Exit Sub
        End If

        ans = MsgBox("Do you really want to copy the Participant Mapping" & _
                     "From Billing Period " & FromBP & " To " & ToBP & "?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Save Mapping")

        If ans = MsgBoxResult.Yes Then
            'CheckFor Existing Mapping to the Current BP
            Dim chkExist = (From x In cParentChildMapping _
                            Where x.BillPeriod = ToBP _
                            And x.Status = 1 _
                            Select x).ToList
            If chkExist.Count > 0 Then
                Dim ans2 As MsgBoxResult = MsgBoxResult.No
                ans2 = MsgBox("Existing Mapping is found for the Billing Period " & ToBP & _
                              " Do you want to overwrite those settings?", CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), "Overwrite")

                If ans2 = MsgBoxResult.Yes Then
                    WBillHelper.SaveParentChildMapping(FromPCList, ToBP, True)
                    MsgBox("Copy Complete!", MsgBoxStyle.OkOnly)
                    'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, "Copied " & FromBP & " to " & ToBP & ", overwrite previous mapping in " & ToBP & ".")
                    Exit Sub
                End If
            Else
                For Each item In FromPCList
                    If item.ParentFlag = 1 Then
                        With item
                            .Remarks = "Copied Settings from " & FromBP & " Billing period to current Billing Period(" & ToBP & ") as of " & DateTime.Now
                        End With
                    Else
                        With item
                            .Remarks = "Child Settings from " & FromBP & " is copied to Current Billing Period(" & ToBP & "), Parent: " & item.PCNumber & " as of " & DateTime.Now
                        End With
                    End If
                Next
                WBillHelper.SaveParentChildMapping(FromPCList, ToBP)
                MsgBox("Copy Complete!", MsgBoxStyle.OkOnly)
                'LDAPModule.LDAP.InsertLogs(EnumAMSModules.AMS_ParticipantsWindow.ToString, eLogsStatus.Successful, "Copied " & FromBP & " to " & ToBP & ".")
                Exit Sub
            End If
        Else
            Exit Sub
        End If

    End Sub

    Private Sub cbo_ToBP_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_ToBP.SelectedIndexChanged
        ToBP = CInt(cbo_ToBP.SelectedItem.ToString)
    End Sub
End Class